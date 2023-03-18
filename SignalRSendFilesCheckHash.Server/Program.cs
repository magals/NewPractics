using SignalRSendFilesCheckHash.Server;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
var app = builder.Build();
app.MapHub<SyncHub>("/SyncHub");
app.Run();
