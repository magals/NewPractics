namespace BasicAuthorization;

public interface IAuthService
{
    Task<UserResponse> Register(UserRegisterDto userRegisterModel);
    Task<UserResponse> Login(UserLoginDto userLoginDto);
}