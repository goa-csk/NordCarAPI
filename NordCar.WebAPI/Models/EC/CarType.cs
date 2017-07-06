using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.WebAPI.Models.EC
{
    public class CarType
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class CarTypeLocationDetails
    {
        public List<CarType> CarTypes { get; set; }
        public string LocationId { get; set; }
    }
}
