using NordCar.WebAPI.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NordCar.Carla.Data.Repository;
using AutoMapper;
using NordCar.WebAPI.BootStrapper;
using NordCar.WebAPI.Models;
using NordCar.WebAPI.Models.User;
using System.Web.Http.Cors;
using System.Web;
using NordCar.WebAPI.Filter;
using System.Web.Http.Description;
using System.Net.Http.Headers;

namespace NordCar.WebAPI.Controllers
{
   //[EnableCors(origins: "http://localhost:52832/", headers: "*", methods: "*")]

    /// <summary>
    /// DON
    /// </summary>
    public class WebAPIController : BaseAPIController
    {

       
        /// <summary>
        /// Initialise a variable of IContactsManagerRepository from data layer
        /// </summary>
        protected readonly IWebAPIManagerRepository WebAPIManagerRepository;

         /// <summary>
        /// Inject repository
        /// </summary>
        /// <param name="_repository">IWebAPIManagerRepository</param>
        public WebAPIController(IWebAPIManagerRepository _repository)
        {

            if (_repository == null)
            {
                throw new ArgumentNullException("WebAPIManager Repository exception");
            }

            this.WebAPIManagerRepository = _repository;

            var bs = new Bootstrapper();
            bs.Initialize();
        }

        

        /// <summary>
        /// Is Carla program 7319 running.
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetHelloWorld()
        {
           
            if (WebAPIManagerRepository.HelloWorld() == string.Empty)
            {
                return NotFound();
                
            }

            return Ok(WebAPIManagerRepository.HelloWorld());
        }

