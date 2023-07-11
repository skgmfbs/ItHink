using IBM.XMS;
using MiniAPI.DTOs;
using System.Text.Json;

namespace Services
{
    public interface IPublishingService
    {
        Task<bool> ProceedAsync<T>(PublishedDto<T> dto) where T : struct;
    }

    public class PublishingService : IPublishingService
    {
        private readonly IConfiguration _config;

        public PublishingService(IConfiguration config)
        {
            _config = config;
        }

        public Task<bool> ProceedAsync<T>(PublishedDto<T> dto) where T : struct
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
                            using (var producer = session.CreateProducer(destination))
                            {
                                connection.Start();

                                var textMessage = session.CreateTextMessage();
                                var message = JsonSerializer.Serialize(dto);
                                textMessage.Text = message;

                                producer.Send(textMessage);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
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
}