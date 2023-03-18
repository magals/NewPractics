using Microsoft.AspNetCore.SignalR;

namespace SimpleSignalRServer;

public class ChatHub : Hub
{
  public async Task SendMessage(string user, string message)
  {
    Console.WriteLine(user+ " "+message);
    await Clients.All.SendAsync("ReceiveMessage", user, message);
  }

  public override Task OnConnectedAsync()
  {
    Console.WriteLine("OnConnectedAsync");
    return base.OnConnectedAsync();
  }
}
