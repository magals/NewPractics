using TemplateBackgroundService.Models;
using System.Collections.Concurrent;

namespace TemplateBackgroundService;

public class CustomLoggerProvider : ILogger, ILoggerProvider
{
    private static readonly ConcurrentQueue<LogMessage> logs = new();
    public static Action<LogMessage>? LogMessageAction;

    public CustomLoggerProvider()
    {
    }


    public static ConcurrentQueue<LogMessage> LogsQueue => logs;
    public IDisposable BeginScope<TState>(TState state) where TState : notnull => default!;


    public ILogger CreateLogger(string categoryName)
    {
        try
        {
            CustomLoggerProvider logger = new CustomLoggerProvider()
            {

            };
            return logger;
        }
        catch
        {
            throw;
        }
    }

    public void Dispose()
    {
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception, string> formatter)
    {
        if (logs.Count > 100)
        {
            logs.TryDequeue(out LogMessage? result);
        }
        if (state is not null && state?.ToString()?.IndexOf("LogMessageEntity") == -1)
        {
            var templog = new LogMessage
            {
                LogLevel = logLevel,
                DateTimeOffset = DateTimeOffset.UtcNow,
                Message = state?.ToString() ?? string.Empty,
                ThreadId = Thread.CurrentThread.ManagedThreadId
            };

            LogMessageAction?.Invoke(templog);
            logs.Enqueue(templog);
        }
    }

}
