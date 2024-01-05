using Server;
using TypedSignalR.Client.DevTools;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddHostedService<BackWork>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseSignalRHubSpecification();
app.UseSignalRHubDevelopmentUI();

app.MapHub<ServerHub>("/ServerHub");
app.MapGet("/", async (HttpContext context) =>
{
    context.Response.Redirect("/signalr-dev/index.html");
    await context.Response.CompleteAsync();
});
app.Run();
