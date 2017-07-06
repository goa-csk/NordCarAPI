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

namespace NordCar.WebAPI.Tests.Controllers
{
    [TestClass]
    public class PSAPIControllerTest
    {
        private PSAPIController controller;

        [TestInitialize]
        public void init()
        {
            //Arrange
            string ip = "192.168.80.9";
            int port = 1074;
            string logfile = "test.log";

            var rep = new PSAPIManagerRepository(ip, port, logfile);
            controller = new PSAPIController(rep);
            controller.Request = new HttpRequestMessage();
        }

        #region GetCarList 
        /// <summary>
        /// 01
        /// </summary>
        [TestMethod]
        public void GetCarlist_All()
        {
            int locationId = 0;
            //Act
            var response = controller.GetCarlist(locationId);
            //Assert
            Assert.IsNotNull(response);
        }

        /// <summary>
        /// 01
        /// </summary>
        [TestMethod]
        public void GetCarlist_53()
        {
            int locationId = 53;

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
            int locationId = 53;
            //Act
            var response = controller.GetPriceList(locationId,"o",false);
            //Assert
            Assert.IsNotNull(response);
        }
        /// <summary>
        /// 02
        /// </summary>
        [TestMethod]
        public void GetPriceList_extra()
        {
            //Note: Returnere ingenting?, bruges ikke pt.
            int locationId = 66;
            //Act
            var response = controller.GetPriceList(locationId, "A", true);
            //Assert
            Assert.IsNotNull(response);
        }
        #endregion

        #region GetAvailabillityList
        /// <summary>
        /// 03
        /// </summary>
        [TestMethod]
        public void GetAvailabillityList()
        {
            //http://localhost:52833/api/PSAPI/GetAvaiabillityList?locationId=53&productId=830&returnLocationId=53&categoryId=A&pickupDate=09012015&returnDate=11012015&pickupTime=0700&returnTime=0700&age=26
            int locationId = 53;
            int productId = 830;
            int returnlocationId = 53;
            string categoryId = "A";
            string pickupDate = convertDateTimeToCarlaDateTime(DateTime.Now.AddDays(2));
            string pickupTime = "0700";
            string returnDate = convertDateTimeToCarlaDateTime(DateTime.Now.AddDays(3));
            string returnTime = "0700";
            int age = 26;
            //Act
            var response = controller.GetAvaiabillityList(locationId, productId, returnlocationId, categoryId, pickupDate, returnDate, pickupTime, returnTime, age);
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
            //Se availabillitylist.
            var extra = new List<ExtraProduct>(){new ExtraProduct(){id=1, numbUnit = ""}};

            var price = new Price2() { locationId = 53, returnLocationId = 53, pickupDate = convertDateTimeToCarlaDateTime(DateTime.Now), pickupTime = "0700", returnDate = convertDateTimeToCarlaDateTime(DateTime.Now.AddDays(14)), returnTime = "0700", categoryId = "A", productId = 830, extras = extra };

            //Act
            var response = controller.UpdatePrice(price);

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
            var drivers = new List<Driver2>();

            drivers.Add(new Driver2() { name = "James-0" });
            drivers.Add(new Driver2() { name = "James-1" });
            drivers.Add(new Driver2() { name = "James-2" });

            var extras = new List<ExtraProduct>();

            extras.Add(new ExtraProduct() { id = 1, numbUnit = "1" });
            extras.Add(new ExtraProduct() { id = 2, numbUnit = "2" });
            extras.Add(new ExtraProduct() { id = 3, numbUnit = "3" });

            var rental = new Rental_PS() { 
                locationId = 53,
                returnLocationId = 53,
                productId = 1,
                pickupDate = convertDateTimeToCarlaDateTime(DateTime.Now.AddDays(1)),
                pickupTime = "0800",
                returnDate = convertDateTimeToCarlaDateTime(DateTime.Now.AddDays(1)),
                returnTime = "0700",
                categoryId = "A",
                coRenterName = "Bonnie",
                coRenterSurName = "Raith",
                coRenterLicenseNo = "012334560123456",
                coRenterBirthDay = "02021996",
                rekvisitionNo = "blank",
                payType = "1",
                drivers = drivers,
                bookStatus = "1",
                renterName = "Clyde Raith",
                renterBirthDay = "03051996",
                renterAddress = "Don Johnson Drive 12th",
                renterZipCity = "600 Miami",
                renterPhone = "00812563489",
                renterEmail = "donjohnson@vice.com",
                extras = extras };


            //Act
            var response = controller.SubmitRental(rental);
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

        #region DibsResult
        /// <summary>
        /// 17
        /// </summary>
        [TestMethod]
        public void DibsResult()
        {
            int SubmitRentalReservationsNo = 1002; //Booking Id
            int PAYMENTFLAG = 0;
            int paymenttype = 0;
            int paymentCode = 0; 
            int paymentAmount = 0;
            int depositPaymentCode = 0;
            int depositPaymentamount = 0;

            //Act
            var response = controller.DibsResult(SubmitRentalReservationsNo,
                PAYMENTFLAG, paymenttype, paymentCode, paymentAmount, depositPaymentCode, depositPaymentamount);
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
            var extra = new List<ExtraProduct>() { new ExtraProduct() { id = 1, numbUnit = "" } };

            var price = new Price2() { locationId = 53, returnLocationId = 53, pickupDate = convertDateTimeToCarlaDateTime(DateTime.Now), pickupTime = "0800", returnDate = convertDateTimeToCarlaDateTime(DateTime.Now.AddDays(4)), returnTime = "0700", categoryId = "3", productId = 1, extras = extra };

            //Act
            var response = controller.PromotionUpdate(price);

            //Assert
            Assert.IsNotNull(response);
        }
        #endregion

        private string convertDateTimeToCarlaDateTime(DateTime dtstr)
        {
            return dtstr.ToString("ddMMyyyy");
        }
    }
}
