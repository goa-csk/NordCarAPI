using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NordCar.WebAPI;
using NordCar.WebAPI.Controllers;
using NordCar.Carla.Data.Implementation;
using NordCar.WebAPI.Models;
using System.Web.Http.Results;

namespace NordCar.WebAPI.Tests.Controllers
{
    /// <summary>
    /// DON
    /// </summary>
    [TestClass]
    public class WebAPIControllerTest
    {
        private WebAPIController controller;

        [TestInitialize]
        public void init()
        {
            //Arrange
            string ip = "192.168.80.9";
            int port = 1074;
            string logfile = "log47.log";

            var rep = new WebAPIManagerRepository(ip, port, logfile);
            controller = new WebAPIController(rep);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
        }

        #region GetCarList
        /// <summary>
        /// 01
        /// </summary>
        [TestMethod]
        public void GetCarlist()
        {
            int locationId = 0;
            
            //Act
            var response = controller.GetCarlist(locationId);

            //Assert
            Assert.IsNotNull(response);
        }
        #endregion

        #region GetPriceList
        /// <summary>
        /// 02
        /// </summary>
        [TestMethod]
        public void GetPriceList()
        {
            int locationId = 66;
            //Act
            var response = controller.GetPriceList(locationId, "A", false);
            //Assert
            Assert.IsNotNull(response);
        }
        /// <summary>
        /// 02
        /// </summary>
        [TestMethod]
        public void GetPriceList_extra()
        {
            int locationId = 66;
            //Act
            var response = controller.GetPriceList(locationId, "A", true);
            //Assert
            Assert.IsNotNull(response);
        }
        #endregion

        #region GetAvailabilityList
        /// <summary>
        /// 03
        /// </summary>
        [TestMethod]
        public void GetAvailabilityList()
        {
            int locationId = 66;
            int productId = 180; //1 dag
            int returnlocationId = 66;
            string categoryId = "A";
            string pickupDate = convertDateTimeToCarlaDateTime(DateTime.Now.AddDays(10));
            string pickupTime = "0700";
            string returnDate = convertDateTimeToCarlaDateTime(DateTime.Now.AddDays(11));
            string returnTime = "0700";
            //Act
            var response = controller.GetAvaiabillityList(locationId, productId, returnlocationId, categoryId, pickupDate, returnDate, pickupTime, returnTime);
            //Assert
            Assert.IsNotNull(response);

        }
        #endregion

        #region SelectProduct
        /// <summary>
        /// 04
        /// </summary>
        [TestMethod]
        public void SelectProduct()
        {
            int locationId = 66;
            int productId = 180; //1 dag
            int returnlocationId = 66;
            string categoryId = "A";
            string pickupDate = convertDateTimeToCarlaDateTime(DateTime.Now.AddDays(10));
            string pickupTime = "0700";
            string returnDate = convertDateTimeToCarlaDateTime(DateTime.Now.AddDays(11));
            string returnTime = "0700";
            //Act
            var response = controller.SelectProduct(locationId, returnlocationId, pickupDate, pickupTime, returnDate, returnTime, categoryId, productId);
            //Assert
            Assert.IsNotNull(response);
        }
        #endregion

        #region UpdatePrice
        /// <summary>
        /// 05
        /// </summary>
        [TestMethod]
        public void UpdatePrice()
        {
            int locationId = 66;
            int returnlocationId = 66;
            int productId = 180; //1 dag
            string categoryId = "A";
            string pickupDate = convertDateTimeToCarlaDateTime(DateTime.Now.AddDays(10));
            string pickupTime = "0700";
            string returnDate = convertDateTimeToCarlaDateTime(DateTime.Now.AddDays(11));
            string returnTime = "0700";
            
            var extra = new List<ExtraProduct>(){new ExtraProduct(){id=1, numbUnit = ""}};

            var price = new Price2() { locationId = locationId, returnLocationId = returnlocationId, pickupDate = pickupDate, pickupTime = pickupTime, returnDate = returnDate, returnTime = returnTime, categoryId = categoryId, productId = productId, extras = extra };

            //Act
            var response = controller.UpdatePrice(price);

            //Assert
            Assert.IsNotNull(response);
        }
        #endregion

        #region GetLocationList
        /// <summary>
        /// 06
        /// </summary>
        [TestMethod]
        public void GetLocationList()
        {
            int locationId = 66;
            //Act
            var response = controller.GetLocationList(locationId);
            //Assert
            Assert.IsNotNull(response);
        }
        #endregion

