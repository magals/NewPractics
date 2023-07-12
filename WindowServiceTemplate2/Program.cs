using Microsoft.Extensions.Hosting;
using WindowServiceTemplate2;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
      options.ServiceName = "Code-Maze Service";
    })
    .ConfigureServices(services =>
    {
      services.AddHostedService<Worker>();
    })
    .Build();
await host.RunAsync();