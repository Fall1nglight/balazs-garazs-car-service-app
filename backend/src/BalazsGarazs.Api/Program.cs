using BalazsGarazs.Api.Data.Employees;
using BalazsGarazs.Api.Startup;
using DotNetEnv;
using Serilog;

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
{
    Env.NoClobber().TraversePath().Load();
}

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

try
{
    Log.Information("Starting BalazsGarazs.Api");

    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddApiServices(builder.Configuration);

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddlewares();
    app.MapEndpoints();
    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
