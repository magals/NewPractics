using TypedSignalR.Client;

namespace SignalRTypedSignalR.Client.DevTools.Server;

[Receiver] // <- Add!
public interface IChatHubReceiver
{
  Task OnEnter(string userName);
  Task OnMessage(Message message);
}