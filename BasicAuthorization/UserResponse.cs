namespace BasicAuthorization;

public class UserResponse
{
    public string Id { get; set; }
    public string[]? Roles { get; set; }
    public string? Email { get; set; }
    public string? Username { get; set; }
    public string? Phone { get; set; }
    public string? Token { get; set; }
}