        #region 01 GetCarList
        /// <summary>
        /// GetCarList
        /// </summary>
        /// <param name="locationId">Id of a valid location, if 0 then return all locations</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(CarListItem_DON))]
        public IHttpActionResult GetCarlist(int locationId = 0)
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.WebAPIManagerRepository.GetCarsList(bs, locationId);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<List<NordCar.Carla.Data.Entities.CarListItem_DON>, List<CarListItem_DON>>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1),HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region 02 GetPriceList
        /// <summary>
        /// GetPriceList
        /// </summary>
        /// <param name="locationId">Id of a valid location, if 0 then return all locations</param>
        /// <param name="categoryId">if 0 then return all catagories</param>
        /// <param name="extra">if true returns a list of  extra components instead</param>
        /// <returns>PriceListItem_DON or PriceListItemExtra_DON</returns>
        [HttpGet]
        [ResponseType(typeof(PriceListItem_DON))]
        public IHttpActionResult GetPriceList(int locationId = 0, string categoryId = "", bool extra = true)
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            if (extra)
            {
                var data = this.WebAPIManagerRepository.GetPriceListExtra(bs, locationId,categoryId);

                if (data.Item1.Succes)
                {
                    return Ok(Mapper.Map<List<NordCar.Carla.Data.Entities.PriceListItemExtra_DON>, List<PriceListItemExtra_DON>>(data.Item2));
                }
                else
                {
                    return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1),HttpStatusCode.NotFound);
                }
            }
            else
            {
                var data = this.WebAPIManagerRepository.GetPriceList(bs, locationId, categoryId);

                if (data.Item1.Succes)
                {
                    return Ok(Mapper.Map<List<NordCar.Carla.Data.Entities.PriceListItem_DON>, List<PriceListItem_DON>>(data.Item2));
                }
                else
                {
                    return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1),HttpStatusCode.NotFound);
                }


            }
        }
        #endregion

        #region 03 GetAvaiabillityList
        /// <summary>
        /// GetAvaiabillityList
        /// </summary>
        /// <param name="locationId">Id of a valid location, if 0 then return all locations</param>
        /// <param name="productId">Specific productId, if 0 the return all products</param>
        /// <param name="returnLocationId">should be the same as locationid</param>
        /// <param name="categoryId">"A" or "3"</param>
        /// <param name="pickupDate">DATE</param>
        /// <param name="returnDate">DATE</param>
        /// <param name="pickupTime">TIME</param>
        /// <param name="returnTime">TIME</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(AvailabillityItem_DON))]
        public IHttpActionResult GetAvaiabillityList(int locationId = 0, int productId = 0, int returnLocationId = 0, string categoryId = "A", string pickupDate = "", string returnDate = "", string pickupTime = "", string returnTime = "")
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
                var data = this.WebAPIManagerRepository.GetAvaiabillityList(bs, locationId, productId, returnLocationId, categoryId, pickupDate, returnDate, pickupTime, returnTime);

                if (data.Item1.Succes)
                {
                    return Ok(Mapper.Map<NordCar.Carla.Data.Entities.AvailabillityItem_DON, AvailabillityItem_DON>(data.Item2));
                }
                else
                {
                    return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1),HttpStatusCode.NotFound);
                }
            
        }
        #endregion

        #region 04 Select Product
        /// <summary>
        /// SelectProduct
        /// </summary>
        /// <param name="locationId">Id of a valid location, if 0 then return all locations</param>
        /// <param name="returnLocationId">Should be same as locationId</param>
        /// <param name="pickupDate">DATE</param>
        /// <param name="pickupTime">TIME</param>
        /// <param name="returnDate">DATE</param>
        /// <param name="returnTime">TIME</param>
        /// <param name="categoryId">"A" or "3"</param>
        /// <param name="productId">Which product to get extra list for</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(Product))]
        public IHttpActionResult SelectProduct(int locationId = 0, int returnLocationId = 0, string pickupDate = "", string pickupTime = "", string returnDate = "", string returnTime = "", string categoryId = "", int productId = 0)
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.WebAPIManagerRepository.SelectProduct(bs, locationId, returnLocationId, pickupDate, pickupTime, returnDate, returnTime, categoryId, productId);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<List<NordCar.Carla.Data.Entities.Product>, List<Product>>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1),HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region 05 Update Price
      
        //[HttpGet]
        //public IHttpActionResult UpdatePrice(int locationId = 0, int returnLocationId = 0, string pickupDate = "", string pickupTime = "", string returnDate = "", string returnTime = "", string categoryId = "", int productId = 0,
        //    int ExtraProdId_01 = 0, string ExtraProdCurrentNumbUnits_01 = "",
        //    int ExtraProdId_02 = 0, string ExtraProdCurrentNumbUnits_02 = "",
        //    int ExtraProdId_03 = 0, string ExtraProdCurrentNumbUnits_03 = "",
        //    int ExtraProdId_04 = 0, string ExtraProdCurrentNumbUnits_04 = "",
        //    int ExtraProdId_05 = 0, string ExtraProdCurrentNumbUnits_05 = "",
        //    int ExtraProdId_06 = 0, string ExtraProdCurrentNumbUnits_06 = "",
        //    int ExtraProdId_07 = 0, string ExtraProdCurrentNumbUnits_07 = "",
        //    int ExtraProdId_08 = 0, string ExtraProdCurrentNumbUnits_08 = "",
        //    int ExtraProdId_09 = 0, string ExtraProdCurrentNumbUnits_09 = "",
        //    int ExtraProdId_10 = 0, string ExtraProdCurrentNumbUnits_10 = "",
        //    int ExtraProdId_11 = 0, string ExtraProdCurrentNumbUnits_11 = "",
        //    int ExtraProdId_12 = 0, string ExtraProdCurrentNumbUnits_12 = "",
        //    int ExtraProdId_13 = 0, string ExtraProdCurrentNumbUnits_13 = "",
        //    int ExtraProdId_14 = 0, string ExtraProdCurrentNumbUnits_14 = "",
        //    int ExtraProdId_15 = 0, string ExtraProdCurrentNumbUnits_15 = "",
        //    int ExtraProdId_16 = 0, string ExtraProdCurrentNumbUnits_16 = "",
        //    int ExtraProdId_17 = 0, string ExtraProdCurrentNumbUnits_17 = "",
        //    int ExtraProdId_18 = 0, string ExtraProdCurrentNumbUnits_18 = "",
        //    int ExtraProdId_19 = 0, string ExtraProdCurrentNumbUnits_19 = "",
        //    int ExtraProdId_20 = 0, string ExtraProdCurrentNumbUnits_20 = "")
        //{
        //    var bs1 = fillbasics(FunctionList.UpdatePrice, BookTypes.PSBOOK);
        //    var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
        //    var data = this.WebAPIManagerRepository.UpdatePrice(bs, locationId, returnLocationId, pickupDate, pickupTime, returnDate, returnTime, categoryId, productId,
        //          ExtraProdId_01,  ExtraProdCurrentNumbUnits_01,
        //     ExtraProdId_02,  ExtraProdCurrentNumbUnits_02,
        //     ExtraProdId_03,  ExtraProdCurrentNumbUnits_03,
        //     ExtraProdId_04,  ExtraProdCurrentNumbUnits_04,
        //     ExtraProdId_05,  ExtraProdCurrentNumbUnits_05,
        //     ExtraProdId_06,  ExtraProdCurrentNumbUnits_06,
        //     ExtraProdId_07,  ExtraProdCurrentNumbUnits_07,
        //     ExtraProdId_08,  ExtraProdCurrentNumbUnits_08,
        //     ExtraProdId_09,  ExtraProdCurrentNumbUnits_09,
        //     ExtraProdId_10,  ExtraProdCurrentNumbUnits_10,
        //     ExtraProdId_11,  ExtraProdCurrentNumbUnits_11,
        //     ExtraProdId_12,  ExtraProdCurrentNumbUnits_12,
        //     ExtraProdId_13,  ExtraProdCurrentNumbUnits_13,
        //     ExtraProdId_14,  ExtraProdCurrentNumbUnits_14,
        //     ExtraProdId_15,  ExtraProdCurrentNumbUnits_15,
        //     ExtraProdId_16,  ExtraProdCurrentNumbUnits_16,
        //     ExtraProdId_17,  ExtraProdCurrentNumbUnits_17,
        //     ExtraProdId_18,  ExtraProdCurrentNumbUnits_18,
        //     ExtraProdId_19,  ExtraProdCurrentNumbUnits_19,
        //     ExtraProdId_20,  ExtraProdCurrentNumbUnits_20);

        //    if (data.Item1.Succes)
        //    {
        //        return Ok(Mapper.Map<List<NordCar.Carla.Data.Entities.PriceInfo>, List<PriceInfo>>(data.Item2));
        //    }
        //    else
        //    {
        //        return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1),HttpStatusCode.NotFound);
        //    }

           
        //}
        /// <summary>
        /// UpdatePrice
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(PriceInfo_DON))]
        public IHttpActionResult UpdatePrice(Price2 price)
        { 
            //first no binding to Carla
            //return CreatedAtRoute("DefaultApi", new { id = price.productId }, price);

            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var price1 = Mapper.Map<Price2, NordCar.Carla.Data.Entities.Price2>(price);
            var data = this.WebAPIManagerRepository.UpdatePrice2(bs, price1); 

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<List<NordCar.Carla.Data.Entities.PriceInfo_DON>, List<PriceInfo_DON>>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }

        }

        #endregion

        #region 06 GetLocationList

        /// <summary>
        /// GetLocationList
        /// </summary>
        /// <param name="locationId">Id of a valid location, if 0 then return all locations</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(Location))]
        public IHttpActionResult GetLocationList(int locationId)
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.WebAPIManagerRepository.GetLocationList(bs, locationId);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<List<NordCar.Carla.Data.Entities.Location>, List<Location>>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1),HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region 07 Login
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginType">1=private, 2=company</param>
        /// <param name="username">1) driver license, 2) company name</param>
        /// <param name="password">1) birthdate (DATE), 2) customer no.</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(User))]
        public IHttpActionResult Login(int loginType = 1, string username = "", string password = "")
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            if (loginType == 1)
            {
                var data = this.WebAPIManagerRepository.LoginPrivate(bs, loginType, username, password);

                if (data.Item1.Succes)
                {
                    return Ok(Mapper.Map<NordCar.Carla.Data.Entities.User, User>(data.Item2));
                }
                else
                {
                    return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
                }
            }
            else
            {
                var data = this.WebAPIManagerRepository.LoginCompany(bs, loginType, username, password);

                if (data.Item1.Succes)
                {
                    return Ok(Mapper.Map<NordCar.Carla.Data.Entities.UserCompany, UserCompany>(data.Item2));
                }
                else
                {
                    return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
                }
            
            }
        }
        #endregion

        #region 08 Accounts
        /// <summary>
        /// Private Accounts
        /// </summary>
        /// <param name="customerType"></param>
        /// <param name="customerId"></param>
        /// <param name="email"></param>
        /// <param name="driverLicense"></param>
        /// <param name="birthDay"></param>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="address1"></param>
        /// <param name="address2"></param>
        /// <param name="zipCode"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="phone"></param>
        /// <param name="mobilePhone"></param>
        /// <param name="newsLetter"></param>
        /// <param name="smsServive"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult PrivateAccount(int customerType = 0, int customerId = 0, string email = "", string driverLicense = "", string birthDay = "", string name = "", string surname = "", string address1 = "", string address2 = "", string zipCode = "", string city = "", string country = "", string phone = "", string mobilePhone = "", bool newsLetter = false, bool smsServive = false)
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.WebAPIManagerRepository.AccountPrivate(bs, customerType, customerId, email, driverLicense, birthDay, name, surname, address1, address2, zipCode,  city, country, phone, mobilePhone, newsLetter, smsServive);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<NordCar.Carla.Data.Entities.Account, Account>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1),HttpStatusCode.NotFound);
            }
        }
        /// <summary>
        /// Company Account
        /// </summary>
        /// <param name="customerType"></param>
        /// <param name="customerId"></param>
        /// <param name="email"></param>
        /// <param name="cvr"></param>
        /// <param name="companyname"></param>
        /// <param name="address1"></param>
        /// <param name="address2"></param>
        /// <param name="zipCode"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="phone"></param>
        /// <param name="mobilePhone"></param>
        /// <param name="newsLetter"></param>
        /// <param name="smsServive"></param>
        /// <param name="companyContact"></param>
        /// <param name="companyContactInfo"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult CompanyAccount(int customerType, int customerId, string email, string cvr, string companyname, string address1, string address2, string zipCode, string city, string country, string phone, string mobilePhone, bool newsLetter, bool smsServive, string companyContact, string companyContactInfo)
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.WebAPIManagerRepository.AccountCompany(bs, customerType, customerId, email, cvr, companyname, address1, address2, zipCode, city, country, phone, mobilePhone, newsLetter, smsServive, companyContact, companyContactInfo);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<NordCar.Carla.Data.Entities.Account, Account>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1),HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region 09 ReturnCompanyCustomerId
        /// <summary>
        /// ReturnCompanyCustomerId
        /// </summary>
        /// <param name="companyName"></param>
        /// <param name="companyContactEmail"></param>
        /// <returns></returns>
      
        [HttpGet]
        public IHttpActionResult ReturnCompanyCustomerId(string companyName, string companyContactEmail)
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.WebAPIManagerRepository.ReturnCompanyCustomerId(bs, companyName, companyContactEmail);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<NordCar.Carla.Data.Entities.CompanyCustomer, CompanyCustomer>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1),HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region 10 SubmitRental
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="rental"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(RentalInfo))]
        public IHttpActionResult SubmitRental(int customerId, Rental rental)
        {
            
            var bs1 = fillbasics();
            bs1.CustomerId = customerId.ToString();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var rent1 = Mapper.Map<Rental, NordCar.Carla.Data.Entities.Rental>(rental);
            var data = this.WebAPIManagerRepository.SubmitRental(bs, rent1);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<NordCar.Carla.Data.Entities.RentalInfo,RentalInfo>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }

        }
        #endregion

        #region 11 GetBookingList
        /// <summary>
        /// GetBookingList
        /// </summary>
        /// <param name="loginType">1=Private,2=Company</param>
        /// <param name="customerId">Customer id</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(Booking))]
        public IHttpActionResult GetBookingList(int loginType, int customerId)
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.WebAPIManagerRepository.GetBookingList(bs,loginType,customerId);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<List<NordCar.Carla.Data.Entities.Booking>, List<Booking>>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1),HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region 12 CancelRental
        /// <summary>
        /// CancelRental
        /// </summary>
        /// <param name="bookingId">booking id</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult CancelRental(int bookingId)
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.WebAPIManagerRepository.CancelRental(bs, bookingId);

            if (data.Item1.Succes)
            {
                return Ok(data.Item2);
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1),HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region 14 GetAddDefaults
        /// <summary>
        /// GetAddDefaults
        /// </summary>
        /// <param name="addId">[OPTIONAL] AddId for a selected campaign, if 0 then request current</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(Defaults))]
        public IHttpActionResult GetAddDefaults(int addId=0)
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.WebAPIManagerRepository.GetAddDefaults(bs, addId);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<NordCar.Carla.Data.Entities.Defaults, Defaults>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1),HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region 15 GetFrontPageDefault
        /// <summary>
        /// GetFrontPageDefault
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(FrontPageDefault_DON))]
    
        public IHttpActionResult GetFrontPageDefault()
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.WebAPIManagerRepository.GetFrontPageDefault(bs);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<NordCar.Carla.Data.Entities.FrontPageDefault_DON, FrontPageDefault_DON>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl,APIMethodControl>(data.Item1),HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region 16 UpdateCompanyDrivers
        /// <summary>
        /// UpdateCompanyDrivers
        /// </summary>
        /// <param name="subFunction">0=MatchList, 1=Remote Driver, 2=Add Driver (only)</param>
        /// <param name="customerId"></param>
        /// <param name="driverName"></param>
        /// <param name="driverSurName"></param>
        /// <param name="driverBirthDate">DATE</param>
        /// <param name="driverLicense"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(CompanyDriverItem))]
        public IHttpActionResult UpdateCompanyDrivers(int subFunction=0, int customerId=0, string driverName="", string driverSurName="", string driverBirthDate="", string driverLicense="")
        {
            var bs1 = fillbasics();
            bs1.CustomerId = customerId.ToString();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.WebAPIManagerRepository.UpdateCompanyDrivers(bs, subFunction, customerId, driverName, driverSurName, driverBirthDate, driverLicense);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<List<NordCar.Carla.Data.Entities.CompanyDriverItem>, List<CompanyDriverItem>>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1),HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region 17 DibsResult
        /// <summary>
        /// DibsResult
        /// </summary>
        /// <param name="bookingId">Booking returned from SubmitRental function</param>
        /// <param name="paymentFlag">[statuscode]</param>
        /// <param name="paymentType">[paymentType]</param>
        /// <param name="paymentCode">transact (1st DIBS reply)</param>
        /// <param name="paymentAmount">TotalPrice [from SubmitRental]</param>
        /// <param name="depositPaymentCode">transact (2nd DIBS reply)</param>
        /// <param name="depositPaymentAmount">DepositOnline(DKK) [from SubmitRental]</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(DibsResultItem))]
        public IHttpActionResult DibsResult(int bookingId, int paymentFlag, int paymentType, int paymentCode, string paymentAmount, int depositPaymentCode, string depositPaymentAmount)
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.WebAPIManagerRepository.DibsResult(bs, bookingId, paymentFlag, paymentType, paymentCode, paymentAmount, depositPaymentCode, depositPaymentAmount);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<NordCar.Carla.Data.Entities.DibsResultItem, DibsResultItem>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1),HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region 18 GetInvalidPickupDates
        /// <summary>
        /// GetInvalidPickupDates
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="categoryId">"3" or "A"</param>
        /// <param name="pickupYear">"YYYY"</param>
        /// <param name="pickupMonth">"MM" 0-12</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(InvalidDateItem))]
        public IHttpActionResult GetInvalidPickupDates(int locationId, string categoryId, string pickupYear, string pickupMonth)
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.WebAPIManagerRepository.GetInvalidPickupDatas(bs, locationId, categoryId, pickupYear, pickupMonth);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<NordCar.Carla.Data.Entities.InvalidDateItem, InvalidDateItem>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1),HttpStatusCode.NotFound);
            }


        }
        #endregion

        #region 19 GetInvalidReturnDates
        /// <summary>
        /// GetInvalidReturnDates
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="categoryId"></param>
        /// <param name="returnYear"></param>
        /// <param name="returnMonth"></param>
        /// <param name="pickupDate"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(InvalidDateItem))]
        public IHttpActionResult GetInvalidReturnDates(int locationId, string categoryId, string returnYear, string returnMonth, string pickupDate)
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.WebAPIManagerRepository.GetInvalidReturnDates(bs, locationId, categoryId, returnYear, returnMonth, pickupDate);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<NordCar.Carla.Data.Entities.InvalidDateItem, InvalidDateItem>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1),HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region 20 GetOpenHours
        /// <summary>
        /// GetOpenHours
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="date"></param>
        /// <param name="isPickupDate"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(OpenHours))]
        public IHttpActionResult GetOpenHours(int locationId = 0, string date = "", int isPickupDate = 0)
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.WebAPIManagerRepository.GetOpenHours(bs, locationId, date, isPickupDate);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<NordCar.Carla.Data.Entities.OpenHours, OpenHours>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1),HttpStatusCode.NotFound);
            }


        }
        #endregion

        #region 21 Promotion Update

        //[HttpGet]
        //public IHttpActionResult PromotionUpdate(int locationId, int returnLocationId, string pickupDate, string pickupTime, string returnDate, string returnTime, string categoryId, int productId,
        //     int ExtraProdId_01, string ExtraProdCurrentNumbUnits_01,
        //    int ExtraProdId_02, string ExtraProdCurrentNumbUnits_02,
        //    int ExtraProdId_03, string ExtraProdCurrentNumbUnits_03,
        //    int ExtraProdId_04, string ExtraProdCurrentNumbUnits_04,
        //    int ExtraProdId_05, string ExtraProdCurrentNumbUnits_05,
        //    int ExtraProdId_06, string ExtraProdCurrentNumbUnits_06,
        //    int ExtraProdId_07, string ExtraProdCurrentNumbUnits_07,
        //    int ExtraProdId_08, string ExtraProdCurrentNumbUnits_08,
        //    int ExtraProdId_09, string ExtraProdCurrentNumbUnits_09,
        //    int ExtraProdId_10, string ExtraProdCurrentNumbUnits_10,
        //    int ExtraProdId_11, string ExtraProdCurrentNumbUnits_11,
        //    int ExtraProdId_12, string ExtraProdCurrentNumbUnits_12,
        //    int ExtraProdId_13, string ExtraProdCurrentNumbUnits_13,
        //    int ExtraProdId_14, string ExtraProdCurrentNumbUnits_14,
        //    int ExtraProdId_15, string ExtraProdCurrentNumbUnits_15,
        //    int ExtraProdId_16, string ExtraProdCurrentNumbUnits_16,
        //    int ExtraProdId_17, string ExtraProdCurrentNumbUnits_17,
        //    int ExtraProdId_18, string ExtraProdCurrentNumbUnits_18,
        //    int ExtraProdId_19, string ExtraProdCurrentNumbUnits_19,
        //    int ExtraProdId_20, string ExtraProdCurrentNumbUnits_20)
        //{
        //    var bs1 = fillbasics(FunctionList.PromotionUpdates, BookTypes.PSBOOK);
        //    var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
        //    var data = this.WebAPIManagerRepository.PromotionUpdate(bs, locationId, returnLocationId, pickupDate, pickupTime, returnDate, returnTime, categoryId, productId,
        //          ExtraProdId_01, ExtraProdCurrentNumbUnits_01,
        //     ExtraProdId_02, ExtraProdCurrentNumbUnits_02,
        //     ExtraProdId_03, ExtraProdCurrentNumbUnits_03,
        //     ExtraProdId_04, ExtraProdCurrentNumbUnits_04,
        //     ExtraProdId_05, ExtraProdCurrentNumbUnits_05,
        //     ExtraProdId_06, ExtraProdCurrentNumbUnits_06,
        //     ExtraProdId_07, ExtraProdCurrentNumbUnits_07,
        //     ExtraProdId_08, ExtraProdCurrentNumbUnits_08,
        //     ExtraProdId_09, ExtraProdCurrentNumbUnits_09,
        //     ExtraProdId_10, ExtraProdCurrentNumbUnits_10,
        //     ExtraProdId_11, ExtraProdCurrentNumbUnits_11,
        //     ExtraProdId_12, ExtraProdCurrentNumbUnits_12,
        //     ExtraProdId_13, ExtraProdCurrentNumbUnits_13,
        //     ExtraProdId_14, ExtraProdCurrentNumbUnits_14,
        //     ExtraProdId_15, ExtraProdCurrentNumbUnits_15,
        //     ExtraProdId_16, ExtraProdCurrentNumbUnits_16,
        //     ExtraProdId_17, ExtraProdCurrentNumbUnits_17,
        //     ExtraProdId_18, ExtraProdCurrentNumbUnits_18,
        //     ExtraProdId_19, ExtraProdCurrentNumbUnits_19,
        //     ExtraProdId_20, ExtraProdCurrentNumbUnits_20);

        //    if (data.Item1.Succes)
        //    {
        //        return Ok(Mapper.Map<List<NordCar.Carla.Data.Entities.PromotionInfo>, List<PromotionInfo>>(data.Item2));
        //    }
        //    else
        //    {
        //        return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
        //    }


        //}

        /// <summary>
        /// Promotion Update
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(PromotionInfo))]
   
        public IHttpActionResult PromotionUpdate(string CustomerId, string OrgBookingNo, string VoucherCode, Price2 price)
        {
            //first no binding to Carla
            //return CreatedAtRoute("DefaultApi", new { id = price.productId }, price);

            var bs1 = fillbasics();
            bs1.OrgBookNr = OrgBookingNo;
            bs1.VoucherCode = VoucherCode;
            bs1.CustomerId = CustomerId;
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var price1 = Mapper.Map<Price2, NordCar.Carla.Data.Entities.Price2>(price);
            var data = this.WebAPIManagerRepository.PromotionUpdate2(bs, price1);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<List<NordCar.Carla.Data.Entities.PromotionInfo>, List<PromotionInfo>>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }

        }
        #endregion

        /// <summary>
        /// 22
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult ReturnProductDefs(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 23 GetOrMailPdf
        /// </summary>
        /// <param name="pdftype">0</param>
        /// <param name="resNo"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetOrMailPdf(int pdftype = 0, string resNo = "", string email = "")
        {
            
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            bs.OrgBookNr = resNo;

            var data = this.WebAPIManagerRepository.PDF(bs,pdftype,resNo,email);

            if (data.Item1.Succes)
            {
                IHttpActionResult response;
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(data.Item2);
                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");
                response = ResponseMessage(result);
                return response; 
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }
    
          

          
        }
    }
}
