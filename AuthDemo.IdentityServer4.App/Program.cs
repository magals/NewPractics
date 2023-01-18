using AuthDemo.IdentityServer4.App;
using Serilog;
//https://www.learmoreseekmore.com/2021/07/identityserver4-protect-api-with-clientcredentials-implementing-iclientstore-and-iresourcestore.html
Log.Logger = new LoggerConfiguration()
		.WriteTo.Console()
		.CreateBootstrapLogger();

Log.Information("Starting up");

try
{
	var builder = WebApplication.CreateBuilder(args);

	builder.Host.UseSerilog((ctx, lc) => lc
			.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
			.Enrich.FromLogContext()
			.ReadFrom.Configuration(ctx.Configuration));

	var app = builder
			.ConfigureServices()
			.ConfigurePipeline();

	app.Run();
}
catch (Exception ex)
{
	Log.Fatal(ex, "Unhandled exception");
}
finally
{
	Log.Information("Shut down complete");
	Log.CloseAndFlush();
}