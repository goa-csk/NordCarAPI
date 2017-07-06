using NordCar.Shared.ServiceDiscovery;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Shared.Rest
{
    public class HttpClientFactory
    {    
        //private readonly ILogger _logger = LoggerManager.CreateLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ConcurrentDictionary<SupportedServices, HttpClient> _serviceToClientMap = new ConcurrentDictionary<SupportedServices, HttpClient>();

        private readonly TimeSpan _timeOut = TimeSpan.FromMinutes(5);//TODO MHT : 12-09-2016 - do not hardcode a timeout this high
        private readonly BaseAddressResolver _baseAddressResolver;
        private readonly TokenCreator _tokenCreator;

        public HttpClientFactory(IServiceDiscovererDeprecated serviceDiscovererDeprecated)
        {
           
            _baseAddressResolver = new BaseAddressResolver(serviceDiscovererDeprecated);
            _tokenCreator = new TokenCreator(_baseAddressResolver);
        }

        public void ClearCaches()
        {
            _serviceToClientMap.Clear();
        }

        public async Task<HttpClient> GetHttpClient(SupportedServices serviceToCall)
        {
            //METHOD FLOW:
            //
            // Reasoning for method:
            // We need this to be treadsafe and as such we use a concurrentDictionary
            // This is not enough as the call to create a new client is time consuming (needs security token)
            // To solve this we have the following flow:
            //
            //  - Try to get a cached value
            //  - If none is found create a new and make a concurrent GetOrAdd 
            //    This is done to ensure that we; get one that has been added by another tread OR add ours to the cache
            //  - return the found client

            HttpClient client;
            var isCachedClientFound = _serviceToClientMap.TryGetValue(serviceToCall, out client);

            if (!isCachedClientFound)
            {
                var newClientToAdd = await CreateNewHttpClient(serviceToCall);
                client = _serviceToClientMap.GetOrAdd(serviceToCall, newClientToAdd);
            }

            return client;
        }

        private async Task<HttpClient> CreateNewHttpClient(SupportedServices serviceToCall)
        {
            //_logger.LogDebug($"New HTTP client created for {serviceToCall}");
            var client = new HttpClient(
                new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip
                });
            await Configure(client, serviceToCall);
            return client;
        }

        private async Task Configure(HttpClient client, SupportedServices serviceToCall)
        {
            client.Timeout = _timeOut;
            client.BaseAddress = _baseAddressResolver.ResolveBaseAddress(serviceToCall);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            //var bearerToken = await _tokenCreator.RequestTokenAsync(_bioCredentials, serviceToCall);
            //client.SetBearerToken(bearerToken);
        }

        public void Dispose()
        {
            foreach (var client in _serviceToClientMap.Values)
            {
                client.Dispose();
            }
            _serviceToClientMap.Clear();
        }
    }
}
