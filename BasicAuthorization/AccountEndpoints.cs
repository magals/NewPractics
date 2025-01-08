
using Microsoft.AspNetCore.Mvc;

namespace BasicAuthorization;

public class AccountEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/accounts")
                                           .WithOpenApi();

        group.MapPost("/login", Login);
        group.MapPost("/register", Register);
    }

    public async Task<IResult> Login(IAuthService authService, [AsParameters] UserLoginDto userLoginDto)
    {
        var result = await authService.Login(userLoginDto);

        return Results.Ok(result);
    }

    public async Task<IResult> Register(IAuthService authService, [AsParameters] UserRegisterDto userRegisterDto)
    {
        var result = await authService.Register(userRegisterDto);

        return Results.Ok(result);
    }
}
