using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Server.Rest.Api.Dto.Link
{
    public class ResLink : ILink
    {
        public Guid Id { get; set; }
        public string Uri { get; set; }
    }
}
