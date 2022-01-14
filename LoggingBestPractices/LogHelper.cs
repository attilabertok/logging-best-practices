using Collector.Serilog.Enrichers.Assembly;
using Serilog;

namespace LoggingBestPractices;

public static class LogHelper
{
    public static LoggerConfiguration Enrich(this LoggerConfiguration configuration)
    {
        return configuration
            .Enrich.With(new SourceSystemEnricher(typeof(Program).Assembly))
            .Enrich.With(new SourceSystemInformationalVersionEnricher(typeof(Program).Assembly))
            .Enrich.WithEnvironmentName()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentUserName()
            .Enrich.FromLogContext()
            .Enrich.WithProperty("CustomProperty", "CustomValue");
    }
}
