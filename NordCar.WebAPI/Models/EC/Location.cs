using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Models.EC
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