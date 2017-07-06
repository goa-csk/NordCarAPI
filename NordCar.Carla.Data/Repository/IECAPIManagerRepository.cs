﻿using NordCar.Carla.Data.Entities;
using NordCar.Carla.Data.Entities.EC;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NordCar.Carla.Data.Entities.CustomerPrivate;
using NordCar.Carla.Data.Entities.Promotion;
using NordCar.Carla.Data.Entities.DiscountSheet;
using NordCar.Carla.Data.Entities.MicroSite;

namespace NordCar.Carla.Data.Repository
{
    public interface IECAPIManagerRepository
    {
        //Function nr. 0
        Tuple<APIMethodControl, string> GetVersion(BasicStructure basic);
        //Function nr. 24
        Tuple<APIMethodControl, List<NordCar.Carla.Data.Entities.EC.Location>> GetLocations(BasicStructure basic, string countryId, string carGroupId); 
        //Function nr. 25
        Tuple<APIMethodControl, NordCar.Carla.Data.Entities.EC.LocationDetail> GetLocationDetails(BasicStructure basic, string id, DateTime StartDate, int PeriodLengthInDays);
        //Function nr. 26
        Tuple<APIMethodControl, List<NordCar.Carla.Data.Entities.EC.Country>> GetCountries(BasicStructure basic);
        //Function nr. 27
        Tuple<APIMethodControl, List<NordCar.Carla.Data.Entities.EC.CarDetail>> GetAvailableCars(BasicStructure basic, NordCar.Carla.Data.Entities.EC.PickDropInfo input, string age);
        //Function nr. 28
        Tuple<APIMethodControl, List<NordCar.Carla.Data.Entities.EC.CarType>> GetCarTypes(BasicStructure basic); 
        //Function nr. 29
        Tuple<APIMethodControl, NordCar.Carla.Data.Entities.EC.CarExtra> GetCarExtras(BasicStructure basic, NordCar.Carla.Data.Entities.EC.PickDropInfo input, string productId, string age);
        //Function nr. 30
        Tuple<APIMethodControl, NordCar.Carla.Data.Entities.EC.PriceCalculated> UpdatePrice(BasicStructure basic, NordCar.Carla.Data.Entities.EC.PricePart input, string age);
        //Function nr. 31
        Tuple<APIMethodControl,RentalInfo> MakeReservation(BasicStructure basic, NordCar.Carla.Data.Entities.EC.Reservation input);
        //Function nr. 32
        Tuple<APIMethodControl, List<NordCar.Carla.Data.Entities.EC.Booking>> SearchBooking(BasicStructure basic, string reservationNo, string email, string pickupDate, string lastName);
        //Function nr. 33
        Tuple<APIMethodControl, bool> CancelBooking(BasicStructure basic, string reservationNo);
        //Function nr. 34
        Tuple<APIMethodControl, byte[]> GetPdfBooking(BasicStructure basic, int pdfType, string reservationNo, string email);
        //Function nr. 35
        Tuple<APIMethodControl, NordCar.Carla.Data.Entities.EC.Account> CreateAccount(BasicStructure basic, NordCar.Carla.Data.Entities.EC.Person account);
        //Function nr. 36
        Tuple<APIMethodControl, NordCar.Carla.Data.Entities.EC.Account> Login(BasicStructure basic, string loginType, string userId, string password);
        //Function nr. 37
        Tuple<APIMethodControl, string> ForgotPassword(BasicStructure basic, string loginType, string userId);
        //Function nr. 38
        Tuple<APIMethodControl, NordCar.Carla.Data.Entities.EC.Account> ModifyAccount(BasicStructure basic, NordCar.Carla.Data.Entities.EC.Person account);
        //Function nr. 39
        Tuple<APIMethodControl, List<NordCar.Carla.Data.Entities.EC.CarSpec>> GetCarSpecifications(BasicStructure basic, string countryId, string cartype, string carGroup, string age);
        //Function nr. 40,41,42
        Tuple<APIMethodControl, List<NordCar.Carla.Data.Entities.EC.DropDownListItem>> DropDownLists(int type, BasicStructure basic);
        //Function nr. 43
        Tuple<APIMethodControl, List<NordCar.Carla.Data.Entities.EC.Exception>> GetLocationDateExceptions(BasicStructure basic, string LocationId, DateTime StartDate, int PeriodLengthInDays );
        //Function nr 44
        Tuple<APIMethodControl, DibsResultItem> DibsResult(BasicStructure basic, int bookingId, int paymentFlag, int paymentType, int paymentCode, int paymentAmount, int depositPaymentCode, int depositPaymentAmount);
        //Function nr 45
        Tuple<APIMethodControl, string> CheckPromotionCode(BasicStructure basic);
        //Function nr 46
        Tuple<APIMethodControl, List<NordCar.Carla.Data.Entities.EC.CarTypeLocationDetails>> GetCarTypesByLocation(BasicStructure basic, string LocationId, string Country);
        //Function nr 47
        Tuple<APIMethodControl, ReservationText> GetReservationText(BasicStructure basic, string reservationNo);
        //Function nr 48
        Tuple<APIMethodControl, byte[]> GetDiscountSheetXls(BasicStructure basic, string discountSheetId);
        //Function nr 49
        Tuple<APIMethodControl, List<DiscountSheet>> GetDiscountSheetList(BasicStructure basic);
         //Function nr 50
        Tuple<APIMethodControl, List<Promotion>> GetPromotionCodeList(BasicStructure basic);
        //Function nr 50a
        Tuple<APIMethodControl, Promotion> GetPromotion(BasicStructure basic, string id);

