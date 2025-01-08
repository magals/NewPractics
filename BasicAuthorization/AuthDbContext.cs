using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BasicAuthorization
{
  public class AuthDbContext : IdentityDbContext<User, Role, string>
  {
    public AuthDbContext() { }
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

    async public Task EnsureSeedTestData(IServiceProvider applicationServices)
    {
      UserManager<User> userManager = applicationServices.GetRequiredService<UserManager<User>>();
      RoleManager<Role> roleManager = applicationServices.GetRequiredService<RoleManager<Role>>();
      //////////////Identityies/////////////////////
      /*var roleresult = await roleManager.CreateAsync(new Role
      {
        Id = 1.t,
        Name = nameof(StaticStringsIdentityies.User),
      });
      var roleresult2 = await roleManager.CreateAsync(new Role
      {
        Id = 2,
        Name = nameof(StaticStringsIdentityies.SuperAdmin),
      });
      var roleresult3 = await roleManager.CreateAsync(new Role
      {
        Id = 3,
        Name = nameof(StaticStringsIdentityies.Vision),
      });
      */
      await SaveChangesAsync();
    }

    
  }



}