        #region Login
        /// <summary>
        /// 07
        /// </summary>
        [TestMethod]
        public void Login_private()
        {
            int logintype = 1;
            //Act
            var response = controller.Login(logintype, "12121235", "09011962");
            //Assert
            Assert.IsNotNull(response);
        }

        /// <summary>
        /// 07
        /// </summary>
        [TestMethod]
        public void Login_company()
        {
            int logintype = 2;
            //Act
            var response = controller.Login(logintype, "driveonnet", "404485");
            //Assert
            Assert.IsNotNull(response);
        }
        #endregion

        #region Account
        /// <summary>
        /// 08
        /// </summary>
        [TestMethod]
        public void Account_Private()
        {
            int customerType = 1; //Private 
            int customerId = 0; //Create 
            string email = "csk@goapplicate.com"; 
            string driverLicense = "12121237"; 
            string birthDay = "08021971"; 
            string name = "Claus"; 
            string surname = "Skydt"; 
            string address1 = "Nattegalevej 121"; 
            string address2 = ""; 
            string zipCode = "8464"; 
            string city = "Galten"; 
            string country = "Danmark"; 
            string phone = "40223481"; 
            string mobilePhone = "40223482"; 
            bool newsLetter = false; 
            bool smsServive = false;

            //Act
            var response = controller.PrivateAccount(customerType, customerId, email, driverLicense, birthDay, name, surname, address1, address2, zipCode, city, country, phone, mobilePhone, newsLetter, smsServive);
            //Assert
            Assert.IsNotNull(response);
        }

        /// <summary>
        /// 08
        /// </summary>
        [TestMethod]
        public void Account_Company()
        {
            int customerType = 2; //Company 
            int customerId = 0; //Create 
            string email = "csk@goapplicate.com";
            string cvr = "123456789";
            string companyName = "Claus & Co";
            string address1 = "Nattegalevej 123";
            string address2 = "";
            string zipCode = "8464";
            string city = "Galten";
            string country = "Danmark";
            string phone = "40223483";
            string mobilePhone = "40223484";
            bool newsLetter = false;
            bool smsServive = false;
            string companyContact = "";
            string companyContactInfo = "";

            //Act
            var response = controller.CompanyAccount(customerType, customerId, email,cvr, companyName, address1, address2, zipCode, city, country, phone, mobilePhone, newsLetter, smsServive,companyContact,companyContactInfo);
            //Assert
            Assert.IsNotNull(response);
        }
        #endregion

        #region ReturnCompanyCustomerId
        /// <summary>
        /// 09
        /// </summary>
        [TestMethod]
        public void ReturnCompanyCustomerId()
        {
            string CompanyName =  "driveonnet";
            string CompanyContactEmail = "jbs@driveon.net";

            //Act
            var response = controller.ReturnCompanyCustomerId(CompanyName,CompanyContactEmail);
            
            //Assert
            Assert.IsNotNull(response);
        }
        #endregion

        #region SubmitRental
        /// <summary>
        /// 10
        /// </summary>
        [TestMethod]
        public void SubmitRental()
        {
            
            int custId = 404485;
            
            var drivers = new List<Driver2>();

            drivers.Add(new Driver2() { name = "James-0" });
            drivers.Add(new Driver2() { name = "James-1" });
            drivers.Add(new Driver2() { name = "James-2" });

            var extras = new List<ExtraProduct>();

            extras.Add(new ExtraProduct() { id = 1, numbUnit = "1" });
            extras.Add(new ExtraProduct() { id = 2, numbUnit = "2" });
            extras.Add(new ExtraProduct() { id = 3, numbUnit = "3" });

            var rental = new Rental()
            {
                locationId = 66,
                returnLocationId = 66,
                productId = 180,
                pickupDate = convertDateTimeToCarlaDateTime(DateTime.Now.AddDays(10)),
                pickupTime = "0800",
                returnDate = convertDateTimeToCarlaDateTime(DateTime.Now.AddDays(11)),
                returnTime = "0800",
                categoryId = "A",
                coRenterName = "Bonnie",
                coRenterSurName = "Raith",
                coRenterLicenseNo = "012334560123456",
                coRenterBirthDay = "02021996",
                rekvisitionNo = "blank",
                payType = "2",
                drivers = drivers,
                extras = extras
            };

            //Act
            var response = controller.SubmitRental(custId,rental);
            //Assert
            Assert.IsNotNull(response);
        }
        #endregion

