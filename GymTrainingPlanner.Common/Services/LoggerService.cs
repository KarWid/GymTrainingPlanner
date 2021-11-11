namespace GymTrainingPlanner.Common.Services
{
    using System;
    using Microsoft.AspNetCore.Http;
    using NLog;
    using GymTrainingPlanner.Common.Exceptions;

    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(HttpContext httpContext, Exception ex);
    }

    public class LoggerService : ILoggerService
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();
        
        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }
        
        public void LogError(HttpContext httpContext, Exception ex)
        {
            var description = GetHttpContextDescription(httpContext);

            if (ex is BaseApiException baseApiException)
            {
                _logger.Error(baseApiException.GetErrorMessage(), description);
                return;
            }

            _logger.Error(ex, description);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogWarn(string message)
        {
            _logger.Warn(message);
        }

        private string GetHttpContextDescription(HttpContext httpContext)
        {
            if (httpContext?.Request == null)
            {
                return string.Empty;
            }

            return $"{httpContext.Request.Path} - {httpContext.Request.Method}";
        }
    }
}
