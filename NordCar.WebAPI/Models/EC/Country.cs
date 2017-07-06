using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.WebAPI.Models.EC
{
    public class Country
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class DropDownListItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
