using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

namespace ExampleFeatureFlags
{
  [FeatureGate(FeatureFlags.ClipArticleContent)]
  public class ArticlesController : Controller
  {
  }
}
