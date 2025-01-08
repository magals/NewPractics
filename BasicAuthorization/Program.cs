using Asp.Versioning;
using Asp.Versioning.Builder;
using BasicAuthorization;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
AppConfig appConfig = new AppConfig();
builder.Configuration.GetSection("AppConfig").Bind(appConfig);
builder.Services.AddSingleton(appConfig);

builder.Services.AddDbContext<AuthDbContext>(options =>
           options.UseInMemoryDatabase("dbname"));
builder
    .AddBearerAuthentication()
    .AddSwagger();
builder.Services.AddEndpoints(typeof(Program).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(options =>
{
  options.DefaultApiVersion = new ApiVersion(1);
  options.ReportApiVersions = true;
  options.AssumeDefaultVersionWhenUnspecified = true;
  options.ApiVersionReader = ApiVersionReader.Combine(
      new UrlSegmentApiVersionReader(),
      new HeaderApiVersionReader("X-Api-Version"));
}).AddApiExplorer(options =>
{
  options.GroupNameFormat = "'v'V";
  options.SubstituteApiVersionInUrl = true;
});


var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

RouteGroupBuilder versionedGroup = app
    .MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

app.MapEndpoints(versionedGroup);
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");


using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  try
  {
    var db = services.GetRequiredService<AuthDbContext>();
    db.Database.Migrate();
    if (db is AuthDbContext plsd)
    {
      //#if DEBUG
      if (args != null && (args?.Contains("-debug") ?? false))
      {
        plsd.EnsureSeedTestData(services).Wait();
      }
      //#endif
    }
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while migrating the database.");
  }
}

app.Run();
