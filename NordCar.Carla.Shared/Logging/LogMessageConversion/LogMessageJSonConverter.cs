using System;
using NordCar.Carla.Shared.Logging.LoggingMessage;
using Newtonsoft.Json;

namespace NordCar.Carla.Shared.Logging.LogMessageConversion
{
    internal class LogMessageJSonConverter : ILogMessageConverter
    {
        /// <summary>
        /// Converts the given <see cref="LogMessage"/> to a JSON string.
        /// </summary>
        public string ConvertToString(LogMessage message)
        {
            try
            {
                return JsonConvert.SerializeObject(message);
            }
            // ReSharper disable once UnusedVariable
            catch (JsonSerializationException e)
            {
#if DEBUG
                throw new ArgumentException("The given message could not be serialized.", e);
#else
                return AttemptConvertAfterFailedSerialization(message);
#endif
            }
        }

        /// <summary>
        /// If a  <see cref="LogMessage"/> fails to serialize to JSon then this message can be used to get as much as possible information from the message.
        /// 
        /// Firstly an attempt is made to convert the message without  <see cref="LogMessage.Content"/> if this fails then a simple string  representation is made.
        /// 
        /// </summary>
        private string AttemptConvertAfterFailedSerialization(LogMessage originalMessage)
        {
            try
            {
                var safeLogMessage = new LogMessage
                {
                    Message = originalMessage.Message,
                    Cause = originalMessage.Cause,
                    Created = originalMessage.Created
                };

                foreach (var domainObject in originalMessage.Content)
                {
                    //call to string here to attempt to avoid serialization errors
                    safeLogMessage.AddDomainObject(domainObject.ToString());
                }

                return JsonConvert.SerializeObject(safeLogMessage);
            }
            catch (JsonSerializationException e)
            {
                var originalCause = originalMessage.Cause?.ToString() ?? "no cause specified in LogMessage";
                return "Failed to serialize LogMessage with message: " + originalMessage.Message + " and cause " + originalCause + ". SerializationException: " + e;
            }
        }
    }
}
