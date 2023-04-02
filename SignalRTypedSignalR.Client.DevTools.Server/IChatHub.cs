using TypedSignalR.Client;

namespace SignalRTypedSignalR.Client.DevTools.Server;

[Hub] // <- Add!
public interface IChatHub
{
  Task EnterRoom(Guid roomId, string userName);
  Task PostMessage(string text);
  Task<Message[]> GetMessages();
}