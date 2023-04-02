namespace SignalRTypedSignalR.Client.DevTools.Server;

public record Message(Guid UserId, string UserName, string Text, DateTime DateTime);