using Microsoft.Extensions.Logging;

namespace LoggingBestPractices.Patterns;

public class LogAndRethrowAntipattern
{
    private readonly ILogger _logger;

    public class InnerClass
    {
        private readonly ILogger _logger;

        public InnerClass(ILogger logger)
        {
            _logger = logger;
        }

        public void LogAndRethrow()
        {
            try
            {
                throw new FileNotFoundException("Could not find the file", "nonExisting.file");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "File not found");
                throw;
            }
        }

        public void EvenWorse()
        {
            try
            {
                throw new FileNotFoundException("Could not find the file", "nonExisting.file");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "File not found");
                throw new Exception("Do not do this, unless you are explicitly trying to hide the exception from the user.");
            }
        }
    }

    public LogAndRethrowAntipattern(ILogger logger)
    {
        _logger = logger;
    }

    public void LogAndRethrow()
    {
        var innerClass = new InnerClass(_logger);

        try
        {
            innerClass.LogAndRethrow();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "InnerClass threw an exception");
        }
    }

    public void EvenWorse()
    {
        var innerClass = new InnerClass(_logger);

        try
        {
            innerClass.EvenWorse();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "InnerClass threw an exception");
        }
    }
}
