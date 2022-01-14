using LoggingBestPractices;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using System.Diagnostics;

const string logFilename = "app.log";
const string seqServerUrl = @"http://localhost:5341/";

var path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName) ?? string.Empty;
var logFullPath = Path.Combine(path, logFilename);

var serilogLogger = new LoggerConfiguration()
    .Enrich()
    .WriteTo.File(logFullPath)
    .WriteTo.Seq(seqServerUrl)
    .CreateLogger();

var provider = new SerilogLoggerProvider(serilogLogger);
var logger = provider.CreateLogger(nameof(Program));

LogInterpolated();
LogException();
LogWithContext();
LogWithNullObjectPattern();

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

    using (logger.BeginScope("this log is in an {Scope} scope", "external"))
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
