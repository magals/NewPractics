using SignalR.GenerationClientAndDevTools.Server;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
var app = builder.Build();
app.MapHub<ChatHub>("/ChatHub");
app.Run();