        #region GetBookingList
        /// <summary>
        /// 11
        /// </summary>
        [TestMethod]
        public void GetBookingList()
        {
            int loginType = 2; //Company
            int customerId = 404485;

            //Act
            var response = controller.GetBookingList(loginType,customerId);

            //Assert
            Assert.IsNotNull(response);
        }
        #endregion

        #region CancelRental 
        /// <summary>
        /// 12
        /// </summary>
        [TestMethod]
        public void CancelRental()
        {
            int bookingId = 26642738; //Reservation No
            

            //Act
            var response = controller.CancelRental(bookingId);

            //Assert
            Assert.IsNotNull(response);
        }
        #endregion

        #region GetAddDefaults
        /// <summary>
        /// 14
        /// </summary>
        [TestMethod]
        public void GetAddDefaults()
        {
            int addId = 0; 


            //Act
            var response = controller.GetAddDefaults(addId);

            //Assert
            Assert.IsNotNull(response);
        }
        #endregion

        #region GetFrontPageDefault
        /// <summary>
        /// 15
        /// </summary>
        [TestMethod]
        public void GetFrontPageDefaults()
        {
            //Act
            var response = controller.GetFrontPageDefault();
            //Assert
            Assert.IsNotNull(response);
        }
        #endregion

        #region UpdateCompanyDrivers
        /// <summary>
        /// 16
        /// </summary>
        [TestMethod]
        public void UpdateCompanyDrivers()
        {
            //kun subfunc= 2 implementeret.
            int subFunction = 2;
            int customerId = 404485;
            string driverName = "Santa"; 
            string driverSurName = "Claus";
            string driverBirthDate = "02031972";
            string driverLicense = "1234123456";
            
            //Act
            var response = controller.UpdateCompanyDrivers(subFunction,customerId,driverName,driverSurName,driverBirthDate,driverLicense);
            //Assert
            Assert.IsNotNull(response);
        }
        #endregion 

        #region DibsResult
        /// <summary>
        /// 17
        /// </summary>
        [TestMethod]
        public void DibsResult()
        {
            int custId = 404485;

            var drivers = new List<Driver2>();

            drivers.Add(new Driver2() { name = "James-0" });
            drivers.Add(new Driver2() { name = "James-1" });
            drivers.Add(new Driver2() { name = "James-2" });

            var extras = new List<ExtraProduct>();

            extras.Add(new ExtraProduct() { id = 1, numbUnit = "1" });
            extras.Add(new ExtraProduct() { id = 2, numbUnit = "2" });
            extras.Add(new ExtraProduct() { id = 3, numbUnit = "3" });

            var rental = new Rental()
            {
                locationId = 66,
                returnLocationId = 66,
                productId = 180,
                pickupDate = convertDateTimeToCarlaDateTime(DateTime.Now.AddDays(10)),
                pickupTime = "0800",
                returnDate = convertDateTimeToCarlaDateTime(DateTime.Now.AddDays(11)),
                returnTime = "0800",
                categoryId = "A",
                coRenterName = "Bonnie",
                coRenterSurName = "Raith",
                coRenterLicenseNo = "012334560123456",
                coRenterBirthDay = "02021996",
                rekvisitionNo = "blank",
                payType = "2",
                drivers = drivers,
                extras = extras
            };

            
            IHttpActionResult actionResult = controller.SubmitRental(custId, rental);
            var contentResult = actionResult as OkNegotiatedContentResult<RentalInfo>;
            
            int SubmitRentalReservationsNo = int.Parse(contentResult.Content.ReservationNo);
            int PAYMENTFLAG = 0;
            int paymenttype = 2;
            int paymentCode = 0; //First dibs reply
            string paymentAmount = contentResult.Content.TotalPrice;
            int depositPaymentCode = 0; //Second dibs reply
            string depositPaymentamount = contentResult.Content.DepositOnline; 

            //Act
            var response = controller.DibsResult(SubmitRentalReservationsNo, PAYMENTFLAG, paymenttype, paymentCode, paymentAmount, depositPaymentCode, depositPaymentamount);
            //Assert
            Assert.IsNotNull(response);
        }
        #endregion 

