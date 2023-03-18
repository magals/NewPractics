using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SignalRSendFilesCheckHash.Models;
using System;
using System.Collections;
using System.Security.Cryptography;

namespace SignalRSendFilesCheckHash.Server.Services;

public class ReadHashContentService : BackgroundService
{
  private readonly ILogger<ReadHashContentService> logger;
  private readonly AppConfig appConfig;

  public ReadHashContentService(ILogger<ReadHashContentService> logger,
                                AppConfig appConfig)
  {
    this.logger = logger;
    this.appConfig = appConfig;
  }
  protected override Task ExecuteAsync(CancellationToken stoppingToken)
  {
    ArgumentException.ThrowIfNullOrEmpty(appConfig.PathContent);
    Task.Run(() =>
    {
      while (true) 
      {
        DirectoryInfo directinfo = new DirectoryInfo(appConfig.PathContent);
        FileInfo[] Files = directinfo.GetFiles();
        var array = new byte[16];
        Span<byte> buffer = new Span<byte>(array);
        foreach (FileInfo file in Files)
        {
          using (var md5 = MD5.Create())
          {
            using (var stream = File.OpenRead(file.FullName))
            {
              
              var asd = md5.ComputeHash(stream);
              buffer = HelpersMethods.Xor(buffer.ToArray(),asd);
            }
          }
        }

        appConfig.HashAllContent = BitConverter.ToString(buffer.ToArray());
        logger.LogInformation("Hash MD5 all Content:{0}", appConfig.HashAllContent);
        Thread.Sleep(10000);
      }
    });
    return Task.CompletedTask;
  }

  


}
