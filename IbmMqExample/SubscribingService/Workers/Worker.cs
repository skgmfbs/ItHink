using System.Text;
using System.Text.Json;
using IBM.XMS;
using SubscribingService.DTOs;

namespace SubscribingService.Workers;

public class Worker : BackgroundService
{
    private readonly IConfiguration _config;

    public Worker(ILogger<Worker> logger, IConfiguration config)
    {
        _config = config;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            using (var connection = CreateConnectionFactory().CreateConnection())
            {
                using (var session = connection.CreateSession(false, AcknowledgeMode.AutoAcknowledge))
                {
                    using (var destination = session.CreateTopic(_config["MQ:TopicName"]))
                    {
                        destination.SetIntProperty(XMSC.WMQ_TARGET_CLIENT, XMSC.WMQ_TARGET_DEST_MQ);
                        using (var consumer = session.CreateConsumer(destination))
                        {
                            connection.Start();
                            var timeout = Convert.ToInt32(_config["MQ:Timeout"]);

                            while (!stoppingToken.IsCancellationRequested)
                            {
                                var message = consumer.Receive(timeout);
                                if (message == null) continue;

                                var data = "";
                                try
                                {
                                    data = ((ITextMessage)message).Text;
                                }
                                catch (Exception)
                                {
                                    try
                                    {
                                        var byteMessage = (IBytesMessage)message;
                                        var byteArr = new byte[(int)byteMessage.BodyLength];
                                        byteMessage.ReadBytes(byteArr);

                                        data = Encoding.UTF8.GetString(byteArr, 0, byteArr.Length);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }

                                Console.WriteLine("Received message:" + data);

                                var publishedDto = JsonSerializer.Deserialize<PublishedDto<ToDoResultDto>>(data);

                                try
                                {
                                    using (var client = new HttpClient())
                                    {
                                        var todoState = new ToDoStateDto
                                        {
                                            ToDoId = publishedDto.MessageObject.Id,
                                            Status = ToDoStatus.InProgress
                                        };
                                        var todoStateString = JsonSerializer.Serialize(todoState);
                                        var apiHost = _config["ApiHost"];
                                        using (var response = await client.PostAsync("/api/todos/state", new StringContent(todoStateString)))
                                        {
                                            response.EnsureSuccessStatusCode();
                                            string responseBody = await response.Content.ReadAsStringAsync();
                                            // Above three lines can be replaced with new helper method below
                                            // string responseBody = await client.GetStringAsync(uri);

                                            Console.WriteLine(responseBody);
                                        }
                                    }
                                }
                                catch (HttpRequestException e)
                                {
                                    Console.WriteLine("Exception message :{0} ", e.Message);
                                }

                                await Task.Delay(1000, stoppingToken);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private IConnectionFactory CreateConnectionFactory()
    {
        var connectionFactory = XMSFactoryFactory.GetInstance(XMSC.CT_WMQ).CreateConnectionFactory();

        connectionFactory.SetIntProperty(XMSC.WMQ_CLIENT_RECONNECT_OPTIONS, XMSC.WMQ_CLIENT_RECONNECT_Q_MGR);
        connectionFactory.SetStringProperty(XMSC.WMQ_HOST_NAME, _config["MQ:Host"]);
        connectionFactory.SetIntProperty(XMSC.WMQ_PORT, Convert.ToInt32(_config["MQ:Port"]));
        connectionFactory.SetStringProperty(XMSC.WMQ_QUEUE_MANAGER, _config["MQ:Qmgr"]);
        connectionFactory.SetStringProperty(XMSC.WMQ_CHANNEL, _config["MQ:Channel"]);
        //connectionFactory.SetStringProperty(XMSC.WMQ_SSL_CIPHER_SPEC, _config["MQ:Cipher"]);
        connectionFactory.SetStringProperty(XMSC.USERID, _config["MQ:AppUserId"]);
        connectionFactory.SetStringProperty(XMSC.PASSWORD, _config["MQ:AppPassword"]);
        connectionFactory.SetIntProperty(XMSC.WMQ_CLIENT_RECONNECT_TIMEOUT, Convert.ToInt32(_config["MQ:Timeout"]));

        return connectionFactory;
    }
}
