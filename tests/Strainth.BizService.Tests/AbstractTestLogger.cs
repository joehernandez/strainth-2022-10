using Microsoft.Extensions.Logging;

namespace Strainth.BizService.Tests;

// https://whuysentruit.medium.com/unit-testing-ilogger-in-asp-net-core-9a2d066d0fb8#:~:text=Unit-testing%20ILogger%20in%20ASP.NET%20Core%20ASP.NET%20Core%20makes,and%20how%20we%20can%20solve%20this%20with%20ease.
public abstract class AbstractTestLogger<T> : ILogger<T>
{
    public IDisposable BeginScope<TState>(TState state)
        => throw new NotImplementedException();

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        => Log(logLevel, exception, formatter(state, exception));

    public abstract void Log(LogLevel logLevel, Exception ex, string information);
}