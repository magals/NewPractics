// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using SignalRSendFilesCheckHash.Models;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .Enrich.WithEnvironmentName()
    .Enrich.WithEnvironmentUserName()
    .Enrich.WithClientIp()
    .Enrich.WithClientAgent()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341/", apiKey: "b9HQGJhY8swnzp5b1biv")
    .CreateLogger();

try
{
  Log.Logger.Information("Start Program");
  HubConnection connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5206" + "/SyncHub")
                .AddMessagePackProtocol()
                .Build();
  connection.StartAsync().Wait();

  Directory.CreateDirectory("assets");

  Task.Run(async () =>
  {
      await foreach (var file in connection.StreamAsync<FileDTO>("SyncGetFileListAsync"))
      {
      Log.Logger.Information("file name:{0} file.Content:{1}", file.FileName, file.Content.Length);
        await File.WriteAllBytesAsync("assets//" + file.FileName, file.Content);
      }
      GC.Collect();
    await connection.DisposeAsync();
  }).Wait();

}
catch (Exception ex)
{
  Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
  Log.Logger.Information("Close Program");
  Log.CloseAndFlush();
}