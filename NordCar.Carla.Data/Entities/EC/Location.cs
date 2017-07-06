using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Carla.Data.Entities.EC
{
    public class Location
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
