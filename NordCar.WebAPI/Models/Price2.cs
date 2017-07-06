using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Models
{
    public class ExtraProduct
    {
        public int id { get; set; }
        public string numbUnit { get; set; }
    }
    
    public class Price2
    {
        public int locationId { get; set; } 
        public int returnLocationId { get; set; } 
        public string pickupDate { get; set; } 
        public string pickupTime { get; set; } 
        public string returnDate { get; set; } 
        public string returnTime { get; set; } 
        public string categoryId { get; set; }
        public int productId { get; set; }
        public List<ExtraProduct> extras { get; set; }
    }
}