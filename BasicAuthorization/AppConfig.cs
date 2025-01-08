namespace BasicAuthorization;

public class AppConfig
{

    public AuthenticationClass? Authentication { get; set; }

    public class AuthenticationClass
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string TokenPrivateKey { get; set; } = string.Empty;
        public int ExpireIntervalMinutes { get; set; }
    }
}
