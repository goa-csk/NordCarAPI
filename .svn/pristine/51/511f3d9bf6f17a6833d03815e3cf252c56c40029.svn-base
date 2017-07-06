using System;

namespace NordCar.Carla.Shared.Logging.LoggingMessage
{
    public class LogExceptionMessage
    {
        public LogExceptionMessage(Exception ex)
        {
            Exception = ex.ToString();
            InnerException = ex.InnerException?.ToString();
            StackTrace = ex.StackTrace;
        }

        public string Exception { get; set; }
        public string InnerException { get; set; }
        public string StackTrace { get; set; }

        public override string ToString()
        {
            return $"\nException: {Exception}, \nInnerException: {InnerException}, \nStackTrace: {StackTrace}";
        }
    }
}
