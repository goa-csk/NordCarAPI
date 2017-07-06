using FluentValidation.Attributes;
using NordCar.WebAPI.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Models.EC
{
    /// <summary>
    /// Trip
    /// </summary>
    public class Trip
    { 
        public string LocationId {get; set;}
        public string LocationName { get; set; }
        /// <summary>
        /// Date format [dd-MM-yyyy] 
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// Time format [hh:mm]
        /// </summary>
        public string Time { get; set; }

    }
    [Validator(typeof(PickDropInfoValidator))]
    public class PickDropInfo
    {
        public string CountryId { get; set; }
        /// <summary>
        /// CarTypeId Empty=All
        /// </summary>
        public string CarTypeId { get; set; }
        public string CarGroupId { get; set; }
        public Trip PickUp { get; set; }
        public Trip DropOff { get; set; }
        public BasicStructure1 Basic { get; set; }
    }
}