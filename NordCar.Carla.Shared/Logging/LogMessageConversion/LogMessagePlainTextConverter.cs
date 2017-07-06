using NordCar.Carla.Shared.Logging.LoggingMessage;

namespace NordCar.Carla.Shared.Logging.LogMessageConversion
{
    internal class LogMessagePlainTextConverter : ILogMessageConverter
    {
        /// <summary>
        /// Converts the given <see cref="LogMessage"/> to a plain text string.
        /// </summary>
        public string ConvertToString(LogMessage message)
        {
            return message.ToString();
        }
    }
}
