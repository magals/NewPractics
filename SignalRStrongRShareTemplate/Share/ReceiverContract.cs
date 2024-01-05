using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Share;

public class ReceiverContract : IReceiverContract, TypedSignalR.Client.IHubConnectionObserver
{
    public Task OnClosed(Exception? exception)
    {
        Console.WriteLine("OnClosed");
        return Task.CompletedTask;
    }

    public Task OnReconnected(string? connectionId)
    {
        Console.WriteLine("OnReconnected");
        return Task.CompletedTask;
    }

    public Task OnReconnecting(Exception? exception)
    {
        Console.WriteLine("OnReconnecting");
        return Task.CompletedTask;
    }

    public Task ReceiveKeyCode(uint keycode)
    {
        Console.WriteLine("KeyCode:"+ keycode);
        return Task.CompletedTask;
    }
}
