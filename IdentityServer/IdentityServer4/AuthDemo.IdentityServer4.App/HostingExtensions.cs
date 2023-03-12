using AuthDemo.IdentityServer4.App.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace AuthDemo.IdentityServer4.App
{
	internal static class HostingExtensions
	{
		public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
		{
			// uncomment if you want to add a UI
			//builder.Services.AddRazorPages();

			builder.Services.AddIdentityServer(options =>
					{
						// https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes
						options.EmitStaticAudienceClaim = true;
					})
					.AddInMemoryIdentityResources(Config.IdentityResources)
					.AddInMemoryApiScopes(Config.ApiScopes)
					.AddInMemoryClients(Config.Clients)
					.AddClientStore<Core.ClientStore>()
					.AddClientStore<Core.ClientStore>()
					.AddResourceStore<Core.ResourceStore>(); ;

			builder.Services.AddDbContext<AuthDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDbConnection"));
			});

			return builder.Build();
		}

		public static WebApplication ConfigurePipeline(this WebApplication app)
		{
			app.UseSerilogRequestLogging();

			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			// uncomment if you want to add a UI
			//app.UseStaticFiles();
			//app.UseRouting();

			app.UseIdentityServer();

			// uncomment if you want to add a UI
			//app.UseAuthorization();
			//app.MapRazorPages().RequireAuthorization();

			return app;
		}
	}
}