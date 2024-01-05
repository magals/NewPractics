// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;
using Share;
using TypedSignalR.Client;

Console.WriteLine("Hello, World!");
var url = "http://localhost:5122/ServerHub";
HubConnection connection = new HubConnectionBuilder()
            .WithUrl(url)
            .WithAutomaticReconnect()
            .Build();


var hub = connection.CreateHubProxy<ISenderContract>();
var subscription1 = connection.Register<IReceiverContract>(new ReceiverContract());

await connection.StartAsync();
Console.ReadLine();