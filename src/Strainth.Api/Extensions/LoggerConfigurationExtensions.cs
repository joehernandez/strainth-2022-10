using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Strainth.Api.Build;

namespace Strainth.Api.Extensions;

internal static class LoggerConfigurationExtensions
{
    internal static void SetupLoggerConfiguration(string appName, BuildInfo buildInfo)
    {
        Log.Logger = new LoggerConfiguration()
            .ConfigureBaseLogging(appName, buildInfo)
            .CreateLogger();
    }

    internal static LoggerConfiguration ConfigureBaseLogging(
        this LoggerConfiguration loggerConfiguration,
        string appName,
        BuildInfo buildInfo
    )
    {
        loggerConfiguration
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            // AMAZING COLOURS IN THE CONSOLE!!!!
            .WriteTo.Async(a => a.Console(theme: AnsiConsoleTheme.Code))
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            // Build information as custom properties
            .Enrich.WithProperty(nameof(buildInfo.BuildId), buildInfo.BuildId)
            .Enrich.WithProperty(nameof(buildInfo.BuildNumber), buildInfo.BuildNumber)
            .Enrich.WithProperty(nameof(buildInfo.BranchName), buildInfo.BranchName)
            .Enrich.WithProperty(nameof(buildInfo.CommitHash), buildInfo.CommitHash)
            .Enrich.WithProperty("ApplicationName", appName);

        return loggerConfiguration;
    }

    internal static LoggerConfiguration AddApplicationInsightsLogging(this LoggerConfiguration loggerConfiguration, IServiceProvider services, IConfiguration configuration)
    {
        if (!string.IsNullOrWhiteSpace(configuration.GetValue<string>("ApplicationInsights:ConnectionString")))
        {
            loggerConfiguration.WriteTo.ApplicationInsights(
                services.GetRequiredService<TelemetryConfiguration>(),
                TelemetryConverter.Traces
            );
        }

        return loggerConfiguration;
    }
}