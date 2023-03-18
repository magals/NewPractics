// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;
using System.Data;

try
{
  HubConnection connection= new HubConnectionBuilder()
                .WithUrl("http://localhost:5174" + "/ChatHub")
                .Build();
  connection.StartAsync().Wait();
  connection.On<string, string>("ReceiveMessage", (user, trt) =>
  {
    Console.WriteLine(user + " " + trt);
  });

  connection.InvokeAsync("SendMessage", "Max", "test");
  Console.Read();
}
catch (Exception e)
{
  Console.WriteLine("Error" + e.Message);
}