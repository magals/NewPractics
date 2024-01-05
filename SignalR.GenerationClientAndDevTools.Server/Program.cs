using SignalR.GenerationClientAndDevTools.Server;
using TypedSignalR.Client.DevTools;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();

  app.UseSignalRHubSpecification(); // <- Add!
  app.UseSignalRHubDevelopmentUI(); // <- Add!
}

app.MapHub<ChatHub>("/ChatHub");
app.MapGet("/", async (HttpContext context) =>
{
  context.Response.Redirect("/signalr-dev/index.html");
  await context.Response.CompleteAsync();
});
app.Run();
