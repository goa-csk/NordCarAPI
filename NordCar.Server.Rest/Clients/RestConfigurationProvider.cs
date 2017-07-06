using NordCar.Shared.Utils;
using System;

namespace NordCar.Server.Rest.Clients
{
    public class RestClientConfigurationProvider : ConfigurationProvider
    {
       
        public TimeSpan RequestTimeout() 
        {
          return TimeSpan.FromMinutes(GetIntValue("RestClientRequestTimeout", 5));
        }

        public TimeSpan CacheTimeout()
        {
            return TimeSpan.FromMinutes(GetIntValue("RestClientCacheTimeout", 5));
        }
    }
}
