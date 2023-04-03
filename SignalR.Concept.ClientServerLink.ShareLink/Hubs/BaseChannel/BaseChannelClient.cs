using Microsoft.AspNetCore.SignalR.Client;

namespace SignalR.Concept.ClientServerLink.ShareLink.Hubs.BaseChannel;

public class BaseChannelClient : IBaseChannelClient_Receiver, IHubConnectionObserver, IDisposable
{
  private readonly IBaseChannelClient_Transmiser _hub;
  private readonly IDisposable _subscription;
  private readonly CancellationTokenSource _cancellationTokenSource = new();

  public BaseChannelClient(HubConnection connection)
  {
    _hub = connection.CreateHubProxy<IBaseChannelClient_Transmiser>(_cancellationTokenSource.Token);
    _subscription = connection.Register<IBaseChannelClient_Receiver>(this);
  }

  public void Dispose()
  {
    throw new NotImplementedException();
  }

  public Task OnClosed(Exception? exception)
  {
    throw new NotImplementedException();
  }

  public Task OnReconnected(string? connectionId)
  {
    throw new NotImplementedException();
  }

  public Task OnReconnecting(Exception? exception)
  {
    throw new NotImplementedException();
  }
}
