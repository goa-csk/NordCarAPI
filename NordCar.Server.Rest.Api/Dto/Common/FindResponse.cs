using NordCar.Server.Rest.Api.Dto.Link;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Server.Rest.Api.Dto.Common
{
    public class FindResponse<T> where T : ResLink
    {
        public List<T> Items { get { return new List<T>(); } set; }
        public string NextUri { get; set; }
        public string PreviousUri { get; set; }
    }
}
