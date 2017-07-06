using NordCar.Server.Rest.Api.Dto.Common;
using NordCar.Shared.ServiceDiscovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Shared.Rest
{
    public abstract class DomainServiceRestClientBase : IDomainServiceRestClient
    {

        private readonly RestClientBase _restClient;

        protected DomainServiceRestClientBase(
            IServiceDiscovererDeprecated serviceDiscovererDeprecated,
            SupportedServices serviceToCall)
        {
            _restClient = new RestClientBase(serviceDiscovererDeprecated, serviceToCall);
        }

        protected async Task<Response<TReturn>> PostAsync<TReturn, TMessage>(string uriExtension, TMessage message)
        {
            return await _restClient.PostAsync<TReturn, TMessage>(uriExtension, message);
        }

        protected async Task<ResponseSimple> PostAsync<TMessage>(string uriExtension, TMessage message) where TMessage : PostRequest
        {
            return await _restClient.PostAsync(uriExtension, message);
        }

        protected async Task<ResponseSimple> PutAsync<T>(string uriExtension, T message)
        {
            return await _restClient.PutAsync(uriExtension, message);
        }

        protected async Task<Response<T>> GetAsync<T>(string uriExtension)
        {
            return await _restClient.GetAsync<T>(uriExtension);
        }

        protected async Task<Response<string>> GetAsync(string uriExtension)
        {
            return await _restClient.GetAsync(uriExtension);
        }

        //protected async Task<ResponseSimple> DeleteAsync(string uriExtension, DeleteRequest request)
        //{
        //    return await _restClient.DeleteAsync(uriExtension, request);
        //}

        public async Task<Response<TResponse>> FindByUrl<TResponse>(string url)
        {
            return await _restClient.FindByUrl<TResponse>(url);
        }

    }
}
