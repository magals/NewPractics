using TypedSignalR.Client;

namespace Share;

[Hub]
public interface ISenderContract
{
    Task SendKeyCode(uint keycode);
}