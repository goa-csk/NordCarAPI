using NordCar.Shared.ServiceDiscovery;
using System;


namespace NordCar.Shared.Rest
{
    public class BaseAddressResolver
    {
        private readonly IServiceDiscovererDeprecated _serviceDiscovererDeprecated;

        public BaseAddressResolver(IServiceDiscovererDeprecated serviceDiscovererDeprecated)
        {
            //Objects.RequireNonNull(serviceDiscovererDeprecated);
            _serviceDiscovererDeprecated = serviceDiscovererDeprecated;
        }

        public Uri ResolveBaseAddress(SupportedServices serviceToCall)
        {
            //TODO support serviceLookup
            switch (serviceToCall)
            {
                
                case SupportedServices.ApiService:
                    return new Uri(_serviceDiscovererDeprecated.GetApiServiceAddress());
               
                default:
                    throw new ArgumentOutOfRangeException(serviceToCall.ToString(), serviceToCall, null);
            }
        }
    }
}
