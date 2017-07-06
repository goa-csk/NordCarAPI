using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NordCar.Carla.Data.Repository;
using AutoMapper;
using NordCar.WebAPI.BootStrapper;
using NordCar.WebAPI.Models;
using NordCar.WebAPI.Filter;
using System.Web.Http.Description;
using System.Net.Http.Headers;
using System.Reflection;
using NordCar.WebAPI.Models.EC;
using NordCar.Carla.Shared.Logging;
using Newtonsoft.Json;

namespace NordCar.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ECController : BaseAPIController
    {

        /// <summary>
        /// Initialise a variable of IContactsManagerRepository from data layer
        /// </summary>
        protected readonly IECAPIManagerRepository ECAPIManagerRepository;
        /// <summary>
        /// Inject repository
        /// </summary>
        /// <param name="_repository">IPSAPIManagerRepository</param>
        public ECController(IECAPIManagerRepository _repository)
        {
            if (_repository == null)
            {
                throw new ArgumentNullException("WebAPIManager Repository exception");
            }

            this.ECAPIManagerRepository = _repository;

            var bs = new Bootstrapper();
            bs.Initialize();

            Log.LogInfo("ECController Created");
        }   

        #region Version
        /// <summary>
        /// Get version information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(NordCar.WebAPI.Models.EC.Version))]
       
        public IHttpActionResult GetVersion()
        {           
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.ECAPIManagerRepository.GetVersion(bs);

            if (data.Item1.Succes)
            {
                Type t = typeof(NordCar.WebAPI.Controllers.ECController);
                Assembly assemFromType = t.Assembly;

                Type t2 = typeof(NordCar.Carla.Data.Repository.IECAPIManagerRepository);
                Assembly assemFromType2 = t2.Assembly;

                var vers = new NordCar.WebAPI.Models.EC.Version() { CarlaProgram = data.Item2, Data = assemFromType2.FullName, WebApi = assemFromType.FullName };
                var result = new
                {
                    VersionInfo = vers,
                    Status = "OK"
                };

                return Ok(result);
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }

        }

        #endregion

        #region GetBookTypes

        /// <summary>
        /// GetBookTypes
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(List<NordCar.WebAPI.Models.BookTypes>))]
        public IHttpActionResult GetBookTypes()
        {
            var bs1 = fillbasics();

            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);

            var data = this.ECAPIManagerRepository.GetBookTypes(bs);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    booktypes = Mapper.Map<List<NordCar.Carla.Data.Entities.EC.BookType>, List<NordCar.WebAPI.Models.EC.BookType>>(data.Item2),
                    Status = "OK"
                };

                return Ok(result);
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }

        }
        #endregion

        #region GetLocations
        /// <summary>
        /// GetLocations
        /// </summary>
        /// <param name="bookType">See GetBookTypes</param>
        /// <param name="countryId"></param>
        /// <param name="carGroupId"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<NordCar.WebAPI.Models.EC.Location>))]
        public IHttpActionResult GetLocations(string bookType, string countryId = "", string carGroupId = "")
        {
            
            var bs1 = fillbasics(bookType);
            
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);

            var data = this.ECAPIManagerRepository.GetLocations(bs, countryId, carGroupId);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    Locations = Mapper.Map<List<NordCar.Carla.Data.Entities.EC.Location>, List<NordCar.WebAPI.Models.EC.Location>>(data.Item2),
                    Status = "OK"
                };

                return Ok(result);
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }

        }

        #endregion

        #region GetLocationDetails
        /// <summary>
        /// GetLocationDetails
        /// </summary>
        /// <param name="bookType">ex. ECBOOK or DAT</param>
        /// <param name="id">Location id</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(NordCar.WebAPI.Models.EC.LocationDetail))]
        public IHttpActionResult GetLocationDetails(string bookType,string id)
        {

            Log.LogInfo(string.Format("BOOKTYPE={0}, ID={1}",bookType, id));

            var bs1 = fillbasics(bookType);
            
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            
            var data = this.ECAPIManagerRepository.GetLocationDetails(bs, id, System.DateTime.Today, 120);

            if (data.Item1.Succes)
            {

                var result = new
                {
                    Location = Mapper.Map<NordCar.Carla.Data.Entities.EC.LocationDetail, NordCar.WebAPI.Models.EC.LocationDetail>(data.Item2),
                    Status = "OK"
                };

                return Ok(result);
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region GetCountries
        /// <summary>
        /// GetCountries
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        [ResponseType(typeof(List<NordCar.WebAPI.Models.EC.Country>))]
        public IHttpActionResult GetCountries()
        {
            var bs1 = fillbasics();

            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);

            var data = this.ECAPIManagerRepository.GetCountries(bs);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    Locations = Mapper.Map<List<NordCar.Carla.Data.Entities.EC.Country>, List<NordCar.WebAPI.Models.EC.Country>>(data.Item2),
                    Status = "OK"
                };

                return Ok(result);
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region GetAvailableCars

        /// <summary>
        /// GetAvailableCars
        /// </summary>
        /// <param name="input"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModelStateFilter]
        [ResponseType(typeof(List<NordCar.WebAPI.Models.EC.CarDetail>))]
        public IHttpActionResult GetAvailableCars(NordCar.WebAPI.Models.EC.PickDropInfo input, string age)
        {

            var content = JsonConvert.SerializeObject(input);
            Log.LogDebug(new Carla.Shared.Logging.LoggingMessage.LogMessage("GetAvailableCars", content));
            Log.LogDebug(string.Format("Age={0}", age));

            var bs1 = fillbasics(input.Basic);

            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
           
            var input1 = Mapper.Map<NordCar.WebAPI.Models.EC.PickDropInfo, NordCar.Carla.Data.Entities.EC.PickDropInfo>(input);

            var data = this.ECAPIManagerRepository.GetAvailableCars(bs, input1, age);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    ListCars = Mapper.Map<List<NordCar.Carla.Data.Entities.EC.CarDetail>, List<NordCar.WebAPI.Models.EC.CarDetail>>(data.Item2),
                    Status = "OK"
                };

                return Ok(result);
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }

        }

        #endregion

        #region GetCarExtras

        /// <summary>
        /// GetCarExtras
        /// </summary>
        /// <param name="input"></param>
        /// <param name="productId">hovedprodukt</param>
        /// <param name="age">alder</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModelStateFilter]
        [ResponseType(typeof(NordCar.WebAPI.Models.EC.CarExtra))]
        public IHttpActionResult GetCarExtras(NordCar.WebAPI.Models.EC.PickDropInfo input, string productId, string age = "")
        {
            var content = JsonConvert.SerializeObject(input);
            Log.LogDebug(new Carla.Shared.Logging.LoggingMessage.LogMessage("GetCarExtras", content));
            Log.LogInfo(string.Format("Styrende productId={0}", productId));
            Log.LogDebug(string.Format("Age={0}", age));

            var bs1 = fillbasics(input.Basic);
            
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            
            var input1 = Mapper.Map<NordCar.WebAPI.Models.EC.PickDropInfo, NordCar.Carla.Data.Entities.EC.PickDropInfo>(input);

            var data = this.ECAPIManagerRepository.GetCarExtras(bs, input1, productId, age);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    Extras = Mapper.Map<NordCar.Carla.Data.Entities.EC.CarExtra, NordCar.WebAPI.Models.EC.CarExtra>(data.Item2),
                    Status = "OK"
                };

                return Ok(result);
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region UpdatePrice
        /// <summary>
        /// UpdatePrice
        /// </summary>
        /// <param name="input"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModelStateFilter]
        [ResponseType(typeof(NordCar.WebAPI.Models.EC.PriceCalculated))]
        public IHttpActionResult UpdatePrice(NordCar.WebAPI.Models.EC.PricePart input, string age = "")
        {
            var content = JsonConvert.SerializeObject(input);
            Log.LogDebug(new Carla.Shared.Logging.LoggingMessage.LogMessage("UpdatePrice", content));
            Log.LogDebug(string.Format("Age={0}", age));

            var bs1 = fillbasics(input.PickDropInfo.Basic);
           
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            
            var input1 = Mapper.Map<NordCar.WebAPI.Models.EC.PricePart, NordCar.Carla.Data.Entities.EC.PricePart>(input);

            var data = this.ECAPIManagerRepository.UpdatePrice(bs, input1, age);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    Prize = Mapper.Map<NordCar.Carla.Data.Entities.EC.PriceCalculated, NordCar.WebAPI.Models.EC.PriceCalculated>(data.Item2),
                    Status = "OK"
                };

                return Ok(result);
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }

        }
        #endregion

        #region GetCarTypes
        /// <summary>
        /// GetCarTypes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<NordCar.WebAPI.Models.EC.CarType>))]
        public IHttpActionResult GetCarTypes()
        {

            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.ECAPIManagerRepository.GetCarTypes(bs);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    Types = Mapper.Map<List<NordCar.Carla.Data.Entities.EC.CarType>, List<NordCar.WebAPI.Models.EC.CarType>>(data.Item2),
                    Status = "OK"
                };

                return Ok(result);
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }

        }
        #endregion

        #region MakeReservation
        /// <summary>
        /// MakeReservation
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModelStateFilter]
        [ResponseType(typeof(NordCar.WebAPI.Models.RentalInfo))]
        public IHttpActionResult MakeReservation(NordCar.WebAPI.Models.EC.Reservation input)
        {
            var content = JsonConvert.SerializeObject(input);
            Log.LogDebug(new Carla.Shared.Logging.LoggingMessage.LogMessage("MakeReservation", content));
          
            var bs1 = fillbasics(input.PickDropInfo.Basic);

            bs1.CustomerId = input.CustomerNo;

            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var input1 = Mapper.Map<NordCar.WebAPI.Models.EC.Reservation, NordCar.Carla.Data.Entities.EC.Reservation>(input);

            var data = this.ECAPIManagerRepository.MakeReservation(bs, input1);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    RentalInfo = Mapper.Map<NordCar.Carla.Data.Entities.RentalInfo, NordCar.WebAPI.Models.RentalInfo>(data.Item2),
                    Status = "OK"
                };

                return Ok(result);
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }

        }
        #endregion

        #region SearchBooking
        /// <summary>
        /// SearchBooking
        /// </summary>
        /// <param name="reservationNo"></param>
        /// <param name="email"></param>
        /// <param name="lastName"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(NordCar.WebAPI.Models.Booking))]
       public IHttpActionResult SearchBooking(string reservationNo, string email, string lastName, string date)
        {

            Log.LogInfo(string.Format("Reservation={0}", reservationNo));
            Log.LogInfo(string.Format("Email={0}", email));
            Log.LogInfo(string.Format("LastName={0}", lastName));
            Log.LogInfo(string.Format("Date={0}", date));

            var bs1 = fillbasics();
            //var bs1 = fillbasics(FunctionList.SearchBooking, bStruct);
            bs1.CustomerId = "";

            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);

            var data = this.ECAPIManagerRepository.SearchBooking(bs, reservationNo, email, date, lastName);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    Bookings = Mapper.Map<List<NordCar.Carla.Data.Entities.EC.Booking>, List<NordCar.WebAPI.Models.EC.Booking>>(data.Item2),
                    Status = "OK"
                };

                return Ok(result);
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }

        }
        #endregion

        #region CancelBooking
        /// <summary>
        /// CancelBooking
        /// Cancelling the reservation.
        /// </summary>
        /// <param name="reservationNo"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult CancelBooking(string reservationNo)
        {

            Log.LogInfo(string.Format("Reservation={0}", reservationNo));
          
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);

            var data = this.ECAPIManagerRepository.CancelBooking(bs, reservationNo);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    Cancelled = data.Item2.ToString(),
                    Status = "OK"
                };
                return Ok(result);

            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }


        }
        #endregion

        #region GetPdfBooking
        /// <summary>
        /// GetPdfBooking
        /// Returnning reservation as pdf
        /// </summary>
        /// <param name="reservationNo"></param>
        /// <returns>PDF Document</returns>
        [HttpGet]
        public IHttpActionResult GetPdfBooking(string reservationNo = "")
        {

            Log.LogInfo(string.Format("Reservation={0}", reservationNo));
          
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
           
            var data = this.ECAPIManagerRepository.GetPdfBooking(bs, 0, reservationNo, "");

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
        #endregion

        #region LoginType
        /// <summary>
        /// Login typer
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult LoginTypes()
        {
            var enumVals = new List<object>();

            foreach (var item in Enum.GetValues(typeof(LoginType)))
            {
        
                enumVals.Add(new
                {
                    id = (int)item,
                    name = item.ToString()
                });
            }

            return Ok(enumVals);
        }

        #endregion

        #region CreateAccount
        // Create new personal account
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModelStateFilter]
        [ResponseType(typeof(NordCar.WebAPI.Models.EC.Account))]
        public IHttpActionResult CreateAccount(NordCar.WebAPI.Models.EC.PersonalAccount input)
        {
            var content = JsonConvert.SerializeObject(input);
            Log.LogDebug(new Carla.Shared.Logging.LoggingMessage.LogMessage("CreateAccount", content));

            //Adding default program
            if (input.person.FrequentTravelerProgram == null)
            {
                input.person.FrequentTravelerProgram = new Models.EC.FrequentTravelerProgram() { Id = "0", CardNumber = "", ExpiryDate = "" };
            }

            var bs1 = fillbasics(input.basic);
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var input1 = Mapper.Map<NordCar.WebAPI.Models.EC.Person, NordCar.Carla.Data.Entities.EC.Person>(input.person);

            var data = this.ECAPIManagerRepository.CreateAccount(bs, input1);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    CustomerAccount = Mapper.Map<NordCar.Carla.Data.Entities.EC.Account, NordCar.WebAPI.Models.EC.Account>(data.Item2),

                    Status = "OK"
                };
                return Ok(result);

            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region Login
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>      
        [HttpPost]
        [ValidateModelStateFilter]
        [ResponseType(typeof(NordCar.WebAPI.Models.EC.Account))]
        public IHttpActionResult Login(NordCar.WebAPI.Models.EC.LoginInfo login)
        {
            var bs1 = fillbasics(login.Basic);
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
           
            var data = this.ECAPIManagerRepository.Login(bs, ((int)login.LoginType).ToString(), login.UserName, login.Password);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    CustomerAccount = Mapper.Map<NordCar.Carla.Data.Entities.EC.Account, NordCar.WebAPI.Models.EC.Account>(data.Item2),

                    Status = "OK"
                };
                return Ok(result);
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region ForgotPassword
        /// <summary>
        /// ForgotPassword
        /// </summary>
        /// <param name="email"></param>
        /// <returns>password</returns>
        [HttpGet]
        public IHttpActionResult ForgotPassword(string email)
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);

            var data = this.ECAPIManagerRepository.ForgotPassword(bs, "1", email);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    Password = data.Item2.ToString(),
                    Status = "OK"
                };
                return Ok(result);

            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region ModifyAccount
        /// <summary>
        /// ModifyAccount
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModelStateFilter]
        [ResponseType(typeof(NordCar.WebAPI.Models.EC.Account))]
        public IHttpActionResult ModifyAccount(NordCar.WebAPI.Models.EC.PersonalAccount input)
        {
            //CustomerNo on PersonalAccount is not used and should be removed, however remember it is used on response.
            var content = JsonConvert.SerializeObject(input);
            Log.LogDebug(new Carla.Shared.Logging.LoggingMessage.LogMessage("ModifyAccount", content));

            var bs1 = fillbasics(input.basic);
            
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var input1 = Mapper.Map<NordCar.WebAPI.Models.EC.Person, NordCar.Carla.Data.Entities.EC.Person>(input.person);

            var data = this.ECAPIManagerRepository.ModifyAccount(bs, input1);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    CustomerAccount = Mapper.Map<NordCar.Carla.Data.Entities.EC.Account, NordCar.WebAPI.Models.EC.Account>(data.Item2),

                    Status = "OK"
                };
                return Ok(result);

            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region SecretQuestions
        /// <summary>
        /// SecretQuestions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetSecretQuestions()
        {

            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);

            var data = this.ECAPIManagerRepository.DropDownLists(40,bs);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    Lists = Mapper.Map<List<NordCar.Carla.Data.Entities.EC.DropDownListItem>, List<NordCar.WebAPI.Models.EC.DropDownListItem>>(data.Item2),
                    Status = "OK"
                };
                return Ok(result);

            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }


        }
        #endregion

        #region GetPaymentCardTypes
        /// <summary>
        /// PaymentCardTypes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetPaymentCardTypes()
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);

            var data = this.ECAPIManagerRepository.DropDownLists(41,bs);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    Lists = Mapper.Map<List<NordCar.Carla.Data.Entities.EC.DropDownListItem>, List<NordCar.WebAPI.Models.EC.DropDownListItem>>(data.Item2),
                    Status = "OK"
                };
                return Ok(result);
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region GetFrequentTravelerPrograms
        /// <summary>
        /// GetFrequentTravelerPrograms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetFrequentTravelerPrograms()
        {

            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);

            var data = this.ECAPIManagerRepository.DropDownLists(42,bs);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    Lists = Mapper.Map<List<NordCar.Carla.Data.Entities.EC.DropDownListItem>, List<NordCar.WebAPI.Models.EC.DropDownListItem>>(data.Item2),
                    Status = "OK"
                };
                return Ok(result);

            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region GetCarSpecifications
        /// <summary>
        /// GetCarSpecifications
        /// </summary>
        /// <param name="bookType"></param>
        /// <param name="countryId"></param>
        /// <param name="carType"></param>
        /// <param name="carGroup"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetCarSpecifications(string bookType, string countryId, string carType, string carGroup, string age)
        {

            Log.LogInfo(string.Format("countryId={0}", countryId));
            Log.LogInfo(string.Format("carType={0}", carType));
            Log.LogInfo(string.Format("carGroup={0}", carGroup));
            Log.LogInfo(string.Format("bookType={0}", bookType));

            var bs1 = fillbasics(bookType);
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);

            var data = this.ECAPIManagerRepository.GetCarSpecifications(bs, countryId, carType, carGroup, age);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    CarSpec = Mapper.Map<List<NordCar.Carla.Data.Entities.EC.CarSpec>, List<NordCar.WebAPI.Models.EC.CarSpec>>(data.Item2),
                    Status = "OK"
                };
                return Ok(result);

            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region GetFleet
        /// <summary>
        /// GetFleet
        /// </summary>
        /// <param name="bookType"></param>
        /// <param name="countryId"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(NordCar.WebAPI.Models.EC.CarSpec))]
        public IHttpActionResult GetFleet(string bookType, string countryId, string age)
        {

            Log.LogInfo(string.Format("countryId={0}", countryId));
        
            var bs1 = fillbasics(bookType);
            
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);

            var data = this.ECAPIManagerRepository.GetCarSpecifications(bs, countryId, "", "", age);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    CarSpec = Mapper.Map<List<NordCar.Carla.Data.Entities.EC.CarSpec>, List<NordCar.WebAPI.Models.EC.CarSpec>>(data.Item2),
                    Status = "OK"
                };
                return Ok(result);

            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region GetBookings
        /// <summary>
        /// GetBookings
        /// </summary>
        /// <param name="customerNo"></param>
        [HttpGet]
        [ResponseType(typeof(NordCar.WebAPI.Models.Booking))]
        public IHttpActionResult GetBookings(string customerNo)
        {
            Log.LogInfo(string.Format("customerNo={0}", customerNo));
         
            var bs1 = fillbasics();
            bs1.CustomerId = customerNo;
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);

            var data = this.ECAPIManagerRepository.SearchBooking(bs, "", "", "", "");

            if (data.Item1.Succes)
            {
                var result = new
                {
                    Bookings = Mapper.Map<List<NordCar.Carla.Data.Entities.EC.Booking>, List<NordCar.WebAPI.Models.EC.Booking>>(data.Item2),
                    Status = "OK"
                };

                return Ok(result);
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }

        }
        #endregion

        #region DibsResult
        /// <summary>
        /// DibsResult
        /// </summary>
        /// <param name="bookingId">Booking returned from MakeReservation function</param>
        /// <param name="paymentFlag">[statuscode]</param>
        /// <param name="paymentType">[paymentType]</param>
        /// <param name="paymentCode">transact (1st DIBS reply)</param>
        /// <param name="paymentAmount">TotalPrice [from MakeReservation]</param>
        /// <param name="depositPaymentCode">transact (2nd DIBS reply)</param>
        /// <param name="depositPaymentAmount">DepositOnline(DKK) [from MakeReservation]</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(DibsResultItem))]
        public IHttpActionResult DibsResult(int bookingId, int paymentFlag, int paymentType, int paymentCode, int paymentAmount, int depositPaymentCode, int depositPaymentAmount)
        {

            Log.LogInfo(string.Format("bookingId={0}", bookingId));
            Log.LogInfo(string.Format("paymentFlag={0}", paymentFlag));
            Log.LogInfo(string.Format("paymentType={0}", paymentType));
            Log.LogInfo(string.Format("paymentCode={0}", paymentCode));
            Log.LogInfo(string.Format("paymentAmount={0}", paymentAmount));
            Log.LogInfo(string.Format("depositPaymentCode={0}", depositPaymentCode));
            Log.LogInfo(string.Format("depositPaymentAmount={0}", depositPaymentAmount));
      
      
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.ECAPIManagerRepository.DibsResult(bs, bookingId, paymentFlag, paymentType, paymentCode, paymentAmount, depositPaymentCode, depositPaymentAmount);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<NordCar.Carla.Data.Entities.DibsResultItem, DibsResultItem>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }
        }

        #endregion

        #region CheckPromotionCode
        /// <summary>
        /// CheckPromotionCode
        /// </summary>
        /// <param name="basic"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModelStateFilter]
        public IHttpActionResult CheckPromotionCode(BasicStructure1 basic)
        {

            Log.LogInfo(string.Format("promotionCode={0}", basic.VoucherCode));

            var bs1 = fillbasics(basic);
            
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);

            var data = this.ECAPIManagerRepository.CheckPromotionCode(bs);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    PromotionResult = data.Item2,
                    Status = "OK"
                };

                return Ok(result);
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }

        }
        #endregion

        #region GetCarTypesByLocation
       /// <summary>
        /// GetCarTypesByLocation
       /// </summary>
        /// <param name="bookType"></param>
       /// <param name="locationId"></param>
       /// <param name="country"></param>
       /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(NordCar.WebAPI.Models.EC.CarTypeLocationDetails))]
        public IHttpActionResult GetCarTypesByLocationDetails(string bookType, string locationId, string country)
        {

            var bs1 = fillbasics(bookType);
            
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            
            var data = this.ECAPIManagerRepository.GetCarTypesByLocation(bs,locationId,country);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    Types = Mapper.Map<List<NordCar.Carla.Data.Entities.EC.CarTypeLocationDetails>, List<NordCar.WebAPI.Models.EC.CarTypeLocationDetails>>(data.Item2),
                    Status = "OK"
                };

                return Ok(result);
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }

        }
        #endregion

        #region GetReservationText
        /// <summary>
        /// GetReservationText
        /// </summary>
        /// <param name="reservationNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(NordCar.WebAPI.Models.EC.ReservationText))]
        public IHttpActionResult GetReservationText(string reservationNumber)
        {

            Log.LogInfo(string.Format("reservationNumber={0}", reservationNumber));

            var bs1 = fillbasics();
           
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);

            var data = this.ECAPIManagerRepository.GetReservationText(bs,reservationNumber);

            if (data.Item1.Succes)
            {
                var result = new
                {
                    texts = Mapper.Map<NordCar.Carla.Data.Entities.EC.ReservationText,NordCar.WebAPI.Models.EC.ReservationText>(data.Item2),
                    Status = "OK"
                };

                return Ok(result);
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }

        }
        #endregion
   
    }
}