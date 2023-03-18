using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using SignalRSendFilesCheckHash.Models;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

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

  IConfiguration Configuration = new ConfigurationBuilder()
   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
   .AddEnvironmentVariables()
   .Build();

  AppConfig appConfig = new AppConfig();
  Configuration.Bind(nameof(AppConfig), appConfig);

  HubConnection connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5206" + "/SyncHub")
                .AddMessagePackProtocol()
                .Build();
  connection.StartAsync().Wait();

  Directory.CreateDirectory("assets");

  ArgumentException.ThrowIfNullOrEmpty(appConfig.PathContent);

  DirectoryInfo directinfo = new DirectoryInfo(appConfig.PathContent);
  FileInfo[] Files = directinfo.GetFiles();
  var buffer = new byte[16];
  foreach (FileInfo file in Files)
  {
    using (var md5 = MD5.Create())
    {
      using (var stream = File.OpenRead(file.FullName))
      {
        var asd = md5.ComputeHash(stream);
        buffer = HelpersMethods.Xor(buffer.ToArray(), asd);
      }
    }
  }
  appConfig.HashAllContent = BitConverter.ToString(buffer.ToArray());


  string responcehash = connection.InvokeAsync<string>("CheckHashContent").Result;

  if (!string.Equals(appConfig.HashAllContent, responcehash))
  {
    await foreach (var file in connection.StreamAsync<FileDTO>("SyncGetFileListAsync"))
    {
      Log.Logger.Information("file name:{0} file.Content:{1}", file.FileName, file.Content.Length);
      await File.WriteAllBytesAsync($"{appConfig.PathContent}//" + file.FileName, file.Content);
    }
  }

  await connection.DisposeAsync();
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

