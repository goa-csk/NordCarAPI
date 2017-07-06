using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NordCar.Shared.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NordCar.Server.Rest.Clients
{
    public class RestClient : IRestClient
    {
        private const string ApplicationJson = "application/json";
        private const string GZip = "gzip";

        //private readonly ILogger _logger = LoggerManager.CreateLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly RestClientConfigurationProvider ConfigurationProvider = new RestClientConfigurationProvider();
        //private readonly IBioCredentials _credentials;
        private readonly IServiceDiscoverer _serviceDiscoverer;
        private HttpClient _httpClient;
        private DateTime _lastUpdated;
        private Uri _absoluteUri;
        private readonly TimeSpan _requestTimeout;
        private readonly TimeSpan _cacheTimeout;
        private string _serviceName;
        private string _serviceVersion;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private bool _redescoverServiceUri;

        //public RestClient(IBioCredentials credentials)
        public RestClient()
        
        {
            //if (credentials == null)
            //    throw new ArgumentNullException(nameof(credentials));

            //_credentials = credentials;
            _serviceDiscoverer = null;

            _requestTimeout = ConfigurationProvider.RequestTimeout;
            _cacheTimeout = ConfigurationProvider.CacheTimeout;
        }

        //public RestClient(IBioCredentials credentials, IServiceDiscoverer serviceDiscoverer)
        public RestClient( IServiceDiscoverer serviceDiscoverer)
        
        {
           // if (credentials == null)
           //     throw new ArgumentNullException(nameof(credentials));
            if (serviceDiscoverer == null)
                throw new ArgumentNullException(serviceDiscoverer.ToString());

            //_credentials = credentials;
            _serviceDiscoverer = serviceDiscoverer;

            _requestTimeout = ConfigurationProvider.RequestTimeout;
            _cacheTimeout = ConfigurationProvider.CacheTimeout;
        }

        /// <summary>
        /// See https://www.dotnetperls.com/uri to figure out what AbsoluteUri means
        /// </summary>
        /// <param name="absoluteUri">full URI to service</param>
        public void Configure(Uri absoluteUri)
        {
            if (absoluteUri == null)
                throw new ArgumentNullException(absoluteUri.ToString());

            _absoluteUri = absoluteUri;
            _httpClient = null;
        }

        public void Configure(string serviceName, string serviceVersion)
        {
            if (string.IsNullOrEmpty(serviceName))
                throw new ArgumentNullException(serviceName.ToString());
            if (string.IsNullOrEmpty(serviceVersion))
                throw new ArgumentNullException(serviceVersion.ToString());

            _serviceName = serviceName;
            _serviceVersion = serviceVersion;
        }

        public async Task<Response<T>> GetAsync<T>(string uriExtension)
        {
            try
            {
                var response = await GetResponseAsync(uriExtension);
                return await ResponseFactory.GenerateResponse<T>(response);
            }
            catch (Exception)
            {
                SetRediscoverServiceUri();
                throw;
            }
        }

        public async Task<Response<string>> GetAsync(string uriExtension)
        {
            try
            {
                var response = await GetResponseAsync(uriExtension);
                return await ResponseFactory.GenerateResponseSimple(response);
            }
            catch (Exception)
            {
                SetRediscoverServiceUri();
                throw;
            }
        }

        public async Task<Response<TResponse>> GetAbsoluteAsync<TResponse>(string absoluteUri)
        {
            try
            {
                var client = await GetHttpClient();
                var response = await client.GetAsync(absoluteUri);
                return await ResponseFactory.GenerateResponse<TResponse>(response);
            }
            catch (Exception)
            {
                SetRediscoverServiceUri();
                throw;
            }
        }

        public async Task<ResponseSimple> PostAsync<TMessage>(string uriExtension, TMessage message)
        {
            try
            {
                var content = SerializeAsJson(message);
                var client = await GetHttpClient();
                var response = await client.PostAsync(_absoluteUri + uriExtension, content);
                var simpleResponse = await ResponseFactory.GenerateResponseSimple(response);
                return simpleResponse;
            }
            catch (Exception)
            {
                SetRediscoverServiceUri();
                throw;
            }
        }

        public async Task<Response<TReturn>> PostAsyncWithResponse<TReturn, TMessage>(string uriExtension, TMessage message)
        {
            try
            {
                var content = SerializeAsJson(message);
                var client = await GetHttpClient();
                var response = await client.PostAsync(_absoluteUri + uriExtension, content);
                return await ResponseFactory.GenerateResponse<TReturn>(response);
            }
            catch (Exception)
            {
                SetRediscoverServiceUri();
                throw;
            }
        }

        public async Task<Response<TReturn>> PostAsync<TReturn, TMessage>(string uriExtension, TMessage message)
        {
            try
            {
                var client = await GetHttpClient();
                var response = await client.PostAsJsonAsync(_absoluteUri + uriExtension, message);
                return await ResponseFactory.GenerateResponse<TReturn>(response);
            }
            catch (Exception)
            {
                SetRediscoverServiceUri();
                throw;
            }
        }

        public async Task<ResponseSimple> PutAsync<T>(string uriExtension, T message)
        {
            try
            {
                var content = SerializeAsJson(message);
                var client = await GetHttpClient();
                var response = await client.PutAsync(_absoluteUri + uriExtension, content);
                return await ResponseFactory.GenerateResponseSimple(response);
            }
            catch (Exception)
            {
                SetRediscoverServiceUri();
                throw;
            }
        }

        public async Task<ResponseSimple> DeleteAsync(string uriExtension)
        {
            try
            {
                var client = await GetHttpClient();
                var response = await client.DeleteAsync(_absoluteUri + uriExtension);
                return await ResponseFactory.GenerateResponseSimple(response);
            }
            catch (Exception)
            {
                SetRediscoverServiceUri();
                throw;
            }
        }

        private StringContent SerializeAsJson<TMessage>(TMessage message)
        {
            var content = new StringContent(JsonConvert.SerializeObject(message, new StringEnumConverter()), Encoding.UTF8, ApplicationJson);
            return content;
        }

        private async Task<HttpResponseMessage> GetResponseAsync(string uriExtension)
        {
            var client = await GetHttpClient();
            var response = await client.GetAsync(_absoluteUri + uriExtension);
            return response;
        }

        /// <summary>
        /// See https://www.dotnetperls.com/uri to figure out what Scheme and Authority means
        /// </summary>
        private async Task<HttpClient> GetHttpClient()
        {
            try
            {
                await _semaphore.WaitAsync();

                if (IsTimeToClearHttpClientCache())
                {
                    ClearHttpClientCache();
                }

                if (!HasCachedHttpClient())
                {
                    if (_absoluteUri == null || GetRediscoverServiceUri())
                    {
                        await DiscoverServiceUri();
                    }

                    //_httpClient = await CreateHttpClient(_credentials, GetServiceBaseAddress());
                    _httpClient = await CreateHttpClient(GetServiceBaseAddress());
                    
                    _lastUpdated = DateTime.Now;
                    //_logger.LogDebug($"Updated HTTP client for {_absoluteUri}");
                }
            }
            finally
            {
                _semaphore.Release();
            }

            return _httpClient;
        }

        private bool GetRediscoverServiceUri()
        {
            return _redescoverServiceUri && HasServiceNameAndVersionDefined();
        }

        private void SetRediscoverServiceUri()
        {
            _redescoverServiceUri = HasServiceNameAndVersionDefined();
        }

        private Uri GetServiceBaseAddress()
        {
            //return new Uri($"{_absoluteUri.Scheme}://{_absoluteUri.Authority}");
            return new Uri(string.Format("{0}://{1}",_absoluteUri.Scheme,_absoluteUri.Authority));
        
        }

        private async Task DiscoverServiceUri()
        {
            if (!HasServiceNameAndVersionDefined())
                throw new OperationCanceledException("Client not configured");

            if (_serviceDiscoverer != null)
            {
                _absoluteUri = await _serviceDiscoverer.GetServiceAddressAsync(_serviceName, _serviceVersion);
                //_logger.LogDebug($"Discovered service address {_absoluteUri}");
            }
            else
            {
                _absoluteUri = new Uri(ServiceConfiguration.GetServiceAddress(_serviceName));
            }
            _redescoverServiceUri = false;
        }

        private bool HasServiceNameAndVersionDefined()
        {
            return !string.IsNullOrEmpty(_serviceName) && !string.IsNullOrEmpty(_serviceVersion);
        }

        private bool HasCachedHttpClient()
        {
            return _httpClient != null;
        }

        private bool IsTimeToClearHttpClientCache()
        {
            return _httpClient != null && (DateTime.Now - _lastUpdated) > _cacheTimeout;
        }

        //private async Task<HttpClient> CreateHttpClient(IBioCredentials credentials, Uri baseAddress)
        private async Task<HttpClient> CreateHttpClient(Uri baseAddress)
       
        {
            var client = new HttpClient(
                new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip
                });

            //await ConfigureHttpClient(client, credentials, baseAddress);
            await ConfigureHttpClient(client, baseAddress);

            return client;
        }

        //private async Task ConfigureHttpClient(HttpClient client, IBioCredentials credentials, Uri baseAddress)
        private async Task ConfigureHttpClient(HttpClient client, Uri baseAddress)
       
        {
            client.Timeout = _requestTimeout;
            client.BaseAddress = baseAddress;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJson));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue(GZip));

            //var bearerToken = await RequestTokenAsync(credentials, baseAddress);
            //client.SetBearerToken(bearerToken);
        }
        //private async Task<string> RequestTokenAsync(IBioCredentials credentials, Uri baseAddress)
        private async Task<string> RequestTokenAsync(Uri baseAddress)
        {
            var payload = string.Format("grant_type=password&username={0}&password={1}","EC","EC");
            var content = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await PostAsyncStringContent("Token", content, baseAddress);
            dynamic tokenPayload = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
            return tokenPayload.access_token;
        }

        private async Task<HttpResponseMessage> PostAsyncStringContent(string uriExtension, StringContent stringContent, Uri baseAddress)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                return await client.PostAsync(uriExtension, stringContent);
            }
        }

        private void ClearHttpClientCache()
        {
            if (_httpClient == null)
                return;

            _httpClient.Dispose();
            _httpClient = null;
        }

        public void Dispose()
        {
            ClearHttpClientCache();
        }
    }
    
}
