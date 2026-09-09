using System.Security.Claims;
using BalazsGarazs.Api.Data.Shared.Database;
using BalazsGarazs.Api.Data.Shared.Interfaces;
using BalazsGarazs.Api.Data.Users;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace BalazsGarazs.Api.Auth.Endpoints;

public class GoogleLoginCallback : IEndpoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/google-callback", Handle);
    }

    private static async Task<Results<UnauthorizedHttpResult, Ok>> Handle(
        AppDbContext db,
        SignInManager<User> signInManager,
        UserManager<User> userManager,
        CancellationToken cancellationToken
    )
    {
        ExternalLoginInfo? loginInfo = await signInManager.GetExternalLoginInfoAsync();
        if (loginInfo == null)
            return TypedResults.Unauthorized();

        User? user = await userManager.FindByLoginAsync(
            loginInfo.LoginProvider,
            loginInfo.ProviderKey
        );

        if (user == null)
        {
            string? email = loginInfo.Principal.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrWhiteSpace(email))
                return TypedResults.Unauthorized();

            user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return TypedResults.Unauthorized();

            IdentityResult addLoginResult = await userManager.AddLoginAsync(user, loginInfo);
            if (!addLoginResult.Succeeded)
                return TypedResults.Unauthorized();
        }

        SignInResult signInResult = await signInManager.ExternalLoginSignInAsync(
            loginInfo.LoginProvider,
            loginInfo.ProviderKey,
            isPersistent: false
        );

        if (!signInResult.Succeeded)
            return TypedResults.Unauthorized();

        return TypedResults.Ok();
    }
}
