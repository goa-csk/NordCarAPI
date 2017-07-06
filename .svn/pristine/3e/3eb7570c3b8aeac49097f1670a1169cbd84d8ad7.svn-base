using AutoMapper;
using Newtonsoft.Json;
using NordCar.Carla.Data.Repository;
using NordCar.Carla.Shared.Logging;
using NordCar.WebAPI.BootStrapper;
using NordCar.WebAPI.Filter;
using NordCar.WebAPI.Models;
using NordCar.WebAPI.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Description;

namespace NordCar.WebAPI.Controllers
{
    public class NCController : BaseAPIController
    {
        //protected readonly ILogger Logger = LoggerManager.CreateLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Initialise a variable of IContactsManagerRepository from data layer
        /// </summary>
        protected readonly IECAPIManagerRepository ECAPIManagerRepository;
        /// <summary>
        /// Inject repository
        /// </summary>
        /// <param name="_repository">IPSAPIManagerRepository</param>
        public NCController(IECAPIManagerRepository _repository)
        {
            if (_repository == null)
            {
                throw new ArgumentNullException("WebAPIManager Repository exception");
            }

            this.ECAPIManagerRepository = _repository;

            var bs = new Bootstrapper();
            bs.Initialize();
            Log.LogInfo("NordCar conrtoller Created");          
        }

        #region ForgotPassword
        /// <summary>
        /// ForgotPassword
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModelStateFilter]
        public IHttpActionResult ForgotPassword(UserRequest user)
        {

            var content = JsonConvert.SerializeObject(user);
            Log.LogDebug(new Carla.Shared.Logging.LoggingMessage.LogMessage("ForgotPassword", content));

            var bs1 = fillbasics(user.Basic);
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);

            var data = this.ECAPIManagerRepository.ForgotPassword(bs, "1", user.Email);

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

        #region GetBookings
        /// <summary>
        /// GetBookings
        /// </summary>       
        [HttpPost]
        [ValidateModelStateFilter]
        [ResponseType(typeof(NordCar.WebAPI.Models.Booking))]
        public IHttpActionResult GetBookings(BasicStructure1 basic)
        {
            var content = JsonConvert.SerializeObject(basic);
            Log.LogDebug(new Carla.Shared.Logging.LoggingMessage.LogMessage("GetBooking",content));

            var bs1 = fillbasics(basic);
            bs1.CustomerId = basic.CustomerId;
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

        #region GetCarTypes
        /// <summary>
        /// GetCarTypes
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateModelStateFilter]
        [ResponseType(typeof(NordCar.WebAPI.Models.EC.CarType))]
        public IHttpActionResult GetCarTypes(BasicStructure1 basic)
        {
            var content = JsonConvert.SerializeObject(basic);
            Log.LogDebug(new Carla.Shared.Logging.LoggingMessage.LogMessage("GetCarTypes", content));

            var bs1 = fillbasics(basic);
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

        #region GetPaymentCardTypes
        /// <summary>
        /// PaymentCardTypes
        /// </summary>
        /// <param name="basic"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModelStateFilter]
        public IHttpActionResult GetPaymentCardTypes(BasicStructure1 basic)
        {
            var content = JsonConvert.SerializeObject(basic);
            Log.LogDebug(new Carla.Shared.Logging.LoggingMessage.LogMessage("GetPaymentCardTypes",content));

            var bs1 = fillbasics(basic);
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
    }

  
    
}
