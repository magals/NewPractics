using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BasicAuthorization;

public class AuthService(AppConfig authOptions,
    UserManager<User> userManager,
    RoleManager<Role> roleManager) : IAuthService
{
  private readonly AppConfig _authOptions = authOptions;

  public async Task<UserResponse> Register(UserRegisterDto userRegisterDto)
  {
    if (await userManager.FindByEmailAsync(userRegisterDto.Email) != null)
    {
      throw new DuplicateEntityException($"Email {userRegisterDto.Email} already exists");
    }

    var createUserResult = await userManager.CreateAsync(new User
    {
      Id = "asd",
      Email = userRegisterDto.Email,
      PhoneNumber = userRegisterDto.Phone,
      UserName = userRegisterDto.Username
    }, userRegisterDto.Password);

    if (createUserResult.Succeeded)
    {
      var user = await userManager.FindByNameAsync(userRegisterDto.Username);

      if (user == null)
      {
        throw new EntityNotFoundException($"User with email {userRegisterDto.Email} not registered");
      }

      var roleresult = await roleManager.CreateAsync(new Role
      {
        Id = 1.ToString(),
        Name = nameof(StaticStringsIdentityies.User),
      });


      var result = await userManager.AddToRoleAsync(user, StaticStringsIdentityies.User);

      if (result.Succeeded)
      {
        var response = new UserResponse
        {
          Id = user.Id,
          Email = user.Email,
          Roles = [StaticStringsIdentityies.User],
          Username = user.UserName,
          Phone = user.PhoneNumber
        };
        return GenerateToken(response);
      }


      throw new Exception($"Errors: {string.Join(";", result.Errors
          .Select(x => $"{x.Code} {x.Description}"))}");
    }

    throw new Exception();
  }

  public async Task<UserResponse> Login(UserLoginDto userLoginDto)
  {
    var user = await userManager.FindByEmailAsync(userLoginDto.Email);

    if (user == null)
    {
      throw new EntityNotFoundException($"User with email {userLoginDto.Email} not found");
    }

    var checkPasswordResult = await userManager.CheckPasswordAsync(user, userLoginDto.Password);

    if (checkPasswordResult)
    {
      var userRoles = await userManager.GetRolesAsync(user);
      var response = new UserResponse
      {
        Id = user.Id,
        Email = user.Email,
        Roles = userRoles.ToArray(),
        Username = user.UserName,
        Phone = user.PhoneNumber
      };
      return GenerateToken(response);
    }

    throw new AuthenticationException();
  }

  public UserResponse GenerateToken(UserResponse userRegisterModel)
  {
    var handler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_authOptions.Authentication.TokenPrivateKey);
    var credentials = new SigningCredentials(
        new SymmetricSecurityKey(key),
        SecurityAlgorithms.HmacSha256Signature);

    var claims = new Dictionary<string, object>
        {
            {ClaimTypes.Name, userRegisterModel.Email!},
            {ClaimTypes.NameIdentifier, userRegisterModel.Id.ToString()},
            {JwtRegisteredClaimNames.Aud, _authOptions.Authentication.Audience},
            {JwtRegisteredClaimNames.Iss, _authOptions.Authentication.Issuer}
        };
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = GenerateClaims(userRegisterModel, _authOptions.Authentication.Audience, _authOptions.Authentication.Issuer),
      Expires = DateTime.UtcNow.AddMinutes(_authOptions.Authentication.ExpireIntervalMinutes),
      SigningCredentials = credentials,
      Claims = claims,
      Audience = _authOptions.Authentication.Audience,
      Issuer = _authOptions.Authentication.Issuer
    };

    var token = handler.CreateToken(tokenDescriptor);
    userRegisterModel.Token = handler.WriteToken(token);

    return userRegisterModel;
  }

  private static ClaimsIdentity GenerateClaims(UserResponse userRegisterModel, string audience, string issuer)
  {
    var claims = new ClaimsIdentity();
    claims.AddClaim(new Claim(ClaimTypes.Name, userRegisterModel.Email!));
    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userRegisterModel.Id.ToString()));
    claims.AddClaim(new Claim(JwtRegisteredClaimNames.Aud, audience));
    claims.AddClaim(new Claim(JwtRegisteredClaimNames.Iss, issuer));

    foreach (var role in userRegisterModel.Roles!)
      claims.AddClaim(new Claim(ClaimTypes.Role, role));

    return claims;
  }
}

public class DuplicateEntityException(string? message = null) : Exception(message)
{

}

public class EntityNotFoundException(string message) : Exception(message)
{
}