        #region GetInvalidPickupDates
        /// <summary>
        /// 18
        /// </summary>
        [TestMethod]
        public void GetInvalidPickupDates()
        {
            
            int locationId = 66;
            string categoryId = "A";
            string pickupYear = DateTime.Now.Year.ToString();
            string pickupMonth = DateTime.Now.Month.ToString("MM");
            
            //Act
            var response = controller.GetInvalidPickupDates(locationId,categoryId,pickupYear,pickupMonth);
            //Assert
            Assert.IsNotNull(response);
        }
        #endregion 

        #region GetInvalidReturnDates
        /// <summary>
        /// 19
        /// </summary>
        [TestMethod]
        public void GetInvalidReturnDates()
        {

            int locationId = 66;
            string categoryId = "A";
            string returnYear = DateTime.Now.Year.ToString();
            string returnMonth = DateTime.Now.Month.ToString("MM");
            string pickupDate = convertDateTimeToCarlaDateTime(DateTime.Now);
              
            //Act
            var response = controller.GetInvalidReturnDates(locationId, categoryId, returnYear, returnMonth, pickupDate);
            //Assert
            Assert.IsNotNull(response);
        }
        #endregion

        #region GetOpenHours
        /// <summary>
        /// 20
        /// </summary>
        [TestMethod]
        public void GetOpenHours()
        {

            int locationId = 66;
            string pickupDate = convertDateTimeToCarlaDateTime(DateTime.Now);
            int isPickupDate = 1;
            //Act
            var response = controller.GetOpenHours(locationId, pickupDate, isPickupDate);
            //Assert
            Assert.IsNotNull(response);
        }
        #endregion 

        #region PromotionUpdate
        /// <summary>
        /// 21
        /// </summary>
        [TestMethod]
        public void PromotionUpdate()
        {
            string customerId = "404485";
            string orgBooking = "";
            string voucherCode = "Rabatkode";
            int locationId = 66;
            int returnlocationId = 66;
            int productId = 180; //1 dag
            string categoryId = "A";
            string pickupDate = convertDateTimeToCarlaDateTime(DateTime.Now.AddDays(10));
            string pickupTime = "0800";
            string returnDate = convertDateTimeToCarlaDateTime(DateTime.Now.AddDays(11));
            string returnTime = "0800";

            var extra = new List<ExtraProduct>() { new ExtraProduct() { id = 1, numbUnit = "" } };

            var price = new Price2() { locationId = locationId, returnLocationId = returnlocationId, pickupDate = pickupDate, pickupTime = pickupTime, returnDate = returnDate, returnTime = returnTime, categoryId = categoryId, productId = productId, extras = extra };


            //Act
            var response = controller.PromotionUpdate(customerId, orgBooking, voucherCode, price);

            //Assert
            Assert.IsNotNull(response);
        }
        #endregion

        private string convertDateTimeToCarlaDateTime(DateTime dtstr)
        {
            return dtstr.ToString("ddMMyyyy");
        }

    //    [TestMethod]
    //    public void Get()
    //    {
    //        // Arrange
    //        ValuesController controller = new ValuesController();

    //        // Act
    //        IEnumerable<string> result = controller.Get();

    //        // Assert
    //        Assert.IsNotNull(result);
    //        Assert.AreEqual(2, result.Count());
    //        Assert.AreEqual("value1", result.ElementAt(0));
    //        Assert.AreEqual("value2", result.ElementAt(1));
    //    }

    //    [TestMethod]
    //    public void GetById()
    //    {
    //        // Arrange
    //        ValuesController controller = new ValuesController();

    //        // Act
    //        string result = controller.Get(5);

    //        // Assert
    //        Assert.AreEqual("value", result);
    //    }

    //    [TestMethod]
    //    public void Post()
    //    {
    //        // Arrange
    //        ValuesController controller = new ValuesController();

    //        // Act
    //        controller.Post("value");

    //        // Assert
    //    }

    //    [TestMethod]
    //    public void Put()
    //    {
    //        // Arrange
    //        ValuesController controller = new ValuesController();

    //        // Act
    //        controller.Put(5, "value");

    //        // Assert
    //    }

    //    [TestMethod]
    //    public void Delete()
    //    {
    //        // Arrange
    //        ValuesController controller = new ValuesController();

    //        // Act
    //        controller.Delete(5);

    //        // Assert
    //    }
    }
}
