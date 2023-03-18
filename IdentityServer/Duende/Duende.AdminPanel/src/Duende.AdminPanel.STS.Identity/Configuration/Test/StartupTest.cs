// Copyright (c) Jan Škoruba. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using Duende.AdminPanel.Admin.EntityFramework.Shared.DbContexts;
using Duende.AdminPanel.STS.Identity.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Duende.AdminPanel.STS.Identity.Configuration.Test
{
  public class StartupTest : Startup
  {
    public StartupTest(IWebHostEnvironment environment, IConfiguration configuration) : base(environment, configuration)
    {
    }

    public override void RegisterDbContexts(IServiceCollection services)
    {
      services.RegisterDbContextsStaging<AdminIdentityDbContext, IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, IdentityServerDataProtectionDbContext>();
    }
  }
}







