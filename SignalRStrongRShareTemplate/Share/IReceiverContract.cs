using TypedSignalR.Client;

namespace Share;

[Receiver]
public interface IReceiverContract
{
    Task ReceiveKeyCode(uint keycode);
}
