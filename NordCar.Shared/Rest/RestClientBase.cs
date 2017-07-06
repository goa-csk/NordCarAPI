using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NordCar.Shared.ServiceDiscovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Shared.Rest
{
    public class RestClientBase : IDisposable
    {
        private static string ApplicationJson = "application/json";

        private readonly SupportedServices _serviceToCall;
        private readonly HttpClientFactory _httpClientFactory;

        public RestClientBase(
            IServiceDiscovererDeprecated serviceDiscovererDeprecated,
            SupportedServices serviceToCall)
        {
            _serviceToCall = serviceToCall;
            _httpClientFactory = new HttpClientFactory(serviceDiscovererDeprecated);
        }

        public async Task<Response<TReturn>> PostAsync<TReturn, TMessage>(string uriExtension, TMessage message) //TODO MHT : 04-10-2016 - rename to search
        {
            var formattetUriExtension = UriFormatter.FormatUriAsExtension(uriExtension);

            var client = await GetHttpClient();
            var response = await client.PostAsJsonAsync(formattetUriExtension, message);
            return await ResponseFactory.GenerateResponse<TReturn>(response);
        }

        public async Task<ResponseSimple> PostAsync<TMessage>(string uriExtension, TMessage message)
        {
            var formattetUriExtension = UriFormatter.FormatUriAsExtension(uriExtension);
            var content = SerializeAsJson(message);

            var client = await GetHttpClient();
            var response = await client.PostAsync(formattetUriExtension, content);
            var simpleResponse = await ResponseFactory.GenerateResponseSimple(response);
            return simpleResponse;
        }

        public async Task<Response<TReturn>> PostAsyncWithResponse<TReturn, TMessage>(string uriExtension, TMessage message)
        {
            var formattetUriExtension = UriFormatter.FormatUriAsExtension(uriExtension);
            var content = SerializeAsJson(message);

            var client = await GetHttpClient();
            var response = await client.PostAsync(formattetUriExtension, content);
            return await ResponseFactory.GenerateResponse<TReturn>(response);
        }

        private StringContent SerializeAsJson<TMessage>(TMessage message)
        {
            var content = new StringContent(JsonConvert.SerializeObject(message, new StringEnumConverter()), Encoding.UTF8, ApplicationJson);
            return content;
        }

        public async Task<ResponseSimple> PutAsync<T>(string uriExtension, T message)
        {
            var formattetUriExtension = UriFormatter.FormatUriAsExtension(uriExtension);
            var content = SerializeAsJson(message);

            var client = await GetHttpClient();
            var response = await client.PutAsync(formattetUriExtension, content);
            return await ResponseFactory.GenerateResponseSimple(response);
        }

        public async Task<Response<T>> GetAsync<T>(string uriExtension)
        {
            var response = await GetResponseAsync(uriExtension);
            return await ResponseFactory.GenerateResponse<T>(response);
        }

        public async Task<Response<string>> GetAsync(string uriExtension)
        {
            var response = await GetResponseAsync(uriExtension);
            return await ResponseFactory.GenerateResponseSimple(response);
        }

        private async Task<HttpResponseMessage> GetResponseAsync(string uriExtension)
        {
            var client = await GetHttpClient();

            var formattetUriExtension = UriFormatter.FormatUriAsExtension(uriExtension);
            var response = await client.GetAsync(formattetUriExtension);
            return response;
        }

        //public async Task<ResponseSimple> DeleteAsync(string uriExtension, DeleteRequest request)
        //{
        //    var formattetUriExtension = UriFormatter.FormatUriAsExtension(uriExtension);

        //    var client = await GetHttpClient();
        //    var response = await client.DeleteAsync(formattetUriExtension + request.AsQueryString());
        //    return await ResponseFactory.GenerateResponseSimple(response);
        //}

        public async Task<Response<TResponse>> FindByUrl<TResponse>(string url)
        {
            return await GetAsync<TResponse>(url);
        }

        public async Task<HttpClient> GetHttpClient()
        {
            return await _httpClientFactory.GetHttpClient(_serviceToCall);
        }

        public void Dispose()
        {
            _httpClientFactory.Dispose();
        }
    }
}
