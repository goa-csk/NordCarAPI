using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.Carla.Data.Entities
{

    public class Driver2
    {
        public string name { get; set; }
    }

    public class Rental
    {
        public int locationId { get; set; }
        public int returnLocationId { get; set; }
        public int productId { get; set; }
        public string pickupDate { get; set; }
        public string pickupTime { get; set; }
        public string returnDate { get; set; }
        public string returnTime { get; set; }
        public string categoryId { get; set; }
        public string coRenterName { get; set; }
        public string coRenterSurName { get; set; }
        public string coRenterLicenseNo { get; set; }
        public string coRenterBirthDay { get; set; }
        public string rekvisitionNo { get; set; }
        public string payType { get; set; }
        public List<Driver2> drivers { get; set; }
        public List<ExtraProduct> extras { get; set; }
    }

    public class Rental_PS : Rental
    {
        public string bookStatus { get; set; }
        public string renterName { get; set; }
        public string renterBirthDay { get; set; }
        public string renterAddress { get; set; }
        public string renterZipCity { get; set; }
        public string renterPhone { get; set; }
        public string renterEmail { get; set; }
    }
}