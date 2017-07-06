using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Models.EC
{
    public class Option
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public string Icon { get; set; }
        public string FeatureId { get; set; }
    }

    public class CarDetail
    {
        
        //public string CarId { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public string TypeId { get; set; }
        public string Image { get; set; }
        public string Price { get; set; }
        public string RentalPrice { get; set; }
        public string YoungDriverFee { get; set; }
        public string AirportFee { get; set; }
        public string CampaignPrice { get; set; }
        public string AcrissCode { get; set; }
        public string ProductId { get; set; }
        public string CarGroupId { get; set; }
        /// <summary>
        /// 1=OK 2=Udsolgt 3=Forespørgsel 4=Forespørgsel.m.booking.nr
        /// </summary>
        public int bookStatus { get; set; }
        public string bookStatusTekst { get; set; }

        public string TeaserText { get; set; }
        public string ProductInfo { get; set; }
        public string Duration { get; set; }
        public string PriceperDay { get; set; }
        public string ConfirmationLocationEmail { get; set; }

        public List<Option> Features { get; set; }
        public Trip PickUp { get; set; }
        public Trip DropOff { get; set; }
    }

    public class CarSpec
    {
        public string CarTypeId { get; set; }
        public string CarGroup { get; set; }
        public string AcrissCode { get; set; }
        public List<Option> Features { get; set; }
    }
}