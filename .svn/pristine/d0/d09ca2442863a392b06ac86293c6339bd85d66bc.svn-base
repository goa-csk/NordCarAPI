using System;
using NordCar.Carla.Shared.Logging.LoggingMessage;
using NordCar.Carla.Shared.Logging.LogMessageConversion;
using log4net;

namespace NordCar.Carla.Shared.Logging
{
    internal class Logger : ILogger
    {
        private readonly Type _logOrigin;
        private readonly ILog _log;
        private readonly ILogMessageConverter _logMessageConverter;

        internal Logger(Type logOrigin, ILog log, ILogMessageConverter logMessageConverter)
        {
            _logOrigin = logOrigin;
            _log = log;
            _logMessageConverter = logMessageConverter;
        }

        public void LogDebug(LogMessage message)
        {
            if (!_log.IsDebugEnabled)
            {
                return;
            }

            if (message == null)
            {
                _log.Debug("Attempt to log a null LogMessage from " + _logOrigin.FullName);
                return;
            }

            message.Origin = _logOrigin.FullName;
            message.Level = LogLevel.Debug;
            _log.Debug(_logMessageConverter.ConvertToString(message));
        }

        public void LogInfo(LogMessage message)
        {
            if (!_log.IsInfoEnabled)
            {
                return;
            }

            if (message == null)
            {
                _log.Info("Attempt to log a null LogMessage from " + _logOrigin.FullName);
                return;
            }

            message.Origin = _logOrigin.FullName;
            message.Level = LogLevel.Info;
            _log.Info(_logMessageConverter.ConvertToString(message));
        }

        public void LogWarning(LogMessage message)
        {
            if (!_log.IsWarnEnabled)
            {
                return;
            }

            if (message == null)
            {
                _log.Warn("Attempt to log a null LogMessage from " + _logOrigin.FullName);
                return;
            }

            message.Origin = _logOrigin.FullName;
            message.Level = LogLevel.Warning;
            _log.Warn(_logMessageConverter.ConvertToString(message));
        }

        public void LogError(LogMessage message)
        {
            if (!_log.IsErrorEnabled)
            {
                return;
            }

            if (message == null)
            {
                _log.Error("Attempt to log a null LogMessage from " + _logOrigin.FullName);
                return;
            }

            message.Origin = _logOrigin.FullName;
            message.Level = LogLevel.Error;
            _log.Error(_logMessageConverter.ConvertToString(message));
        }

        public void LogFatal(LogMessage message)
        {
            if (!_log.IsFatalEnabled)
            {
                return;
            }

            if (message == null)
            {
                _log.Fatal("Attempt to log a null LogMessage from " + _logOrigin.FullName);
                return;
            }

            message.Origin = _logOrigin.FullName;
            message.Level = LogLevel.Fatal;
            _log.Fatal(_logMessageConverter.ConvertToString(message));
        }

        public class x : log4net.Appender.RollingFileAppender
        {

        }
    }
}
