using NordCar.Shared.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Server.Rest.Clients
{
    public class IRestClient : IDisposable
    {
        void Configure(Uri baseAddress);
        void Configure(string serviceName, string serviceVersion);
        Task<Response<string>> GetAsync(string uriExtension);
        Task<Response<T>> GetAsync<T>(string uriExtension);
        Task<Response<TResponse>> GetAbsoluteAsync<TResponse>(string absoluteUri);
        Task<ResponseSimple> PostAsync<TMessage>(string uriExtension, TMessage message);
        Task<Response<TReturn>> PostAsync<TReturn, TMessage>(string uriExtension, TMessage message);
        Task<Response<TReturn>> PostAsyncWithResponse<TReturn, TMessage>(string uriExtension, TMessage message);
        Task<ResponseSimple> PutAsync<T>(string uriExtension, T message);
        Task<ResponseSimple> DeleteAsync(string uriExtension);
    }
}
