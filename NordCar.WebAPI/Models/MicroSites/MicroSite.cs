using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Models.MicroSites
{
    public class MicroSite
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }

    }
}