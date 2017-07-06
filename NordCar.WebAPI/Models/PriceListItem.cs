using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Models
{
    public class PriceListItem
    {
        public string LocationId { get; set; }
        public string CarType { get; set; }
        public string CarGroup { get; set; }
        public string CarGroupFilRef { get; set; }
        public string CarGroupSpecialTopText { get; set; }
        public string CarGroupSpecialBottomText { get; set; }
        public List<PriceProductItem> PriceProductLines { get; set; }
    }

    public class PriceListItem_DON
    {
        public string LocationId { get; set; }
        public string ProductId { get; set; }
        public string Description { get; set; }
        public string ProductPrice { get; set; }
        public string KmPrice { get; set; }
        public string CategoryId { get; set; }
        public string CarType { get; set; }
        public string DefaultProductSelection { get; set; }
        public string PickupTime { get; set; }
        public string ReturnTime { get; set; }
        public string KmIncluded { get; set; }
    }

    public class PriceListItemExtra_DON
    {
        public string LocationId { get; set; }
        public string ExtraProductId { get; set; }
        public string ExtraDescription { get; set; }
        public string ExtraPrice { get; set; }

    }
}