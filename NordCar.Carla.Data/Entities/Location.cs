using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NordCar.Carla.Data.Entities
{
    public class Location
    {
        public string LocationId {get; set;}
        public string Name {get; set;}
        public string Address1 {get; set;}
        public string Address2 {get; set;}
        public string ZipCode {get; set;}
        public string City {get; set;}
        public string Country {get; set;}
        public string PhoneNr {get; set;}
        public string FaxNr {get; set;}
        public string BookingReplyEmail1 {get; set;}
        public string BookingReplyEmail2 { get; set; }
        public string FleightNr {get; set;}
        public string Key {get; set;}
        public string Openhours {get; set;}
        
    }
}
