using Microsoft.AspNetCore.SignalR;
using SignalR.GenerationClientAndDevTools.Shared;

namespace SignalR.GenerationClientAndDevTools.Server;


public class ChatHub : Hub<IClientContract>, IHubContract
{
  public async Task<Status> SendMessage(string user, string message)
  {
    var userDefine = new UserDefineClass() { Datetime = DateTime.Now, RandomId = Guid.NewGuid() };
    await Clients.All.ReceiveMessage(user, message, userDefine);
    return new Status() { StatusMessage = $"[Success] Call SendMessage : {userDefine.Datetime}, {userDefine.RandomId}" };
  }

  public async Task SomeHubMethod()
  {
    await Clients.Caller.SomeClientMethod();
  }

  public override Task OnConnectedAsync()
  {
    Console.WriteLine("OnConnectedAsync");
    return base.OnConnectedAsync();
  }
}
