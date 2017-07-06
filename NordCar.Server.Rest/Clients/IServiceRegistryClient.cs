using NordCar.Shared.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Server.Rest.Clients
{
    public interface IServiceRegistryClient : IRestClient
    {
        Task<Response<FindResponse<ServiceEntryDto>>> FindServicesAsync(FindServicesRequest request);
        Task<ResponseSimple> RegisterServiceAsync(RegisterServiceRequest request);
        Task<ResponseSimple> UnregisterServiceAsync(Guid serviceId, DeleteRequest request);
        Task<ResponseSimple> UpdateServiceAsync(Guid serviceId, UpdateServiceRequest request);
    }
}
