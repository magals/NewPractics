using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFeatureManagement();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("articles/{id}", async (
    Guid id,
    IGetArticle query,
    IFeatureManager featureManager) =>
{
  var article = query.Execute(id);

  if (await featureManager.IsEnabledAsync(FeatureFlags.ClipArticleContent))
  {
    article.Content = article.Content.Substring(0, 50);
  }

  return Results.Ok(article);
});
app.Run();
