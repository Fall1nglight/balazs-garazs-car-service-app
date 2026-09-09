namespace BalazsGarazs.Api.Data.Shared.Db.Seeder;

public class UserSeederOptions
{
    public const string SectionName = "UserSeeder";

    public required string Email { get; set; }
    public required string Username { get; set; }
}
