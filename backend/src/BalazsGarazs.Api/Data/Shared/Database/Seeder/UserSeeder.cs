using BalazsGarazs.Api.Data.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BalazsGarazs.Api.Data.Shared.Database.Seeder;

public static class UserSeeder
{
    public static void SeedWithFirstUser(
        this DbContextOptionsBuilder optionsBuilder,
        IServiceProvider serviceProvider
    )
    {
        optionsBuilder.UseSeeding(
            (context, _) =>
                SeedAsync(context, serviceProvider, CancellationToken.None).GetAwaiter().GetResult()
        );

        optionsBuilder.UseAsyncSeeding(
            (context, _, cancellationToken) =>
                SeedAsync(context, serviceProvider, cancellationToken)
        );
    }

    private static async Task SeedAsync(
        DbContext context,
        IServiceProvider serviceProvider,
        CancellationToken cancellationToken
    )
    {
        AppDbContext db = (AppDbContext)context;

        await using AsyncServiceScope scope = serviceProvider.CreateAsyncScope();

        int numOfEmployees = await db.Users.CountAsync(cancellationToken);
        if (numOfEmployees > 0)
            return;

        UserSeederOptions config = scope
            .ServiceProvider.GetRequiredService<IOptions<UserSeederOptions>>()
            .Value;

        User initialUser = new User()
        {
            Email = config.Email,
            UserName = config.Email,
            CreatedAt = DateTime.UtcNow,
            IsActive = true,
        };

        UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<
            UserManager<User>
        >();

        IdentityResult createResult = await userManager.CreateAsync(initialUser);

        if (!createResult.Succeeded)
        {
            string errors = string.Join(
                "; ",
                createResult.Errors.Select(error => error.Description)
            );
            throw new InvalidOperationException($"Could not seed the initial employee: {errors}");
        }
    }
}
