using Microsoft.AspNetCore.Builder;
using WebLoadEmbeddedDllFromResources;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => "Hello World!");

EmbeddedDllClass.ExtractEmbeddedDlls("plcommpro.dll", WebLoadEmbeddedDllFromResources.Properties.Resources.plcommpro);
EmbeddedDllClass.LoadDll("plcommpro.dll");

var asd = ExternMethods.Connect("");

app.Run();
