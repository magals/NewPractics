// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using SignalRSendFilesCheckHash.Models;
try
{

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
        Console.WriteLine("file name:{0} file.Content:{1}", file.FileName, file.Content.Length);
        await File.WriteAllBytesAsync("assets//" + file.FileName, file.Content);
      }
      GC.Collect();
    await connection.DisposeAsync();
  }).Wait();

}
catch (Exception e)
{
  Console.WriteLine("Error" + e.Message);
}