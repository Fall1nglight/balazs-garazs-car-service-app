namespace BalazsGarazs.Api.Auth.Google;

public class GoogleAuthOptions
{
    public const string SectionName = "Authentication:Google";
    public required string ClientId { get; init; }
    public required string ClientSecret { get; init; }
}
