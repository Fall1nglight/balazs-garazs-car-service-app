using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using BalazsGarazs.Api.Data.Employees;
using BalazsGarazs.Api.Data.Shared.Db;
using BalazsGarazs.Api.Data.Shared.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BalazsGarazs.Api.Auth.Endpoints;

public class GoogleLoginCallback : IEndpoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/google-callback", Handle);
    }

    private static async Task<Results<UnauthorizedHttpResult, Ok>> Handle(
        AppDbContext db,
        SignInManager<Employee> signInManager,
        UserManager<Employee> userManager,
        CancellationToken cancellationToken
    )
    {
        var loginInfo = await signInManager.GetExternalLoginInfoAsync();
        if (loginInfo == null)
            return TypedResults.Unauthorized();

        var user = await userManager.FindByLoginAsync(
            loginInfo.LoginProvider,
            loginInfo.ProviderKey
        );

        if (user == null)
        {
            var email = loginInfo.Principal.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrWhiteSpace(email))
                return TypedResults.Unauthorized();

            user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return TypedResults.Unauthorized();

            var addLoginAsync = await userManager.AddLoginAsync(user, loginInfo);
            if (!addLoginAsync.Succeeded)
                return TypedResults.Unauthorized();
        }

        var signInResult = await signInManager.ExternalLoginSignInAsync(
            loginInfo.LoginProvider,
            loginInfo.ProviderKey,
            isPersistent: false
        );

        if (!signInResult.Succeeded)
            return TypedResults.Unauthorized();

        return TypedResults.Ok();
    }
}
