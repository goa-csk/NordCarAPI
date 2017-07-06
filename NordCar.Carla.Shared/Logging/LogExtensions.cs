using System;
using System.Collections.Generic;
using NordCar.Carla.Shared.Logging.LoggingMessage;

namespace NordCar.Carla.Shared.Logging
{
    public static class LogExtensions
    {
        public static void LogDebug(this ILogger logger, string msg)
        {
            logger.LogDebug(new LogMessage(msg));
        }

        public static void LogDebug(this ILogger logger, string msg, object obj)
        {
            logger.LogDebug(new LogMessage(msg, obj));
        }

        public static void LogDebug(this ILogger logger, string msg, IEnumerable<object> objects)
        {
            logger.LogDebug(new LogMessage(msg, objects));
        }

        public static void LogWarning(this ILogger logger, string msg)
        {
            logger.LogWarning(new LogMessage(msg));
        }

        public static void LogWarning(this ILogger logger, string msg, object obj)
        {
            logger.LogWarning(new LogMessage(msg, obj));
        }

        public static void LogWarning(this ILogger logger, string msg, IEnumerable<object> objects)
        {
            logger.LogWarning(new LogMessage(msg, objects));
        }

        public static void LogInfo(this ILogger logger, string msg)
        {
            logger.LogInfo(new LogMessage(msg));
        }

        public static void LogInfo(this ILogger logger, string msg, object obj)
        {
            logger.LogInfo(new LogMessage(msg, obj));
        }

        public static void LogInfo(this ILogger logger, string msg, IEnumerable<object> objects)
        {
            logger.LogInfo(new LogMessage(msg, objects));
        }

        public static void LogWarning(this ILogger logger, string msg, Exception e)
        {
            logger.LogWarning(new LogMessage(msg, e));
        }

        public static void LogError(this ILogger logger, string msg, Exception e)
        {
            logger.LogError(new LogMessage(msg, e));
        }

        public static void LogError(this ILogger logger, string msg)
        {
            logger.LogError(new LogMessage(msg));
        }

        public static void LogFatal(this ILogger logger, string msg, Exception e)
        {
            logger.LogFatal(new LogMessage(msg, e));
        }
    }
}
