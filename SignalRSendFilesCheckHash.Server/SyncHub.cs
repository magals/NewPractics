using Microsoft.AspNetCore.SignalR;
using Serilog;
using Serilog.Extensions.Hosting;
using SignalRSendFilesCheckHash.Models;

namespace SignalRSendFilesCheckHash.Server;

public class SyncHub : Hub
{
  FileInfo[] Files;
  private readonly ILogger<SyncHub> logger;

  public SyncHub(ILogger<SyncHub> logger)
  {
    DirectoryInfo directinfo = new DirectoryInfo(@"assets");
    Files = directinfo.GetFiles();
    this.logger = logger;
  }
  public async IAsyncEnumerable<FileDTO> SyncGetFileListAsync()
  {

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

  public override Task OnConnectedAsync()
  {
    logger.LogInformation("OnConnectedAsync");
    return base.OnConnectedAsync();
  }
}