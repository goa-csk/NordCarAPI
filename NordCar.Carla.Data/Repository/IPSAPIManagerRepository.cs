using NordCar.Carla.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.Carla.Data.Repository
{
    public interface IPSAPIManagerRepository
    {

        //Function nr 1
        Tuple<APIMethodControl, List<CarListItem>> GetCarsList(BasicStructure basic, int LocationId); //0=ALL

        //Function nr 2a
        Tuple<APIMethodControl, List<PriceListItem>> GetPriceList(BasicStructure basic, int locationId, string categoryId);

        //Function nr 2b
        Tuple<APIMethodControl, List<PriceListExtraItem>> GetPriceListExtra(BasicStructure basic, int locationId, string categoryId);

        //Function nr 3 (GetAvailabillityList)
        Tuple<APIMethodControl, List<AvailabillityItem_PS>> GetAvaiabillityList(BasicStructure basic, int locationId, int productId, int returnLocationId, string categoryId, string pickupDate, string returnDate, string pickupTime, string returnTime, int age);
     
        //Function nr 5 
        Tuple<APIMethodControl, List<PriceInfo>> UpdatePrice(BasicStructure basic, Price2 price);

        //Function nr 10b
        Tuple<APIMethodControl, RentalInfo> SubmitRental(BasicStructure basic, Rental_PS rent); 
      

        //Function nr 15
        Tuple<APIMethodControl, FrontPageDefault_PS> GetFrontPageDefault(BasicStructure basic);

        //Function nr 17
        Tuple<APIMethodControl, DibsResultItem> DibsResult(BasicStructure basic, int bookingId, int paymentFlag, int paymentType, int paymentCode, int paymentAmount, int depositPaymentCode, int depositPaymentAmount);

        //Function nr. 21
        Tuple<APIMethodControl, List<PromotionInfo>> PromotionUpdate(BasicStructure basic, Price2 price);

    }
}