using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NordCar.Carla.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Models.Promotion
{
    public class Promotion
    {
        public string Id { get; set; } //Unigue name

        [JsonConverter(typeof(StringEnumConverter))]
        public EnablePromotion Enable { get; set; } //0 = off, 1 = Skranke, 2=web, 3=1+2
        public int CountLimit { get; set; } //0 = ubegrænset
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<string> DiscountSheetIds { get; set; }
        public int UsedCount { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public PromotionType PromotionType { get; set; }
    }
}