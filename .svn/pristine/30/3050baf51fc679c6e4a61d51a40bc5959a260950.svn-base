using FluentValidation.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NordCar.WebAPI.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Models
{
   

    public enum LanguageList
    {
        DA,
        EN
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum BookTypes
    {
        DON2BOOK = 1, 
        PSBOOK,
        ECBOOK,
        DAT,
        SOS,
        CARLSBERGVIKAR,
        TXBOOK,
        BCBOOK
    }

    
    public class BasicStructure
    {
        public LanguageList Language { get; set; }
        public string BookTypes { get; set; }
        public string IPAddress { get; set; }
        public string CompanyDealId { get; set; }
        public string CustomerId { get; set; }
        public string ExtraId { get; set; }
        public string VoucherCode { get; set; }
        public string OrgBookNr { get; set; }
        public string StepNr { get; set; }
    }

    /// <summary>
    /// Exposed to clients
    /// </summary>
    [Validator(typeof(BasicStructure1Validator))]
    public class BasicStructure1
    {   
        /// <summary>
        /// Language 
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// BookType
        /// </summary>
        public string BookTypes { get; set; }
        /// <summary>
        /// IP address
        /// </summary>
        public string IPAddress { get; set; }
        public string CompanyDealId { get; set; }
        public string CustomerId { get; set; }
        public string ExtraId { get; set; }
        /// <summary>
        /// PromotionCode
        /// </summary>
        public string VoucherCode { get; set; }
        public string OrgBookNr { get; set; }
        public string StepNr { get; set; }
    }

    public static class Helper
    {
        public static List<string> GetBookTypes()
        {
            return Enum.GetNames(typeof(BookTypes)).ToList();
        }

        public static BookTypes ParseEnum(string value)
        {
            BookTypes myvalue;
            Enum.TryParse<BookTypes>(value, true, out myvalue);
            return myvalue;
        }



    }
}

