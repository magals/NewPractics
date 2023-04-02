using SignalRTypedSignalR.Client.DevTools.Server;
using TypedSignalR.Client.DevTools;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddSingleton<IMessageRepository, MessageRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();

  app.UseSignalRHubSpecification(); // <- Add!
  app.UseSignalRHubDevelopmentUI(); // <- Add!
}

app.MapHub<ChatHub>("/hubs/ChatHub"); // <- Add MapHub as usual.
app.MapGet("/", async (HttpContext context) =>
{
  context.Response.Redirect("/signalr-dev/index.html");
  await context.Response.CompleteAsync();
});
app.Run();