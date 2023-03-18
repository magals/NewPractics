using Microsoft.AspNetCore.SignalR;
using SignalRSendFilesCheckHash.Models;

namespace SignalRSendFilesCheckHash.Server;

public class SyncHub : Hub
{
  FileInfo[] Files;
  public SyncHub()
  {
    DirectoryInfo directinfo = new DirectoryInfo(@"assets");
    Files = directinfo.GetFiles();
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
    Console.WriteLine("OnConnectedAsync");
    return base.OnConnectedAsync();
  }
}