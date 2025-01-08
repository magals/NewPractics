using Asp.Versioning;
using Asp.Versioning.Builder;
using BasicAuthorization;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);
AppConfig appConfig = new AppConfig();
builder.Configuration.GetSection("AppConfig").Bind(appConfig);
builder.Services.AddSingleton(appConfig);
/*
builder.Services.AddDbContext<AuthDbContext>(options =>
           options.UseInMemoryDatabase("dbname"));
*/
builder.Services.AddDbContextFactory<AuthDbContext>(options =>
                options.UseNpgsql("Server=localhost;Port=5432;Userid=postgres;Password=mysecretpassword;Pooling=false;MinPoolSize=1;MaxPoolSize=20;Timeout=15;SslMode=Disable;Database=Auth", 
                npgsqlOptionsAction: sqlOptions =>
                {
                  sqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName,
                                                    AuthDbContext.NameSchema);
                  sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                  options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
#if DEBUG
                  //   options.EnableSensitiveDataLogging();
#endif
                }), ServiceLifetime.Transient);


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


var app = builder.Build().MigrateDatabase<AuthDbContext>();

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


app.Run();
