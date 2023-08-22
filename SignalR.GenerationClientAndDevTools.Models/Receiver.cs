

namespace SignalR.GenerationClientAndDevTools.Shared;

public class Receiver : IClientContract, TypedSignalR.Client.IHubConnectionObserver
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

  public Task OnClosed(Exception? e)
  {
    Console.WriteLine($"[On Closed!]");
    return Task.CompletedTask;
  }

  public Task OnReconnected(string? connectionId)
  {
    Console.WriteLine($"[On Reconnected!]");
    return Task.CompletedTask;
  }

  public Task OnReconnecting(Exception? e)
  {
    Console.WriteLine($"[On Reconnecting!]");
    return Task.CompletedTask;
  }
}
