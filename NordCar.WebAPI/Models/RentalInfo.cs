using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.WebAPI.Models
{
    public class RentalInfo
    {
        public string ReservationNo { get; set; }
        public string TotalPrice { get; set; }
        public string DepositOnline { get; set; }
        public string DepositPickupCash { get; set; }
        public string DepositPickupCard { get; set; }
        public string RentPlusDepositOnline { get; set; }
        public string RentPlusDepositPickupCash { get; set; }
        public string RentPlusDepositPickupCard { get; set; }
        public string AddOnsTotalPrice { get; set; }
        public string TotalPriceExclAddOnsTotalPrice { get; set; }
        /// <summary>
        /// Cc email adresser
        /// </summary>
        public List<string> CcEmail { get; set; }
    }
}
