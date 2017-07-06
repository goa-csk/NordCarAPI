using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Carla.Data.Entities.EC
{
    public class ResRAData
    {
        public string ReservationNumber { get; set; }
        public string RANumber { get; set; }
        public string Status { get; set; }
        public string StationNoOut { get; set; }
        public string DateOut { get; set; }
        public string TimeOut { get; set; }
        public string StationNoIn { get; set; }
        public string DateIn { get; set; }
        public string TimeIn { get; set; }
        public string CarRegistrationNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime FirstRegistrationDate { get; set; }
        public bool WinterTires { get; set; }
        public bool Automatic { get; set; }
        public bool Towbar { get; set; }
        public bool ChildSeat { get; set; }
        public bool GPS { get; set; }
        public bool ExtraDriver { get; set; }
        public string CustomerReferenceNumber { get; set; }
        public DateTime RACreatedTime { get; set; }
        public string InvoiceCurrencyCode { get; set; }
        public decimal InvoicedFuelLitre { get; set; }
        public decimal InvoicedFuelPrice { get; set; }
        public decimal MilageRegisteredOnContract { get; set; }

    }

   
}
