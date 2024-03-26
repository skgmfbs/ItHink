using WorkerServiceSample;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Worker background service";
});

var host = builder.Build();
await host.RunAsync();