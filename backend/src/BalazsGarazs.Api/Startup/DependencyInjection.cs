using System.Text.Json.Serialization;
using BalazsGarazs.Api.Auth.Google;
using BalazsGarazs.Api.Common.ExceptionHandlers;
using BalazsGarazs.Api.Data.Shared.Database;
using BalazsGarazs.Api.Data.Shared.Database.Seeder;
using BalazsGarazs.Api.Data.Users;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Google;
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
        AddUserSeederOptions(services, configuration);
        AddPersistence(services, configuration);
        AddAuthOptions(services, configuration);
        AddAuthenticationAndAuthorization(services, configuration);
        AddExceptionHandling(services);
        AddSerilogLogging(services, configuration);
        AddValidation(services);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    private static void AddUserSeederOptions(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<UserSeederOptions>(
            configuration.GetSection(UserSeederOptions.SectionName)
        );
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(
            (serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgreSql"));
                optionsBuilder.SeedWithFirstUser(serviceProvider);
            }
        );
    }

    private static void AddAuthOptions(
        IServiceCollection services,
        IConfiguration configuration
    ) { }

    private static void AddAuthenticationAndAuthorization(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.Lockout.AllowedForNewUsers = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>();

        services
            .AddAuthentication()
            .AddGoogle(
                GoogleDefaults.AuthenticationScheme,
                options =>
                {
                    configuration.GetSection(GoogleAuthOptions.SectionName).Bind(options);
                }
            );

        services.AddAuthorization();
    }

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
