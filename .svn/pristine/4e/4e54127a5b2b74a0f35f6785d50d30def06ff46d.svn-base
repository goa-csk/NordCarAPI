using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using NordCar.Carla.Shared.Logging.LogMessageConversion;
using log4net;
using log4net.Config;
using log4net.Util;

namespace NordCar.Carla.Shared.Logging
{
    public static class LoggerManager
    {
        //On TeamCity our loggermanager already has a configuration before we configure it.
        //For now, we use this flag to ensure we get to configure the log ourselves.
        //In the future, we could transition to a full custom log repository implementation instead of this little hack.
        private static bool _isConfiguredByUs;

        /// <summary>
        /// Creates a logger with the given <paramref name="logOrigin"/>.
        /// 
        /// Before calling this you
        /// </summary>
        /// <param name="logOrigin"></param>
        /// <returns></returns>
        public static ILogger CreateLogger(Type logOrigin)
        {
            if (!LogManager.GetRepository().Configured || !_isConfiguredByUs)
            {
                //When a logger is requested we attempt to configure Log4Net if it is not already configured.
                //This makes for an easier use by services etc - they dont have to call configure at a certain point in their lifetime.
                //If The configuration fails then we can take different steps depending on whether we are in release or debug mode.
                ConfigureLog4Net();
                _isConfiguredByUs = true;
                if (!LogManager.GetRepository().Configured)
                {
                    return HandleUnableToConfigureLog4Net();
                }
            }

            var logMessageConverter = GetLogMessageConverter();
            return new Logger(logOrigin,
                              LogManager.GetLogger(logOrigin),
                              logMessageConverter);
        }

        private static ILogMessageConverter GetLogMessageConverter()
        {
            ILogMessageConverter logMessageConverter;
            var logMessageConverterType = ConfigurationManager.AppSettings["LogMessageConverterType"];
            if (logMessageConverterType == "PlainText")
            {
                logMessageConverter = new LogMessagePlainTextConverter();
            }
            else //JSON is default case
            {
                logMessageConverter = new LogMessageJSonConverter();
            }
            return logMessageConverter;
        }

        private static ILogger HandleUnableToConfigureLog4Net()
        {
#if DEBUG
            throw new InvalidOperationException("Logger not initialized, call ConfigureLog4Net or similiar.");
#else
            return new LoggerNullObject();
#endif
        }

        /// <summary>
        /// Configures log4Net according to the log4net.config placed alongside the executing assembly.
        /// 
        /// If no config file is present then nothing will happen. 
        /// </summary>
        private static void ConfigureLog4Net()
        {

            var localPathToExecutingAssembly = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath;
            var directoryName = Path.GetDirectoryName(localPathToExecutingAssembly);

            if (directoryName == null)
            {
                return;
            }

            var completeFilePath = Path.Combine(directoryName, "log4net.config");
            var uriForConfigFile = new Uri(completeFilePath);
            var configurationFileForLog4Net = new FileInfo(uriForConfigFile.LocalPath);

            if (configurationFileForLog4Net.Exists)
            {
                XmlConfigurator.ConfigureAndWatch(configurationFileForLog4Net);

                //TODO we probably want this in production aswell - just not as a thrown exception but as somekind of fallback.
#if DEBUG
                var errors = LogManager.GetRepository().ConfigurationMessages.Cast<LogLog>();
                if (errors.Any())
                {
                    throw new InvalidOperationException("Errors encountered while configuring log4net: " + errors);
                }
#endif
            }

        }}
}
