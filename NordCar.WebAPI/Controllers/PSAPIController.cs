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
using System.Web.Http.Cors;
using System.Web;
using NordCar.WebAPI.Filter;
using System.Web.Http.Description;


namespace NordCar.WebAPI.Controllers
{
    /// <summary>
    /// PS
    /// </summary>
    public class PSAPIController : BaseAPIController
    {
           /// <summary>
        /// Initialise a variable of IContactsManagerRepository from data layer
        /// </summary>
        protected readonly IPSAPIManagerRepository PSAPIManagerRepository;

         /// <summary>
        /// Inject repository
        /// </summary>
        /// <param name="_repository">IPSAPIManagerRepository</param>
        public PSAPIController(IPSAPIManagerRepository _repository)
        {

            if (_repository == null)
            {
                throw new ArgumentNullException("WebAPIManager Repository exception");
            }

            this.PSAPIManagerRepository = _repository;

            var bs = new Bootstrapper();
            bs.Initialize();
        }

        #region 01 GetCarList
        /// <summary>
        /// GetCarList
        /// </summary>
        /// <param name="locationId">0 = ALL</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(CarListItem))]
  
        public IHttpActionResult GetCarlist(int locationId = 0)
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.PSAPIManagerRepository.GetCarsList(bs, locationId);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<List<NordCar.Carla.Data.Entities.CarListItem>, List<CarListItem>>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }
        }
        #endregion

        #region 02 GetPriceList
        /// <summary>
        /// GetPriceList
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="categoryId"></param>
        /// <param name="extra"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(PriceListItem))]
  
        public IHttpActionResult GetPriceList(int locationId = 0, string categoryId = "", bool extra = true)
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            if (extra)
            {
                var data = this.PSAPIManagerRepository.GetPriceListExtra(bs, locationId, categoryId);

                if (data.Item1.Succes)
                {
                    return Ok(Mapper.Map<List<NordCar.Carla.Data.Entities.PriceListExtraItem>, List<PriceListExtraItem>>(data.Item2));
                }
                else
                {
                    return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
                }
            }
            else
            {
                var data = this.PSAPIManagerRepository.GetPriceList(bs, locationId, categoryId);

                if (data.Item1.Succes)
                {
                    return Ok(Mapper.Map<List<NordCar.Carla.Data.Entities.PriceListItem>, List<PriceListItem>>(data.Item2));
                }
                else
                {
                    return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
                }


            }
        }
        #endregion

        #region 03 GetAvaiabillityList
        /// <summary>
        /// GetAvaiabillityList
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="productId"></param>
        /// <param name="returnLocationId"></param>
        /// <param name="categoryId"></param>
        /// <param name="pickupDate"></param>
        /// <param name="returnDate"></param>
        /// <param name="pickupTime"></param>
        /// <param name="returnTime"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(AvailabillityItem_PS))]
  
        public IHttpActionResult GetAvaiabillityList(int locationId = 0, int productId = 0, int returnLocationId = 0, string categoryId = "", string pickupDate = "", string returnDate = "", string pickupTime = "", string returnTime = "", int age = 0)
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.PSAPIManagerRepository.GetAvaiabillityList(bs, locationId, productId, returnLocationId, categoryId, pickupDate, returnDate, pickupTime, returnTime, age);

            if (data.Item1.Succes)
            {
                
                return Ok(Mapper.Map<List<NordCar.Carla.Data.Entities.AvailabillityItem_PS>, List<AvailabillityItem_PS>>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }

        }
        #endregion

        #region 05 UpdatePrice
        /// <summary>
        /// UpdatePrice
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(PriceInfo))]
  
        public IHttpActionResult UpdatePrice(Price2 price)
        {
            //first no binding to Carla
            //return CreatedAtRoute("DefaultApi", new { id = price.productId }, price);

            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var price1 = Mapper.Map<Price2, NordCar.Carla.Data.Entities.Price2>(price);
            var data = this.PSAPIManagerRepository.UpdatePrice(bs, price1);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<List<NordCar.Carla.Data.Entities.PriceInfo>, List<PriceInfo>>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }

        }
        #endregion

        #region 10 SubmitRental
        /// <summary>
        /// SubmitRental
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(RentalInfo))]
  
        public IHttpActionResult SubmitRental(Rental_PS rental)
        {

            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var rent1 = Mapper.Map<Rental_PS, NordCar.Carla.Data.Entities.Rental_PS>(rental);
            var data = this.PSAPIManagerRepository.SubmitRental(bs, rent1);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<NordCar.Carla.Data.Entities.RentalInfo, RentalInfo>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
            }

        }
        #endregion

        #region 15 GetFrontPageDefault
        /// <summary>
        /// GetFrontPageDefault
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(FrontPageDefault_PS))]
  
        public IHttpActionResult GetFrontPageDefault()
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.PSAPIManagerRepository.GetFrontPageDefault(bs);

            if (data.Item1.Succes)
            {
                return Ok(Mapper.Map<NordCar.Carla.Data.Entities.FrontPageDefault_PS, FrontPageDefault_PS>(data.Item2));
            }
            else
            {
                return Error(Mapper.Map<NordCar.Carla.Data.Entities.APIMethodControl, APIMethodControl>(data.Item1), HttpStatusCode.NotFound);
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
  
        public IHttpActionResult DibsResult(int bookingId, int paymentFlag, int paymentType, int paymentCode, int paymentAmount, int depositPaymentCode, int depositPaymentAmount)
        {
            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var data = this.PSAPIManagerRepository.DibsResult(bs, bookingId, paymentFlag, paymentType, paymentCode, paymentAmount, depositPaymentCode, depositPaymentAmount);

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

        #region 21 PromotionUpdate
        /// <summary>
        /// PromotionUpdate
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(PromotionInfo))]
        public IHttpActionResult PromotionUpdate(Price2 price)
        {
            //first no binding to Carla
            //return CreatedAtRoute("DefaultApi", new { id = price.productId }, price);

            var bs1 = fillbasics();
            var bs = Mapper.Map<BasicStructure, NordCar.Carla.Data.Entities.BasicStructure>(bs1);
            var price1 = Mapper.Map<Price2, NordCar.Carla.Data.Entities.Price2>(price);
            var data = this.PSAPIManagerRepository.PromotionUpdate(bs, price1);

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
    }
}