using System.Text.Json.Serialization;
using BalazsGarazs.Api.Common.ExceptionHandlers;
using BalazsGarazs.Api.Data.Shared.Db;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BalazsGarazs.Api.Startup;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        AddPersistence(services, configuration);
        AddAuthOptions(services, configuration);
        AddAuthenticationAndAuthorization(services);
        AddExceptionHandling(services);
        AddSerilogLogging(services, configuration);
        AddValidation(services);

        return services;
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgreSql"));
        });
    }

    private static void AddAuthOptions(
        IServiceCollection services,
        IConfiguration configuration
    ) { }

    private static void AddAuthenticationAndAuthorization(IServiceCollection services) { }

    private static void AddExceptionHandling(IServiceCollection services)
    {
        services.AddProblemDetails();

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow;
        });

        services.AddExceptionHandler<JsonExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
    }

    private static void AddSerilogLogging(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilog(
            (registeredServices, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(configuration)
                    .ReadFrom.Services(registeredServices);
            }
        );
    }

    private static void AddValidation(IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
    }
}
