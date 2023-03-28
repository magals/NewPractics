using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypedSignalR.Client;

namespace SignalRGenerationClient.Shared;

public class Client : IClientContract, IHubConnectionObserver, IDisposable
{
  private readonly IHubContract _hub;
  private readonly IDisposable _subscription;
  private readonly CancellationTokenSource _cancellationTokenSource = new();

  public Client(HubConnection connection)
  {
    _hub = connection.CreateHubProxy<IHubContract>(_cancellationTokenSource.Token);
    _subscription = connection.Register<IClientContract>(this);
  }

  Task IClientContract.ReceiveMessage(string user, string message, UserDefineClass userDefine)
  {
    Console.WriteLine($"{Environment.NewLine}[Call ReceiveMessage] user: {user}, message: {message}, userDefine.RandomId: {userDefine.RandomId}");
    return Task.CompletedTask;
  }

  Task IClientContract.SomeClientMethod()
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

  public Task OnReconnecting(Exception exception)
  {
    Console.WriteLine($"[On Reconnecting!]");
    return Task.CompletedTask;
  }

  public Task<Status> SendMessage(string user, string message)
  {
    return _hub.SendMessage(user, message);
  }

  public Task SomeHubMethod()
  {
    return _hub.SomeHubMethod();
  }

  public void Dispose()
  {
    _subscription?.Dispose();
  }
}
