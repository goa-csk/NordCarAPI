using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Shared.Rest
{
    public interface IDomainServiceRestClient
    {
       Task<Response<TResponse>> FindByUrl<TResponse>(string url);
    } 
}
