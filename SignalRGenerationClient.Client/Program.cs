// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;
using SignalRGenerationClient.Shared;
using System;
using TypedSignalR.Client;


var url = "http://localhost:5017/ChatHub";
HubConnection connection = new HubConnectionBuilder()
            .WithUrl(url)
            .WithAutomaticReconnect()
            .Build();

await Sample1(connection);

Console.ReadLine();



static async Task Sample1(HubConnection connection)
{
  var hub = connection.CreateHubProxy<IHubContract>();
  var subsc = connection.Register<IClientContract>(new Receiver());

  await connection.StartAsync();

  while (true)
  {
    Console.Write("UserName: ");
    var user = Console.ReadLine();

    Console.Write("Message: ");
    var message = Console.ReadLine();

    if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(message))
    {
      break;
    }

    Console.WriteLine($"[Invoke SendMessage]");
    var status = await hub.SendMessage(user, message);

    Console.WriteLine($"[Return status] {status.StatusMessage}");

    await Task.Delay(TimeSpan.FromSeconds(2));
  }

  Console.WriteLine($"[Invoke SomeHubMethod]");
  await hub.SomeHubMethod();

  await connection.StopAsync();
  subsc.Dispose();
  await Task.Delay(TimeSpan.FromSeconds(1));
}

static async Task Sample2(HubConnection connection)
{
  var client = new Client(connection);

  await connection.StartAsync();

  while (true)
  {
    Console.Write("UserName: ");
    var user = Console.ReadLine();

    Console.Write("Message: ");
    var message = Console.ReadLine();

    if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(message))
    {
      break;
    }

    Console.WriteLine($"[Invoke SendMessage]");
    var status = await client.SendMessage(user, message);

    Console.WriteLine($"[Return status] {status.StatusMessage}");

    await Task.Delay(TimeSpan.FromSeconds(2));
  }

  Console.WriteLine($"[Invoke SomeHubMethod]");
  await client.SomeHubMethod();

  await connection.StopAsync();
  client.Dispose();

  await Task.Delay(TimeSpan.FromSeconds(1));
}