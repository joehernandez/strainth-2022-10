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
    CreateHostBuilder(args).Build().Run();
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