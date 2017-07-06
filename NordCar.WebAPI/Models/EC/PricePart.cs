using FluentValidation.Attributes;
using NordCar.WebAPI.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Models.EC
{
    public class SelectedExtra
    {
        public string Id { get; set; }
         /// <summary>
        /// Checkbox, Dropdown, Numeric
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// If Checkbox
        /// </summary>
        public string Value { get; set; }
    }
    [Validator(typeof(PricePartValidator))]
    public class PricePart
    {
        
        public string ProductId { get; set; }
        public PickDropInfo PickDropInfo { get; set; }
        public SelectedExtras Extra { get; set; }

    }
}