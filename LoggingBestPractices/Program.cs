using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using System.Diagnostics;
using LoggingBestPractices.Infrastructure;
using LoggingBestPractices.Infrastructure.ThingService;
using LoggingBestPractices.Patterns;
using LoggerExtensions = LoggingBestPractices.Infrastructure.LoggerExtensions;

const string logFilename = "app.log";
const string seqServerUrl = @"http://localhost:5341/";

var path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName) ?? string.Empty;
var logFullPath = Path.Combine(path, logFilename);

var serilogLogger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich()
    .WriteTo.File(logFullPath)
    .WriteTo.Seq(seqServerUrl)
    .CreateLogger();

var provider = new SerilogLoggerProvider(serilogLogger);
var factory = new LoggerFactory(new[] {provider});
var logger = provider.CreateLogger(nameof(Program));

LogInterpolated();
LogException();
LogWithContext();
LogWithNullObjectPattern();
LogCategory();
LogEventType();
LogAndRethrow();
LogSampled();
LogWithDecorator();

serilogLogger.Dispose();

void LogInterpolated()
{
    const string logMessage = "log message";
    const string otherLogMessage = "other log message";
    var semanticLogging = new SemanticLogging(logger);

    semanticLogging.LogInterpolated(logMessage);
    semanticLogging.LogInterpolated(otherLogMessage);
}

void LogException()
{
    var exceptionLogging = new ExceptionLogging(logger);
    exceptionLogging.LogException();
}

void LogWithContext()
{
    var loggerScopes = new LoggerScopes(logger);

    using (logger.BeginScope("this log is in an {AmbientScope} scope", "external"))
    {
        loggerScopes.LogWithContext();
    }
}

void LogWithNullObjectPattern()
{
    var nullObjectPattern1 = new NullObjectPattern(logger);
    var nullObjectPattern2 = new NullObjectPattern();

    nullObjectPattern1.Log("the logger was passed in");
    nullObjectPattern2.Log("no logger was passed in");
}

void LogCategory()
{
    var genericTypeCategory = new GenericTypeCategory(logger, factory.CreateLogger<GenericTypeCategory>());

    genericTypeCategory.Log();
}

void LogEventType()
{
    LoggerExtensions.Init();

    var eventType = new EventTypes(logger, serilogLogger);

    eventType.Log(Guid.NewGuid(), "old@email.com", "new@email.com");

    //eventType.TestPerformance(Guid.NewGuid(), "old@email.com", "new@email.com", 100);
}

void LogAndRethrow()
{
    var logAndRethrowAntipattern = new LogAndRethrowAntipattern(logger);

    logAndRethrowAntipattern.LogAndRethrow();
    logAndRethrowAntipattern.EvenWorse();
}

void LogSampled()
{
    var logSampled = new Sampling(logger);

    logSampled.LogCriticalPath();
    logSampled.ProcessAndSample();
}

void LogWithDecorator()
{
    var thingService = new ThingService();
    var decoratedThingService = new LoggingThingService(thingService, serilogLogger);
    var decoratorPattern = new DecoratorPattern(decoratedThingService);

    decoratorPattern.ManipulateThings();
}
