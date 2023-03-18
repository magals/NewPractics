// Copyright (c) Jan Škoruba. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using Duende.AdminPanel.Admin.Api.Helpers;
using Duende.AdminPanel.Admin.Api.Middlewares;
using Duende.AdminPanel.Admin.EntityFramework.Shared.DbContexts;
using Duende.AdminPanel.Admin.EntityFramework.Shared.Entities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Duende.AdminPanel.Admin.Api.Configuration.Test
{
  public class StartupTest : Startup
  {
    public StartupTest(IWebHostEnvironment env, IConfiguration configuration) : base(env, configuration)
    {
    }

    public override void RegisterDbContexts(IServiceCollection services)
    {
      services.RegisterDbContextsStaging<AdminIdentityDbContext, IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, AdminLogDbContext, AdminAuditLogDbContext, IdentityServerDataProtectionDbContext>();
    }

    public override void RegisterAuthentication(IServiceCollection services)
    {
      services
          .AddIdentity<UserIdentity, UserIdentityRole>(options => Configuration.GetSection(nameof(IdentityOptions)).Bind(options))
          .AddEntityFrameworkStores<AdminIdentityDbContext>()
          .AddDefaultTokenProviders();

      services.AddAuthentication(options =>
      {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddCookie(JwtBearerDefaults.AuthenticationScheme);
    }

    public override void RegisterAuthorization(IServiceCollection services)
    {
      services.AddAuthorizationPolicies();
    }

    public override void UseAuthentication(IApplicationBuilder app)
    {
      app.UseAuthentication();
      app.UseMiddleware<AuthenticatedTestRequestMiddleware>();
    }
  }
}







