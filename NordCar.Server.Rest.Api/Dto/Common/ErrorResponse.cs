using NordCar.Shared.Domain.UniqueMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Server.Rest.Api.Dto.Common
{
    public class ErrorResponse
    {
        public List<UniqueMessage> Errors {
            get
            {
                return new List<UniqueMessage>();

            }
            set { }
        } 
    }
}