        //Function nr 51
        Tuple<APIMethodControl, string> PromotionCodeAdd(BasicStructure basic, PromotionAdd promotion);
        //Function nr 52
        Tuple<APIMethodControl, string> PromotionCodeEdit(BasicStructure basic, PromotionAdd promotionEdit);
        //Function nr 53
        Tuple<APIMethodControl, string> DeletePromotionCode(BasicStructure basic, string id);
        //Function nr 55
        Tuple<APIMethodControl, List<MicroSite>> GetMicroSiteList(BasicStructure basic);
        //Function nr 54
        Tuple<APIMethodControl, string> AddMicroSite(BasicStructure basic, MicroSite microsite);
        //Function nr 56
        Tuple<APIMethodControl, string> EditMicroSite(BasicStructure basic, MicroSite microsite);
        //Function nr 57 (SOS)
        Tuple<APIMethodControl, List<QueueInfo>> GetReservationStatusQueue(BasicStructure basic, string customerAgreementNumber);
        //Function nr 58 (SOS)
        Tuple<APIMethodControl, string> ReservationStatusQueueMessageProcessed(BasicStructure basic, string messageId, string SOSResponse);
        //Function nr 59 (SOS)
        Tuple<APIMethodControl, ResRAData> GetResRAData(BasicStructure basic, int typeId, string number);
        Tuple<APIMethodControl, List<BookType>> GetBookTypes(BasicStructure basic);
         //Function nr 60
        Tuple<APIMethodControl, byte[]> GetMicrositeSheetXls(BasicStructure basic, string micrositeSheetId);
        //Function nr 62 (Promotion)
        Tuple<APIMethodControl, List<Umbrella>> GetUmbrellaPromotionList(BasicStructure basic, string UmbrellaId);

        #region Methods CRIS - customer company 
        //Function nr 63 (CRIS)
        Tuple<APIMethodControl, List<CustomerCompanyListItem>> GetCustomerCompanyNumbers(BasicStructure basic, DateTime? changeDateGreaterthan, int? maxAntal, int? startInterval);

        //Function nr 64 (CRIS)
        Tuple<APIMethodControl, CustomerCompany> GetCustomerCompany(BasicStructure basic, string customerCompanyNumber);

        //Function nr 64_1 (CRIS)
        Tuple<APIMethodControl, List<CustomerCompany>> GetListCustomerCompany(BasicStructure basic, List<string> customerCompanyNumbers);
        //Function nr 65 (CRIS)
        Tuple<APIMethodControl, string> CreateCustomerCompany(BasicStructure basic, CustomerCompany customerCompany);

        //Function nr 66 (CRIS)
        Tuple<APIMethodControl, string> UpdateCustomerCompany(BasicStructure basic, CustomerCompany customerCompany);

        //Function nr 67 (CRIS)
        Tuple<APIMethodControl, string> DeleteCustomerCompany(BasicStructure basic, string customerCompanyNumber);
        #endregion

        #region Methods CRIS - customer private 
        //Function nr 68 (CRIS)
        Tuple<APIMethodControl, List<CustomerPrivateListItem>> GetCustomerPrivateNumbers(BasicStructure basic, DateTime? changeDateGreaterthan, int? maxAntal, int? startInterval);

        //Function nr 69 (CRIS)
        Tuple<APIMethodControl, CustomerPrivate> GetCustomerPrivate(BasicStructure basic, string customerPrivateNumber);

        //Function nr 69_1 (CRIS)
        Tuple<APIMethodControl, List<CustomerPrivate>> GetListCustomerPrivate(BasicStructure basic, List<string> customerPrivateNumbers);
        //Function nr 70 (CRIS)
        Tuple<APIMethodControl, string> CreateCustomerPrivate(BasicStructure basic, CustomerPrivate customerPrivate);

        //Function nr 71 (CRIS)
        Tuple<APIMethodControl, string> UpdateCustomerPrivate(BasicStructure basic, CustomerPrivate customerPrivate);

        //Function nr 72 (CRIS)
        Tuple<APIMethodControl, string> DeleteCustomerPrivate(BasicStructure basic, string customerPrivateNumber);
        #endregion
    }
}
  