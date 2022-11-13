using Serilog;
using Strainth.Api;
using Strainth.Api.Build;
using Strainth.Api.Extensions;
using Strainth.Api.Middleware;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting up web host");
    // var builder = WebApplication.CreateBuilder(args);
    CreateHostBuilder(args).Build().Run();

    // builder.Host.UseSerilog((hostBuilderContext, services, loggerConfiguration) =>
    // {
    //     loggerConfiguration.ConfigureBaseLogging(appName, AppVersionInfo.GetBuildInfo());
    //     loggerConfiguration.AddApplicationInsightsLogging(services, hostBuilderContext.Configuration);
    // });
    // builder.Host.UseSerilog((context, lc) => lc
    //     .ReadFrom.Configuration(context.Configuration)
    //     .Enrich.FromLogContext()
    //     .WriteTo.Console());
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.CloseAndFlush();
}

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .UseSerilog((hostBuilderContext, services, loggerConfiguration) =>
        {
            loggerConfiguration.ConfigureBaseLogging("Strainth.Api", AppVersionInfo.GetBuildInfo());
            loggerConfiguration.AddApplicationInsightsLogging(services, hostBuilderContext.Configuration);
        })
        .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
}