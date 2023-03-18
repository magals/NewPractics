// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;
using SignalRSendFilesCheckHash.Models;

Console.WriteLine("Hello, World!");


try
{

  HubConnection connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5152" + "/SyncHub")
                .Build();
  connection.StartAsync().Wait();

  Directory.CreateDirectory("assets");

  Task.Run(async () =>
  {
    while (true)
    {
      await foreach (var file in connection.StreamAsync<FileDTO>("SyncGetFileListAsync"))
      {
        Console.WriteLine("file name:{0} file.Content:{1}", file.FileName, file.Content.Length);
        await File.WriteAllBytesAsync("assets//" + file.FileName, file.Content);
      }
      GC.Collect();
    }
  });

  Console.WriteLine(value: "End");
  Console.Read();

}
catch (Exception e)
{
  Console.WriteLine("Error" + e.Message);
}