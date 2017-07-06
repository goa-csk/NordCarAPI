using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Carla.Shared.Logging.LoggingMessage
{
    public class LogDomainObjectMessage
    {
        public LogDomainObjectMessage(object domainObject)
        {
            Type = domainObject?.GetType().Name;
            Object = domainObject;
        }

        public string Type { get; set; }
        public object Object { get; set; }

        public override string ToString()
        {
            return $"Content = {Type}: {Object}";
        }
    }
}
