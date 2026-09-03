namespace BalazsGarazs.Api.Data.Shared.Interfaces;

public interface IEndpointRouter
{
    static abstract void MapRouter(IEndpointRouteBuilder builder);
}
