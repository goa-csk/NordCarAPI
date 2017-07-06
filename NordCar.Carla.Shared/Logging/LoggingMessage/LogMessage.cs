using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NordCar.Carla.Shared.Logging.LoggingMessage
{
    public class LogMessage
    {
        public LogMessage()
        {
            Content = new List<LogDomainObjectMessage>();
            Created = System.DateTime.UtcNow; //DateTimeFactory.UtcNow;
            ThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        public LogMessage(string message)
            : this()
        {
            Message = message;
        }

        public LogMessage(string message, object domainObject)
            : this()
        {
            Message = message;
            Content.Add(new LogDomainObjectMessage(domainObject));
        }

        public LogMessage(string message, IEnumerable<object> domainObjects)
            : this()
        {
            Message = message;
            Content.AddRange(domainObjects.Where(x => x != null).Select(x => new LogDomainObjectMessage(x)));
        }

        public LogMessage(string message, Exception cause)
            : this()
        {
            Message = message;
            Cause = new LogExceptionMessage(cause);
        }

        public LogMessage(string message, Exception cause, object domainObject)
            : this()
        {
            Message = message;
            Cause = new LogExceptionMessage(cause);
            Content.Add(new LogDomainObjectMessage(domainObject));
        }

        public DateTime Created { get; internal set; }
        public string Level { get; internal set; }
        public string Origin { get; internal set; }
        public int ThreadId { get; internal set; }
        public string Message { get; set; }
        public LogExceptionMessage Cause { get; set; }
        public List<LogDomainObjectMessage> Content { get; private set; }

        public void AddDomainObject(object obj)
        {
            Content.Add(new LogDomainObjectMessage(obj));
        }

        public override string ToString()
        {
            return $"{Created}, {ThreadId}, {Level}, {OriginAsString()}, {Message}{CauseAsString()}{ContentAsString()}";
        }

        private string OriginAsString()
        {
            if (string.IsNullOrEmpty(Origin))
            {
                return "";
            }

            var names = Origin.Split('.');
            return names[names.Length - 1];
        }

        private string CauseAsString()
        {
            if (Cause == null)
            {
                return "";
            }

            return $", {Cause}";
        }

        private string ContentAsString()
        {
            var count = Content.Count;

            if (count == 0)
            {
                return "";
            }

            if (count == 1)
            {
                return $", {Content[0]}";
            }

            var builder = new StringBuilder(", ");
            foreach (var message in Content)
            {
                builder.AppendLine(message.ToString());
            }

            return builder.ToString();
        }
    }
}
