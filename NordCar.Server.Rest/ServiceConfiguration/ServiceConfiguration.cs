using NordCar.Server.Rest.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Server.Rest.ServiceConfiguration
{
    public abstract class ServiceConfiguration : IDisposable, IServiceConfiguration
    {
        //private readonly ILogger _logger = LoggerManager.CreateLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IServiceRegistryClient _serviceRegistryClient;
        private List<RegisterServiceRequest> _services;
        private static readonly ServiceConfigurationProvider ConfigurationProvider = new ServiceConfigurationProvider();
        private readonly int _initialRetryDelay;
        private readonly int _retryCount;

        public const string DefaultServiceScheme = "http";
        public const string DefaultServiceHost = "localhost";

        public const string DefaultServiceRegistryPort = "4000";
        public const string DefaultServiceRegistryAddress = "http://localhost:4000/api/v1/services";

        public const string ServiceRegistryVersion = "1.0.0";
        public const string ServiceRegistryPath = "/api/v1/services";

        public const string ServiceVersion = "0.3.9";
        public const string ServiceScheme = "ServiceScheme";
        public const string ServiceHost = "ServiceHost";
        public const string ServicePort = "ServicePort";

        protected ServiceConfiguration(IServiceRegistryClient serviceRegistryClient)
        {
            if (serviceRegistryClient == null)
                throw new OperationCanceledException("Service registry client cannot be null");

            _serviceRegistryClient = serviceRegistryClient;
            _initialRetryDelay = ConfigurationProvider.InitialRetryDelay;
            _retryCount = ConfigurationProvider.RetryCount;
        }

        protected abstract List<ServiceDescription> GetServiceDescriptions();

        public static ServiceDescription CreateServiceDescription(string name, string path, string portKey, string defaultPortValue)
        {
            return new ServiceDescription(
                name,
                ServiceVersion,
                ConfigurationProvider.GetServiceScheme(ServiceScheme, DefaultServiceScheme),
                ConfigurationProvider.GetServiceHost(ServiceHost, DefaultServiceHost), 
                ConfigurationProvider.GetServicePort(portKey, defaultPortValue),
                path);
        }

        public void RegisterServices()
        {
            Task.Run(async () => await RegisterServicesAsync());
        }

        private async Task RegisterServicesAsync()
        {
            Exception lastException = null;
            string failureContent = string.Empty;
            int retryDelay = _initialRetryDelay;

            for (int i = 0; i < _retryCount; i++)
            {
                try
                {
                    var serviceDescriptions = GetServiceDescriptions();
                    _services = CreateRegisterServiceRequests(serviceDescriptions);
                    await RegisterServices(_services);
                    return;
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    failureContent = ex.Message;
                    await Task.Delay(retryDelay);
                    retryDelay *= 2;
                }
            }

            const string msg = "Unable to call service registry";
            if (lastException != null)
                _logger.LogWarning($"{msg}, {failureContent}", lastException);
            else if (!string.IsNullOrEmpty(failureContent))
                _logger.LogWarning($"{msg}, {failureContent}");
            else
                _logger.LogWarning(msg);
        }

        public void Dispose()
        {
            if (_services == null)
                return;

            UnregisterServices(_services).Wait();
            _services = null;
        }

        private static List<RegisterServiceRequest> CreateRegisterServiceRequests(IEnumerable<ServiceDescription> services)
        {
            var transactionId = Guid.NewGuid();

            return services.Select(service => new RegisterServiceRequest
            {
                Id = Guid.NewGuid(),
                Name = service.Name,
                Security = SecurityStatus.Disabled,
                Status = ServiceStatus.Connected,
                Uri = service.Uri.ToString(),
                Version = service.Version,
                TransactionId = transactionId,
                UserId = UserIds.System,
            }).ToList();
        }

        private async Task RegisterServices(IEnumerable<RegisterServiceRequest> services)
        {
            foreach (var service in services)
            {
                await _serviceRegistryClient.RegisterServiceAsync(service);
                _logger.LogInfo($"Registered service: {service.Name} {service.Uri}");
            }
        }

        private async Task UnregisterServices(IEnumerable<RegisterServiceRequest> services)
        {
            var transactionId = Guid.NewGuid();

            foreach (var service in services)
            {
                var updateRequest = new UpdateServiceRequest
                {
                    Status = ServiceStatus.Disconnected,
                    Name = service.Name,
                    Security = service.Security,
                    Uri = service.Uri,
                    Version = service.Version,
                    TransactionId = transactionId,
                    UserId = service.UserId,
                };

                await _serviceRegistryClient.UpdateServiceAsync(service.Id, updateRequest);
                _logger.LogInfo($"Updated service: {service.Name}");
            }
        }

        public static string GetServiceRegistryAddress()
        {
            return ConfigurationProvider.GetServiceRegistryAddress(DefaultServiceRegistryAddress);
        }

        public static string GetServiceAddress(string serviceName)
        {
            return ConfigurationProvider.GetServiceAddress(serviceName);
        }
    }
}
