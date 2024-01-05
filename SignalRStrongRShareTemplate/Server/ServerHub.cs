using Microsoft.AspNetCore.SignalR;
using Share;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Server;

public class ServerHub : Hub<IReceiverContract>, ISenderContract
{
    public async Task SendKeyCode(uint keycode)
    {
        await this.Clients.All.ReceiveKeyCode(keycode);
    }

    public override Task OnConnectedAsync()
    {
        Console.WriteLine("OnConnectedAsync");
        return base.OnConnectedAsync();
    }
}
