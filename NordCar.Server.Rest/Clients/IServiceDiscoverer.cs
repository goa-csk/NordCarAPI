using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Server.Rest.Clients
{
    public interface IServiceDiscoverer
    {
        Task<Uri> GetServiceAddressAsync(string name, string version, string defaultUriString = null);
    }
}
