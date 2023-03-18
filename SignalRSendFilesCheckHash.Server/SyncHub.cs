using Microsoft.AspNetCore.SignalR;
using SignalRSendFilesCheckHash.Models;

namespace SignalRSendFilesCheckHash.Server;

public class SyncHub : Hub
{
  private readonly ILogger<SyncHub> logger;
  private readonly AppConfig appConfig;

  public SyncHub(ILogger<SyncHub> logger,
                 AppConfig appConfig)
  {
    this.logger = logger;
    this.appConfig = appConfig;
  }
  public async IAsyncEnumerable<FileDTO> SyncGetFileListAsync()
  {
    DirectoryInfo directinfo = new DirectoryInfo(appConfig.PathContent!);
    FileInfo[] Files = directinfo.GetFiles();
    foreach (var file in Files)
    {
      await Task.Delay(1);
      yield return new FileDTO
      {
        FileName = file.Name,
        Extension = file.Extension,
        Content = File.ReadAllBytes(file.FullName)
      };
    }
    GC.Collect();
  }

  public Task<string> CheckHashContent()
  {

    return Task.FromResult(appConfig.HashAllContent ?? string.Empty);
  }

  public override Task OnConnectedAsync()
  {
    logger.LogInformation("OnConnectedAsync");
    return base.OnConnectedAsync();
  }
}