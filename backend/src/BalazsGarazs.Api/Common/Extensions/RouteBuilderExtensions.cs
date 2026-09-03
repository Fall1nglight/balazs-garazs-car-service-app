using BalazsGarazs.Api.Common.Filters;
using BalazsGarazs.Api.Data.Shared.Interfaces;

namespace BalazsGarazs.Api.Common.Extensions;

public static class RouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder builder)
        where TEndpoint : class, IEndpoint
    {
        TEndpoint.Map(builder);
        return builder;
    }

    public static RouteHandlerBuilder WithValidation<TRequest>(this RouteHandlerBuilder builder)
    {
        builder.AddEndpointFilter<RequestValidationFilter<TRequest>>().ProducesValidationProblem();
        return builder;
    }
}
