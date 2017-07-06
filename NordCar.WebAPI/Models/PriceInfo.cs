using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.WebAPI.Models
{
    public class PriceInfo
    {
        public string Total { get; set; }
        public string DepositOnline { get; set; }
        public string DepositCash { get; set; }
        public string DepositCreditCard { get; set; }
        public string TotalDepositOnline { get; set; }
        public string TotalDepositCash { get; set; }
        public string TotalDepositCreditCard { get; set; }
        public string TotalExtraPrice { get; set; }
        public string TotalExclusiveTotalExtraPrice { get; set; }
        public int BookStatus { get; set; }
        public string BookStatusText { get; set; }
        public int PayCashOnCollectFlag { get; set; }
        public int PayCardOnCollectFlag { get; set; }
        public int PayOnlineFlag { get; set; }
        public string DepositDescription { get; set; }
        public string RentPricePrDay { get; set; }
        public string PickupText { get; set; }
        public string ProductDeductibleText { get; set; }
        public int NumberOfDays { get; set; }
        public int NumberOfKMs { get; set; }
        public string ProductName { get; set; }
        
    }
}
