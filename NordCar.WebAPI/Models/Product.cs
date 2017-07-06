using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.WebAPI.Models
{
    public class Product
    {
        public string ProductId { get; set; }
        public string Description { get; set; }
        public string Units { get; set; }
        public string PricePrUnit { get; set; }
        public string CurrentNoUnits { get; set; }
        public string MinNoUnits { get; set; }
        public string MaxNoUnits { get; set; }
        public string SelectionType { get; set; }
    }
}
