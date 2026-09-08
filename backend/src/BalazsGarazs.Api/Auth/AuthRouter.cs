using BalazsGarazs.Api.Auth.Endpoints;
using BalazsGarazs.Api.Common.Extensions;
using BalazsGarazs.Api.Data.Shared.Interfaces;

namespace BalazsGarazs.Api.Auth;

public class AuthRouter : IEndpointRouter
{
    public static void MapRouter(IEndpointRouteBuilder builder)
    {
        var anonAuthRoutes = builder.MapGroup("/auth").AllowAnonymous();
        anonAuthRoutes.MapEndpoint<Login>();
        anonAuthRoutes.MapEndpoint<GoogleLoginCallback>();
    }
}
