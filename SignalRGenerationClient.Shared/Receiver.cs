using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypedSignalR.Client;

namespace SignalRGenerationClient.Shared;

public class Receiver : IClientContract, IHubConnectionObserver
{
  public Task ReceiveMessage(string user, string message, UserDefineClass userDefine)
  {
    Console.WriteLine($"{Environment.NewLine}[Call ReceiveMessage] user: {user}, message: {message}, userDefine.RandomId: {userDefine.RandomId}");
    return Task.CompletedTask;
  }

  public Task SomeClientMethod()
  {
    Console.WriteLine($"{Environment.NewLine}[Call SomeClientMethod]");

    return Task.CompletedTask;
  }

  public Task OnClosed(Exception e)
  {
    Console.WriteLine($"[On Closed!]");
    return Task.CompletedTask;
  }

  public Task OnReconnected(string connectionId)
  {
    Console.WriteLine($"[On Reconnected!]");
    return Task.CompletedTask;
  }

  public Task OnReconnecting(Exception e)
  {
    Console.WriteLine($"[On Reconnecting!]");
    return Task.CompletedTask;
  }
}
