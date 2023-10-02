using Microsoft.AspNetCore.Http;
using Serilog;
using System.Security.Claims;

namespace TemplateBackgroundService.Models;

public class HttpContextInfo
{
    public required string IpAddress { get; set; }
    public required string Host { get; set; }
    public required string Protocol { get; set; }
    public required string Scheme { get; set; }
    public required string User { get; set; }
}

public static class Enricher
{
    internal static void HttpRequestEnricher(IDiagnosticContext diagnosticContext, HttpContext httpContext)
    {
        var httpContextInfo = new HttpContextInfo
        {
            Protocol = httpContext.Request.Protocol,
            Scheme = httpContext.Request.Scheme,
            IpAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Null",
            Host = httpContext.Request.Host.ToString(),
            User = GetUserInfo(httpContext.User)
        };

        diagnosticContext.Set("HttpContext", httpContextInfo, true);
    }

    private static string GetUserInfo(ClaimsPrincipal user)
    {
        if (user.Identity != null && user.Identity.IsAuthenticated && !string.IsNullOrEmpty(user.Identity.Name))
        {
            return user.Identity.Name;
        }
        return Environment.UserName;
    }
}
