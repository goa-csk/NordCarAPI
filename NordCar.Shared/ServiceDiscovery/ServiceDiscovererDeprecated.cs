using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Shared.ServiceDiscovery
{
    public class ServiceDiscovererDeprecated : IServiceDiscovererDeprecated
    {
        public string GetApiServiceAddress()
        {
            return ConfigurationManager.AppSettings["ApiServiceAddress"].TrimEnd('/');
        }   
    }
}
