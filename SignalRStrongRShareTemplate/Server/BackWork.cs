using Microsoft.AspNetCore.SignalR;
using Share;

namespace Server;

public class BackWork : BackgroundService
{
    private readonly IHubContext<ServerHub, IReceiverContract> hubContext;

    public BackWork(IHubContext<ServerHub, IReceiverContract> hubContext)
    {
        this.hubContext = hubContext;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
       
        Task.Run(() =>
        {
            int i = 1;
            while (true)
            {
                hubContext.Clients.All.ReceiveKeyCode((uint)i);
                i++;
                Thread.Sleep(1000);
            }
        });

        return Task.CompletedTask;
    }
}
