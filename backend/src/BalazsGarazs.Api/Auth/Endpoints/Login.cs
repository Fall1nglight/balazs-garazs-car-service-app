using BalazsGarazs.Api.Data.Employees;
using BalazsGarazs.Api.Data.Shared.Interfaces;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;

namespace BalazsGarazs.Api.Auth.Endpoints;

public class Login : IEndpoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/login", Handle).WithDescription("Login page");
    }

    private static IResult Handle(SignInManager<Employee> signInManager)
    {
        var properties = signInManager.ConfigureExternalAuthenticationProperties(
            GoogleDefaults.AuthenticationScheme,
            "/auth/google-callback"
        );

        return TypedResults.Challenge(properties, [GoogleDefaults.AuthenticationScheme]);
    }
}
