using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors;

public static class LoggingBehavior
{
    public static readonly Action<ILogger, string, string, object, Exception?> LogStartHandle =
        LoggerMessage.Define<string, string, object>(
            LogLevel.Information,
            new EventId(1, "StartHandle"),
            "[START] Handle Request={RequestType} - Response={ResponseType} - RequestData={@Request}");

    public static readonly Action<ILogger, string, int, Exception?> LogPerformance =
        LoggerMessage.Define<string, int>(
            LogLevel.Warning,
            new EventId(2, "Performance"),
            "[PERFORMANCE] The request {RequestType} took {TimeTaken}s");

    public static readonly Action<ILogger, string, string, Exception?> LogEndHandle =
        LoggerMessage.Define<string, string>(
            LogLevel.Information,
            new EventId(3, "EndHandle"),
            "[END] Handled {RequestType} with {ResponseType}");
}

public sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        LoggingBehavior.LogStartHandle(_logger, typeof(TRequest).Name, typeof(TResponse).Name, request, null);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next(cancellationToken);

        timer.Stop();
        var elapsed = timer.Elapsed;

        if (elapsed.TotalSeconds > 3)
        {
            LoggingBehavior.LogPerformance(_logger, typeof(TRequest).Name, (int)elapsed.TotalSeconds, null);
        }

        LoggingBehavior.LogEndHandle(_logger, typeof(TRequest).Name, typeof(TResponse).Name, null);

        return response;
    }
}