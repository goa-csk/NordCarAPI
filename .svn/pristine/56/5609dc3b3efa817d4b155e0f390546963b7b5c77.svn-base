using NordCar.Carla.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.Carla.Data.Repository
{
    public interface IWebAPIManagerRepository
    {
        //Function nr 0
        string HelloWorld();

        //Function nr 1
        Tuple<APIMethodControl, List<CarListItem_DON>> GetCarsList(BasicStructure basic, int LocationId); //0=ALL

        //Function nr 2a
        Tuple<APIMethodControl, List<PriceListItem_DON>> GetPriceList(BasicStructure basic, int locationId, string categoryId);

        //Function nr 2b
        Tuple<APIMethodControl, List<PriceListItemExtra_DON>> GetPriceListExtra(BasicStructure basic, int locationId, string categoryId);

        //Function nr 3 (GetAvailabillityList)
        Tuple<APIMethodControl, AvailabillityItem_DON> GetAvaiabillityList(BasicStructure basic, int locationId, int productId, int returnLocationId, string categoryId, string pickupDate, string returnDate, string pickupTime, string returnTime);

        //Function nr 4 (Select Product)
        Tuple<APIMethodControl, List<Product>> SelectProduct(BasicStructure basic, int locationId, int returnLocationId, string pickupDate, string pickupTime, string returnDate, string returnTime, string categoryId, int productId);

        //Function nr 5 (UpdatePrice) multiple price items
        Tuple<APIMethodControl, List<PriceInfo>> UpdatePrice(BasicStructure basic, int locationId, int returnLocationId, string pickupDate, string pickupTime, string returnDate, string returnTime, string categoryId, int productId,
            int ExtraProdId_01, string ExtraProdCurrentNumbUnits_01,
            int ExtraProdId_02, string ExtraProdCurrentNumbUnits_02,
            int ExtraProdId_03, string ExtraProdCurrentNumbUnits_03,
            int ExtraProdId_04, string ExtraProdCurrentNumbUnits_04,
            int ExtraProdId_05, string ExtraProdCurrentNumbUnits_05,
            int ExtraProdId_06, string ExtraProdCurrentNumbUnits_06,
            int ExtraProdId_07, string ExtraProdCurrentNumbUnits_07,
            int ExtraProdId_08, string ExtraProdCurrentNumbUnits_08,
            int ExtraProdId_09, string ExtraProdCurrentNumbUnits_09,
            int ExtraProdId_10, string ExtraProdCurrentNumbUnits_10,
            int ExtraProdId_11, string ExtraProdCurrentNumbUnits_11,
            int ExtraProdId_12, string ExtraProdCurrentNumbUnits_12,
            int ExtraProdId_13, string ExtraProdCurrentNumbUnits_13,
            int ExtraProdId_14, string ExtraProdCurrentNumbUnits_14,
            int ExtraProdId_15, string ExtraProdCurrentNumbUnits_15,
            int ExtraProdId_16, string ExtraProdCurrentNumbUnits_16,
            int ExtraProdId_17, string ExtraProdCurrentNumbUnits_17,
            int ExtraProdId_18, string ExtraProdCurrentNumbUnits_18,
            int ExtraProdId_19, string ExtraProdCurrentNumbUnits_19,
            int ExtraProdId_20, string ExtraProdCurrentNumbUnits_20);

        Tuple<APIMethodControl, List<PriceInfo_DON>> UpdatePrice2(BasicStructure basic, Price2 price);


        //Function nr 6 (GetLocationList)
        Tuple<APIMethodControl, List<Location>> GetLocationList(BasicStructure basic, int locationId);

        //Function nr 7 (Login)
        Tuple<APIMethodControl, User> LoginPrivate(BasicStructure basic, int loginType, string userName, string password);

        Tuple<APIMethodControl, UserCompany> LoginCompany(BasicStructure basic, int loginType, string userName, string password);

        //Function nr 8a (Account Private)
        Tuple<APIMethodControl, Account> AccountPrivate(BasicStructure basic, int customerType, int customerId, string email, string driverLicense, string birthDay, string name, string surname, string address1, string address2, string zipCode, string city, string country, string phone, string mobilePhone, bool newsLetter, bool smsServive);

        //Function nr 8b (Account Company)
        Tuple<APIMethodControl, Account> AccountCompany(BasicStructure basic, int customerType, int customerId, string email, string cvr, string companyname, string address1, string address2, string zipCode, string city, string country, string phone, string mobilePhone, bool newsLetter, bool smsServive, string companyContact, string companyContactInfo);

        //Function nr 9
        Tuple<APIMethodControl, CompanyCustomer> ReturnCompanyCustomerId(BasicStructure basic, string companyName, string companyContactEmail);

        //Function nr 10 (SubmitRental)
        Tuple<APIMethodControl, RentalInfo> SubmitRental(
            BasicStructure basic,
            int locationId,
            int returnLocationId,
            int productId,
            string pickupDate,
            string pickupTime,
            string returnDate,
            string returnTime,
            string categoryId,
            string CoRenterForName,
            string CoRenterSurName,
            string CoRenterDriverLicense,
            string CoRenterBirthDay,
            string RekvisitionNo,
            string PayType,
            string DriverNo1,
            string DriverNo2,
            string DriverNo3,
            string DriverNo4,
            string DriverNo5,
            string DriverNo6,
            string DriverNo7,
            string DriverNo8,
            string DriverNo9,
            string DriverNo10,
            string BookingStatus,
            string RenterName,
            string RenterBirthDay,
            string RenterAddress,
            string RenterZipCodeAndCity,
            string RenterPhoneMobile,
            string REnterEmail,
            int AddOnProdId_01, string AddOnProdCurrentNumbUnits_01,
            int AddOnProdId_02, string AddOnProdCurrentNumbUnits_02,
            int AddOnProdId_03, string AddOnProdCurrentNumbUnits_03,
            int AddOnProdId_04, string AddOnProdCurrentNumbUnits_04,
            int AddOnProdId_05, string AddOnProdCurrentNumbUnits_05,
            int AddOnProdId_06, string AddOnProdCurrentNumbUnits_06,
            int AddOnProdId_07, string AddOnProdCurrentNumbUnits_07,
            int AddOnProdId_08, string AddOnProdCurrentNumbUnits_08,
            int AddOnProdId_09, string AddOnProdCurrentNumbUnits_09,
            int AddOnProdId_10, string AddOnProdCurrentNumbUnits_10,
            int AddOnProdId_11, string AddOnProdCurrentNumbUnits_11,
            int AddOnProdId_12, string AddOnProdCurrentNumbUnits_12,
            int AddOnProdId_13, string AddOnProdCurrentNumbUnits_13,
            int AddOnProdId_14, string AddOnProdCurrentNumbUnits_14,
            int AddOnProdId_15, string AddOnProdCurrentNumbUnits_15,
            int AddOnProdId_16, string AddOnProdCurrentNumbUnits_16,
            int AddOnProdId_17, string AddOnProdCurrentNumbUnits_17,
            int AddOnProdId_18, string AddOnProdCurrentNumbUnits_18,
            int AddOnProdId_19, string AddOnProdCurrentNumbUnits_19,
            int AddOnProdId_20, string AddOnProdCurrentNumbUnits_20);

        //Functionnr. 10b
        Tuple<APIMethodControl, RentalInfo> SubmitRental(BasicStructure basic, Rental rent);

        //Function nr 11
        Tuple<APIMethodControl, List<Booking>> GetBookingList(BasicStructure basic, int logintype, int customerId);

        //Function nr 12
        Tuple<APIMethodControl, bool> CancelRental(BasicStructure basic, int bookingId);

        //Function nr 13 venter

        //Function nr 14
        Tuple<APIMethodControl, Defaults> GetAddDefaults(BasicStructure basic, int addId);

        //Function nr 15
        Tuple<APIMethodControl, FrontPageDefault_DON> GetFrontPageDefault(BasicStructure basic);

        //Function nr 16
        Tuple<APIMethodControl, List<CompanyDriverItem>> UpdateCompanyDrivers(BasicStructure basic, int subFunction, int customerId, string driverName, string driverSurName, string driverBirthDate, string driverLicense);

        //Function nr 17
        Tuple<APIMethodControl, DibsResultItem> DibsResult(BasicStructure basic, int bookingId, int paymentFlag, int paymentType, int paymentCode, string paymentAmount, int depositPaymentCode, string depositPaymentAmount);

        //Function nr 18
        Tuple<APIMethodControl, InvalidDateItem> GetInvalidPickupDatas(BasicStructure basic, int locationId, string categoryId, string pickupYear, string pickupMonth);

        //Function nr 19
        Tuple<APIMethodControl, InvalidDateItem> GetInvalidReturnDates(BasicStructure basic, int locationId, string categoryId, string returnYear, string returnMonth, string pickupDate);

        //Function nr 20
        Tuple<APIMethodControl, OpenHours> GetOpenHours(BasicStructure basic, int LocationId, string date, int isPickupDate);

        //Function nr. 21
        Tuple<APIMethodControl, List<PromotionInfo>> PromotionUpdate(BasicStructure basic, int locationId, int returnLocationId, string pickupDate, string pickupTime, string returnDate, string returnTime, string categoryId, int productId,
         int ExtraProdId_01, string ExtraProdCurrentNumbUnits_01,
         int ExtraProdId_02, string ExtraProdCurrentNumbUnits_02,
         int ExtraProdId_03, string ExtraProdCurrentNumbUnits_03,
         int ExtraProdId_04, string ExtraProdCurrentNumbUnits_04,
         int ExtraProdId_05, string ExtraProdCurrentNumbUnits_05,
         int ExtraProdId_06, string ExtraProdCurrentNumbUnits_06,
         int ExtraProdId_07, string ExtraProdCurrentNumbUnits_07,
         int ExtraProdId_08, string ExtraProdCurrentNumbUnits_08,
         int ExtraProdId_09, string ExtraProdCurrentNumbUnits_09,
         int ExtraProdId_10, string ExtraProdCurrentNumbUnits_10,
         int ExtraProdId_11, string ExtraProdCurrentNumbUnits_11,
         int ExtraProdId_12, string ExtraProdCurrentNumbUnits_12,
         int ExtraProdId_13, string ExtraProdCurrentNumbUnits_13,
         int ExtraProdId_14, string ExtraProdCurrentNumbUnits_14,
         int ExtraProdId_15, string ExtraProdCurrentNumbUnits_15,
         int ExtraProdId_16, string ExtraProdCurrentNumbUnits_16,
         int ExtraProdId_17, string ExtraProdCurrentNumbUnits_17,
         int ExtraProdId_18, string ExtraProdCurrentNumbUnits_18,
         int ExtraProdId_19, string ExtraProdCurrentNumbUnits_19,
         int ExtraProdId_20, string ExtraProdCurrentNumbUnits_20);

        Tuple<APIMethodControl, List<PromotionInfo>> PromotionUpdate2(BasicStructure basic, Price2 price);

        //Function nr 22
        Tuple<APIMethodControl, byte[]> PDF(BasicStructure basic, int pdfType, string reservationNo, string email);

        //Function nr 23
        Tuple<APIMethodControl, string> ReturnProductDefs(BasicStructure basic);
    }

    
}