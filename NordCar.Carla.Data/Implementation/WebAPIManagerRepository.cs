﻿using NordCar.Carla.Data.Entities;
using NordCar.Carla.Data.Infrastructure;
using NordCar.Carla.Data.Repository;
using NordCar.Carla.Data.Test;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NordCar.Carla.Data.Implementation
{
    public class WebAPIManagerRepository : IWebAPIManagerRepository
    {
        const string errorstring = "err";

        private string ip7913;
        private int port7913;
        private string _logfile;

        public WebAPIManagerRepository(string ip, int port, string logfile)
        {
            ip7913 =  ip;
            port7913 = port;
            _logfile = logfile;
        }

        private List<string> FillBasic(BasicStructure basic)
        {
            var temp = 1000;//(int)basic.FunctionId;
            return new List<string> {
                temp.ToString()
                ,basic.Language.ToString()
                ,basic.BookTypes.ToString()
                ,basic.IPAddress
                ,basic.CompanyDealId
                ,basic.CustomerId
                ,basic.ExtraId
                ,basic.VoucherCode
                ,basic.OrgBookNr
                ,basic.StepNr
               
            };
        }


        #region 00 HelloWorld
        /// <summary>
        /// HelloWorld()
        /// </summary>
        /// <returns></returns>
        public string HelloWorld()
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var charData = (char)28;
                string str = "0" + charData + (char)13;

                var text = context.GetData(str);
                var txt = text.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                return txt;
            }
        }
        #endregion

        #region 01 GetCarList
        /// <summary>
        /// Function 01 GetCarsList
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="LocationId"></param>
        /// <returns></returns>
        public Tuple<APIMethodControl, List<CarListItem_DON>> GetCarsList(BasicStructure basic, int LocationId)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var carListItems = new List<CarListItem_DON>();
                var apc = new APIMethodControl();

                var temp = FillBasic(basic);

                temp.Add(LocationId.ToString());

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if ((keys.Length > 1) && (keys[0] != string.Empty))
                {
                    for (var i = 0; i < keys.Length; i += 7)
                    {
                        Debug.WriteLine(string.Format("i={0},loc={1}", i, keys[i]));
                        string _locationId = keys[i];
                        string _cat = keys[i + 1];
                        string _catid = keys[i + 2];
                        string _d1= keys[i + 3];
                        string _d2 = keys[i + 4];
                        string _time = keys[i + 5];
                        
                        carListItems.Add(new CarListItem_DON()
                        {
                            LocationId = _locationId
                            ,
                            Category = _cat
                            ,
                            CategoryId = _catid
                            ,
                            Description1 = _d1
                            ,
                            Description2 = _d2
                            ,
                            DefaultPickUpTime = _time
                            
                        });
                    }
                }

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                }

                return Tuple.Create(apc, carListItems);
            }
        }
        
        #endregion

        #region 02 GetPriceList
        /// <summary>
        /// Function 02 GetPriceList
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="LocationId"></param>
        /// <param name="CategoryId"></param>
        /// <param name="Extra"></param>
        /// <returns></returns>
        public Tuple<APIMethodControl,List<PriceListItem_DON>> GetPriceList(BasicStructure basic, int locationId, string categoryId)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var priceListItems = new List<PriceListItem_DON>();

                var apc = new APIMethodControl();

                var temp = FillBasic(basic);

                temp.Add(locationId.ToString());
                temp.Add(categoryId.ToString());
                
            
                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    for (var i = 0; i < keys.Length; i += 11)
                    {
                        //Debug.WriteLine(string.Format("i={0},loc={1}", i, keys[i]));
                        string _locationId = keys[i];
                        string _productId = keys[i + 1];
                        string _description = keys[i + 2];
                        string _ProductPrice = keys[i + 3];
                        string _KmPrice = keys[i + 4];
                        string _CategoryId = keys[i + 5];
                        string _CarType = keys[i + 6];
                        string _DefaultProductSelection = keys[i + 7];
                        string _PickupTime = keys[i + 8];
                        string _ReturnTime = keys[i + 9];
                        string _KmIncluded = keys[i + 10];
                                 

                        priceListItems.Add(new PriceListItem_DON()
                        {
                            LocationId = _locationId,
                            ProductId = _productId,
                            Description = _description,
                            ProductPrice = _ProductPrice,
                            KmPrice = _KmPrice,
                            CategoryId = _CategoryId,
                            CarType = _CarType,
                            DefaultProductSelection = _DefaultProductSelection,
                            PickupTime = _PickupTime,
                            ReturnTime = _ReturnTime,
                            KmIncluded = _KmIncluded

                        });


                    }
                }

                return Tuple.Create(apc, priceListItems);
            }

        }



        public Tuple<APIMethodControl, List<PriceListItemExtra_DON>> GetPriceListExtra(BasicStructure basic, int locationId, string categoryId)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var priceEx = new List<PriceListItemExtra_DON>();

                var apc = new APIMethodControl();

                var temp = FillBasic(basic);

                temp.Add(locationId.ToString());
                temp.Add(categoryId.ToString());
                temp.Add("EXTRA");

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if ((keys.Length > 0) && (keys[0].ToLower() == errorstring))
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    for (var i = 0; i < keys.Length; i += 10)
                    {
                        priceEx.Add(new PriceListItemExtra_DON()
                        {
                            LocationId = keys[i + 0],
                            ExtraProductId = keys[i + 1],
                            ExtraDescription = keys[i + 2],
                            ExtraUnit = keys[i + 3],
                            ExtraPrice = keys[i + 4],
                            ExtraCategoryId = keys[i + 5],
                            ExtraPriceType = keys[i + 6],
                            ExtraSelectionType = keys[i + 7],
                            ExtraMinNrUnits = keys[i + 8],
                            ExtraMaxNrUnits = keys[i + 9],
                        });
                            
                    }
                }
                return Tuple.Create(apc, priceEx);
            }

          
        }
        #endregion

        #region 03 GetAvailabillityList
        /// <summary>
        /// 03 GetAvailabillityList
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="LocationId"></param>
        /// <param name="ProductId"></param>
        /// <param name="ReturnLlocationId"></param>
        /// <param name="CategoryId"></param>
        /// <param name="PickupDate"></param>
        /// <param name="ReturnDate"></param>
        /// <param name="PickupTime"></param>
        /// <param name="returnTime"></param>
        /// <param name="Age"></param>
        /// <returns></returns>
        public Tuple<APIMethodControl, AvailabillityItem_DON> GetAvaiabillityList(BasicStructure basic, int locationId, int productId, int returnLocationId, string categoryId, string pickupDate, string returnDate, string pickupTime, string returnTime)
          {

            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var availabillityItem = new AvailabillityItem_DON();
                
                var apc = new APIMethodControl();

                var temp = FillBasic(basic);

                temp.Add(locationId.ToString());
                temp.Add(productId.ToString());
                temp.Add(returnLocationId.ToString());
                temp.Add(categoryId.ToString());
                temp.Add(pickupDate);
                temp.Add(returnDate);
                temp.Add(pickupTime);
                temp.Add(returnTime);
               
                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);


                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    availabillityItem.locationId = Helpers.StringEmptyToInt(keys[0]);
                    availabillityItem.productId = Helpers.StringEmptyToInt(keys[1]);
                    availabillityItem.productName = keys[2];
                    availabillityItem.categoryId = keys[3];
                    availabillityItem.duration = Helpers.StringEmptyToInt(keys[4]);
                    availabillityItem.priceTotalInclVat = keys[5];
                    availabillityItem.inclKms = keys[6];
                    availabillityItem.priceExtraKms = keys[7];
                    availabillityItem.pickupDate = keys[8];
                    availabillityItem.pickupTimeMin = keys[9];
                    availabillityItem.pickupTimeMax = keys[10];
                    availabillityItem.returnDate = keys[11];
                    availabillityItem.returnTimeMin = keys[12];
                    availabillityItem.returnTimeMax = keys[13];
                    availabillityItem.depositOnline = keys[14];
                    availabillityItem.depositCash = keys[15];
                    availabillityItem.depositCreditCard = keys[16];
                    availabillityItem.depositOnlineRent = keys[17];
                    availabillityItem.depositCashRent = keys[18];
                    availabillityItem.depositCreditCardRent = keys[19];
                    availabillityItem.paymentCardList = keys[20];
                    availabillityItem.additionalInfo = keys[21];
                    availabillityItem.avaialablePaymentTypes= keys[22];
                    availabillityItem.additionalInfoLink = keys[23];
                }
                    return Tuple.Create(apc, availabillityItem);
            }

        }
        #endregion

        #region 04 SelectProduct
        /// <summary>
        /// SelectProduct
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="LocationId"></param>
        /// <param name="ReturnLocationId"></param>
        /// <param name="CategoryId"></param>
        /// <param name="PickupDate"></param>
        /// <param name="ReturnDate"></param>
        /// <param name="PickupTime"></param>
        /// <param name="ReturnTime"></param>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public Tuple<APIMethodControl, List<Product>> SelectProduct(BasicStructure basic, int locationId, int returnLocationId, string pickupDate, string pickupTime, string returnDate, string returnTime, string categoryId, int productId)
        {

            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var products = new List<Product>();

                var apc = new APIMethodControl();

                var temp = FillBasic(basic);

                temp.Add(locationId.ToString());
                temp.Add(returnLocationId.ToString());
                temp.Add(pickupDate);
                temp.Add(pickupTime);
                temp.Add(returnDate);
                temp.Add(returnTime);
                temp.Add(categoryId.ToString());
                temp.Add(productId.ToString());

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    for (var i = 0; i < keys.Length; i += 8)
                    {
                        products.Add(new Product()
                        {
                            ProductId = keys[i + 0],
                            Description = keys[i + 1],
                            Units = keys[i + 2],
                            PricePrUnit = keys[i + 3],
                            CurrentNoUnits = keys[i + 4],
                            MinNoUnits = keys[i + 5],
                            MaxNoUnits = keys[i + 6],
                            SelectionType = keys[i + 7]
                        });
                    }
                }
                            return Tuple.Create(apc, products);
                   }

          
            
            
        }
        #endregion

        #region 05 UpdatePrice
        public Tuple<APIMethodControl, List<PriceInfo>> UpdatePrice(BasicStructure basic, int locationId, int returnLocationId, string pickupDate, string pickupTime, string returnDate, string returnTime, string categoryId, int productId,
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
            int ExtraProdId_20, string ExtraProdCurrentNumbUnits_20)
        {

            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var priceinfos = new List<PriceInfo>();

                var apc = new APIMethodControl();

                var temp = FillBasic(basic);

                temp.Add(locationId.ToString());
                temp.Add(returnLocationId.ToString());
                temp.Add(pickupDate);
                temp.Add(pickupTime);
                temp.Add(returnDate);
                temp.Add(returnTime);
                temp.Add(categoryId.ToString());
                temp.Add(productId.ToString());
                
                temp.Add(ExtraProdId_01.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_01.ToString());

                temp.Add(ExtraProdId_02.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_02.ToString());

                temp.Add(ExtraProdId_03.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_03.ToString());

                temp.Add(ExtraProdId_04.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_04.ToString());

                temp.Add(ExtraProdId_05.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_05.ToString());

                temp.Add(ExtraProdId_06.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_06.ToString());

                temp.Add(ExtraProdId_07.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_07.ToString());

                temp.Add(ExtraProdId_08.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_08.ToString());

                temp.Add(ExtraProdId_09.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_09.ToString());

                temp.Add(ExtraProdId_10.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_10.ToString());

                temp.Add(ExtraProdId_11.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_11.ToString());

                temp.Add(ExtraProdId_12.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_12.ToString());
                
                temp.Add(ExtraProdId_13.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_13.ToString());
                
                temp.Add(ExtraProdId_14.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_14.ToString());
                
                temp.Add(ExtraProdId_15.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_15.ToString());
                
                temp.Add(ExtraProdId_16.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_16.ToString());
                
                temp.Add(ExtraProdId_17.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_17.ToString());
                
                temp.Add(ExtraProdId_18.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_18.ToString());
                
                temp.Add(ExtraProdId_19.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_19.ToString());
                
                temp.Add(ExtraProdId_20.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_20.ToString());

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    for (var i = 0; i < keys.Length; i += 22)
                    {
                        priceinfos.Add(new PriceInfo()
                        {
                                Total = keys[i + 0], 
                                DepositOnline = keys[i + 1], 
                                DepositCash  = keys[i + 2],
                                DepositCreditCard = keys[i + 3], 
                                TotalDepositOnline = keys[i + 4], 
                                TotalDepositCash  = keys[i + 5],
                                TotalDepositCreditCard = keys[i + 6], 
                                TotalExtraPrice  = keys[i + 7],
                                TotalExclusiveTotalExtraPrice = keys[i + 8],
                                BookStatus = Helpers.StringEmptyToInt(keys[i + 9]),
                                BookStatusText = keys[i + 10],
                                PayCashOnCollectFlag = Helpers.StringEmptyToInt(keys[i + 11]),
                                PayCardOnCollectFlag = Helpers.StringEmptyToInt(keys[i + 12]),
                                PayOnlineFlag = Helpers.StringEmptyToInt(keys[i + 13]),
                                DepositDescription  = keys[i + 14],
                                RentPricePrDay  = keys[i + 15],
                                PickupText  = keys[i + 16],
                                ProductDeductibleText  = keys[i + 17],
                                NumberOfDays = Helpers.StringEmptyToInt(keys[i + 18]),
                                NumberOfKMs = Helpers.StringEmptyToInt(keys[i + 19]),
                                ProductName = keys[i + 20]
                        });
                    }
                }
                return Tuple.Create(apc, priceinfos);
            }

          
        
        }

        public Tuple<APIMethodControl, List<PriceInfo_DON>> UpdatePrice2(BasicStructure basic, Price2 price)
        {

            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var priceinfos = new List<PriceInfo_DON>();

                var apc = new APIMethodControl();

                var temp = FillBasic(basic);

                temp.Add(price.locationId.ToString());
                temp.Add(price.returnLocationId.ToString());
                temp.Add(price.pickupDate);
                temp.Add(price.pickupTime);
                temp.Add(price.returnDate);
                temp.Add(price.returnTime);
                temp.Add(price.categoryId.ToString());
                temp.Add(price.productId.ToString());

                foreach (ExtraProduct ep in price.extras)
                {
                    temp.Add(ep.id.ToString());
                    temp.Add(ep.numbUnit.ToString());

                }

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    for (var i = 0; i < keys.Length; i += 9)
                    {
                        priceinfos.Add(new PriceInfo_DON()
                        {
                            Total = keys[i + 0],
                            DepositOnline = keys[i + 1],
                            DepositCash = keys[i + 2],
                            DepositCreditCard = keys[i + 3],
                            TotalDepositOnline = keys[i + 4],
                            TotalDepositCash = keys[i + 5],
                            TotalDepositCreditCard = keys[i + 6],
                            TotalExtraPrice = keys[i + 7],
                            TotalExclusiveTotalExtraPrice = keys[i + 8],
                            
                        });
                    }
                }
                return Tuple.Create(apc, priceinfos);
            }
        }

        #endregion

        #region 06 GetLocationList
     
        /// <summary>
        /// Function 06 GetLocationList
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="LocationId"></param>
        /// <returns></returns>
        public Tuple<APIMethodControl, List<Location>> GetLocationList(BasicStructure basic, int LocationId)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var locationListItems = new List<Location>();
                var apc = new APIMethodControl();

                var temp = FillBasic(basic);

                temp.Add(LocationId.ToString());

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                
                var keys = Helpers.DecodeString(text);

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    for (var i = 0; i < keys.Length; i += 14)
                    {
                        //Debug.WriteLine(string.Format("i={0},loc={1}", i, keys[i]));
                        string _locationId = keys[i];
                        string _name = keys[i + 1];
                        string _address1 = keys[i + 2];
                        string _address2 = keys[i + 3];
                        string _zipCode = keys[i + 4];
                        string _city = keys[i + 5];
                        string _country = keys[i + 6];
                        string _phoneNr = keys[i + 7];
                        string _faxNr = keys[i + 8];
                        string _bookingreplyEmail1 = keys[i + 9];
                        string _bookingreplyEmail2 = keys[i + 10];
                        string _fleightNr = keys[i + 11];
                        string _key = keys[i + 12];
                        string _openHeader = keys[i + 13];
                        //string _manfre = keys[i + 14];
                        //string _sat = keys[i + 15];
                        //string _sun = keys[i + 16];
                        //string _newyearsday = keys[i + 17];
                        //string _easter1 = keys[i + 18];
                        //string _easter2 = keys[i + 19];
                        //string _easter3 = keys[i + 20];
                        //string _easter4 = keys[i + 21];
                        //string _prayerday = keys[i + 22];
                        //string _ascensionday = keys[i + 23];
                        //string _pinse1 = keys[i + 24];
                        //string _pinse2 = keys[i + 25];
                        //string _nationalday = keys[i + 26];
                        //string _christmasEve = keys[i + 27];
                        //string _christmas1 = keys[i + 28];
                        //string _christmas2 = keys[i + 29];
                        //string _newyearsEve = keys[i + 30];

                        locationListItems.Add(new Location()
                        {
                            LocationId = _locationId,
                            Name = _name,
                            Address1 = _address1,
                            Address2 = _address2,
                            ZipCode = _zipCode,
                            City = _city,
                            Country = _country,
                            PhoneNr = _phoneNr,
                            FaxNr = _faxNr,
                            BookingReplyEmail1 = _bookingreplyEmail1,
                            BookingReplyEmail2 = _bookingreplyEmail2,
                            FleightNr = _fleightNr,
                            Key = _key,
                            Openhours = _openHeader
                           

                        });
                    }
                

             
                }



                return Tuple.Create(apc, locationListItems);
                
            }

        }
        #endregion

        #region 07 Login
        public Tuple<APIMethodControl, User> LoginPrivate(BasicStructure basic, int loginType, string userName, string password)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var usr = new User();

                var apc = new APIMethodControl();

                var temp = FillBasic(basic);
                temp.Add(loginType.ToString());
                temp.Add(userName);
                temp.Add(password);

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    usr.CustomerId = Helpers.StringEmptyToInt(keys[0]);
                    usr.CompanyDealId = Helpers.StringEmptyToInt(keys[1]);
                    usr.Language = keys[2];
                    usr.Name = keys[3];
                    usr.SurName = keys[4];
                    usr.Address1 = keys[5];
                    usr.Address2 = keys[6];
                    usr.ZipCode = keys[7];
                    usr.City = keys[8];
                    usr.Country = keys[9];
                    usr.PhoneNo = keys[10];
                    usr.MobilePhoneNo = keys[11];
                    usr.EmailAddress = keys[12];
                    usr.DriverLicense = keys[13];
                    usr.BirthDate = keys[14];
                    usr.NewsLetter = Helpers.ConvertStringToBool(keys[15]);
                    usr.SMSService = Helpers.ConvertStringToBool(keys[16]);
                   
                    
                }

                return Tuple.Create(apc, usr);

            }
        }

        public Tuple<APIMethodControl, UserCompany> LoginCompany(BasicStructure basic, int loginType, string userName, string password)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var usr = new UserCompany();

                var apc = new APIMethodControl();

                var temp = FillBasic(basic);
                temp.Add(loginType.ToString());
                temp.Add(userName);
                temp.Add(password);

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    usr.CustomerId = Helpers.StringEmptyToInt(keys[0]);
                    usr.CompanyDealId = Helpers.StringEmptyToInt(keys[1]);
                    usr.Language = keys[2];
                    usr.Name = keys[3];
                    usr.Address1 = keys[4];
                    usr.Address2 = keys[5];
                    usr.ZipCode = keys[6];
                    usr.City = keys[7];
                    usr.Country = keys[8];
                    usr.PhoneNo = keys[9];
                    usr.MobilePhoneNo = keys[10];
                    usr.EmailAddress = keys[11];
                    usr.CVRNo = keys[12];
                    usr.NewsLetter = Helpers.ConvertStringToBool(keys[13]);
                    usr.SMSService = Helpers.ConvertStringToBool(keys[14]);
                    usr.CompanyContact = keys[15];
                    usr.CompanyContactInfo = keys[16];

                }

                return Tuple.Create(apc, usr);

            }
        }
        #endregion

        #region 08 Account

        public Tuple<APIMethodControl, Account> AccountPrivate(BasicStructure basic, int customerType, int customerId, string email, string driverLicense, string birthDay, string name, string surname, string address1, string address2, string zipCode, string city, string country, string phone, string mobilePhone, bool newsLetter, bool smsServive)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var usrAcc = new Account();

                var apc = new APIMethodControl();

                var temp = FillBasic(basic);

                temp.Add(customerType.ToString());
                temp.Add(customerId.ToString());
                temp.Add(email); 
                temp.Add(driverLicense); 
                temp.Add(birthDay); 
                temp.Add(name);
                temp.Add(surname); 
                temp.Add(address1); 
                temp.Add(address2); 
                temp.Add(zipCode);
                temp.Add(city);
                temp.Add(country); 
                temp.Add(phone); 
                temp.Add(mobilePhone); 
                temp.Add(newsLetter.ToString());
                temp.Add(smsServive.ToString());

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    usrAcc.ResultString = keys[0];
                    usrAcc.ResultText = keys[1];
                    usrAcc.CustomerNo = keys[2];
                }

                return Tuple.Create(apc, usrAcc);

            }
        }

        public Tuple<APIMethodControl, Account> AccountCompany(BasicStructure basic, int customerType, int customerId, string email, string cvr, string companyname, string address1, string address2, string zipCode, string city, string country, string phone, string mobilePhone, bool newsLetter, bool smsServive, string companyContact, string companyContactInfo)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var usrAcc = new Account();

                var apc = new APIMethodControl();

                var temp = FillBasic(basic);

                temp.Add(customerType.ToString());
                temp.Add(customerId.ToString());
                temp.Add(email);
                temp.Add(cvr);
                temp.Add(companyname);
                temp.Add(address1);
                temp.Add(address2);
                temp.Add(zipCode);
                temp.Add(city);
                temp.Add(country);
                temp.Add(phone);
                temp.Add(mobilePhone);
                temp.Add(newsLetter.ToString());
                temp.Add(smsServive.ToString());
                temp.Add(companyContact);
                temp.Add(companyContactInfo);
                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    usrAcc.ResultString = keys[0];
                    usrAcc.ResultText = keys[1];
                    usrAcc.CustomerNo = keys[2];
                }

                return Tuple.Create(apc, usrAcc);

            }
        }

        #endregion

        #region 09 ReturnCompanyCustomerId
        public Tuple<APIMethodControl, CompanyCustomer> ReturnCompanyCustomerId(BasicStructure basic, string companyName, string companyContactEmail)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var cmpycust = new CompanyCustomer();

                var apc = new APIMethodControl();

                var temp = FillBasic(basic);

                temp.Add(companyName);
                temp.Add(companyContactEmail);

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    cmpycust.CompanyCustomerId = Helpers.StringEmptyToInt(keys[0]);
                    cmpycust.EmailBodyText = keys[1];
                   
                }

                return Tuple.Create(apc, cmpycust);

            }
        }
        #endregion

        #region 10 SubmitRental

        public Tuple<APIMethodControl, RentalInfo> SubmitRental(
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
            int AddOnProdId_20, string AddOnProdCurrentNumbUnits_20)
        {

            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var info = new RentalInfo();

                var apc = new APIMethodControl();

                var temp = FillBasic(basic);

                temp.Add(locationId.ToString());
                temp.Add(returnLocationId.ToString());
                temp.Add(productId.ToString());
                temp.Add(pickupDate);
                temp.Add(pickupTime);
                temp.Add(returnDate);
                temp.Add(returnTime);
                temp.Add(categoryId.ToString());
                temp.Add(CoRenterForName.ToString());
                temp.Add(CoRenterSurName.ToString());
                temp.Add(CoRenterDriverLicense.ToString());
                temp.Add(CoRenterBirthDay.ToString());
                temp.Add(RekvisitionNo.ToString());
                temp.Add(PayType.ToString());
                temp.Add(DriverNo1.ToString());
                temp.Add(DriverNo2.ToString());
                temp.Add(DriverNo3.ToString());
                temp.Add(DriverNo4.ToString());
                temp.Add(DriverNo5.ToString());
                temp.Add(DriverNo6.ToString());
                temp.Add(DriverNo7.ToString());
                temp.Add(DriverNo8.ToString());
                temp.Add(DriverNo9.ToString());
                temp.Add(DriverNo10.ToString());
                temp.Add(BookingStatus.ToString());
                temp.Add(RenterName.ToString());
                temp.Add(RenterBirthDay.ToString());
                temp.Add(RenterAddress.ToString());
                temp.Add(RenterZipCodeAndCity.ToString());
                temp.Add(RenterPhoneMobile.ToString());
                temp.Add(REnterEmail.ToString());
                temp.Add(AddOnProdId_01.ToString());
                temp.Add(AddOnProdCurrentNumbUnits_01.ToString());

                temp.Add(AddOnProdId_02.ToString());
                temp.Add(AddOnProdCurrentNumbUnits_02.ToString());

                temp.Add(AddOnProdId_03.ToString());
                temp.Add(AddOnProdCurrentNumbUnits_03.ToString());

                temp.Add(AddOnProdId_04.ToString());
                temp.Add(AddOnProdCurrentNumbUnits_04.ToString());

                temp.Add(AddOnProdId_05.ToString());
                temp.Add(AddOnProdCurrentNumbUnits_05.ToString());

                temp.Add(AddOnProdId_06.ToString());
                temp.Add(AddOnProdCurrentNumbUnits_06.ToString());

                temp.Add(AddOnProdId_07.ToString());
                temp.Add(AddOnProdCurrentNumbUnits_07.ToString());

                temp.Add(AddOnProdId_08.ToString());
                temp.Add(AddOnProdCurrentNumbUnits_08.ToString());

                temp.Add(AddOnProdId_09.ToString());
                temp.Add(AddOnProdCurrentNumbUnits_09.ToString());

                temp.Add(AddOnProdId_10.ToString());
                temp.Add(AddOnProdCurrentNumbUnits_10.ToString());

                temp.Add(AddOnProdId_11.ToString());
                temp.Add(AddOnProdCurrentNumbUnits_11.ToString());

                temp.Add(AddOnProdId_12.ToString());
                temp.Add(AddOnProdCurrentNumbUnits_12.ToString());

                temp.Add(AddOnProdId_13.ToString());
                temp.Add(AddOnProdCurrentNumbUnits_13.ToString());

                temp.Add(AddOnProdId_14.ToString());
                temp.Add(AddOnProdCurrentNumbUnits_14.ToString());

                temp.Add(AddOnProdId_15.ToString());
                temp.Add(AddOnProdCurrentNumbUnits_15.ToString());

                temp.Add(AddOnProdId_16.ToString());
                temp.Add(AddOnProdCurrentNumbUnits_16.ToString());

                temp.Add(AddOnProdId_17.ToString());
                temp.Add(AddOnProdCurrentNumbUnits_17.ToString());

                temp.Add(AddOnProdId_18.ToString());
                temp.Add(AddOnProdCurrentNumbUnits_18.ToString());

                temp.Add(AddOnProdId_19.ToString());
                temp.Add(AddOnProdCurrentNumbUnits_19.ToString());

                temp.Add(AddOnProdId_20.ToString());
                temp.Add(AddOnProdCurrentNumbUnits_20.ToString());
                
                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    info.ReservationNo = keys[0];
                    info.TotalPrice = keys[1];
                    info.DepositOnline = keys[2];
                    info.DepositPickupCash = keys[3];
                    info.DepositPickupCard = keys[4];
                    info.RentPlusDepositOnline = keys[5];
                    info.RentPlusDepositPickupCash = keys[6];
                    info.RentPlusDepositPickupCard = keys[7];
                    info.AddOnsTotalPrice = keys[8];
                    info.TotalPriceExclAddOnsTotalPrice = keys[9];
                }
                
                return Tuple.Create(apc, info);
            }

          

        }

        public Tuple<APIMethodControl, RentalInfo> SubmitRental(BasicStructure basic, Rental rent)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var info = new RentalInfo();

                var apc = new APIMethodControl();

                var temp = FillBasic(basic);

                temp.Add(rent.locationId.ToString());
                temp.Add(rent.returnLocationId.ToString());
                temp.Add(rent.productId.ToString());
                temp.Add(rent.pickupDate);
                temp.Add(rent.pickupTime);
                temp.Add(rent.returnDate);
                temp.Add(rent.returnTime);
                temp.Add(rent.categoryId.ToString());
                temp.Add(rent.coRenterName.ToString());
                temp.Add(rent.coRenterSurName.ToString());
                temp.Add(rent.coRenterLicenseNo.ToString());
                temp.Add(rent.coRenterBirthDay.ToString());
                temp.Add(rent.rekvisitionNo.ToString());
                temp.Add(rent.payType.ToString());
                
                for (int i =0; i<10; i++)
                {
                    try
                    {
                        temp.Add(rent.drivers[i].name.ToString());
                    }
                    catch
                    {
                        temp.Add("");
                    }
                }

                foreach (ExtraProduct ep in rent.extras)
                {
                    temp.Add(ep.id.ToString());
                    temp.Add(ep.numbUnit.ToString());

                }

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    info.ReservationNo = keys[0];
                    info.TotalPrice = keys[1];
                    info.DepositOnline = keys[2];
                    info.DepositPickupCash = keys[3];
                    info.DepositPickupCard = keys[4];
                    info.RentPlusDepositOnline = keys[5];
                    info.RentPlusDepositPickupCash = keys[6];
                    info.RentPlusDepositPickupCard = keys[7];
                    info.AddOnsTotalPrice = keys[8];
                    info.TotalPriceExclAddOnsTotalPrice = keys[9];
                }

                return Tuple.Create(apc, info);
            }

        
        }
        #endregion

        #region 11 GetBookingList

        public Tuple<APIMethodControl, List<Booking>> GetBookingList(BasicStructure basic, int logintype, int customerId)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var bookings = new List<Booking>();
                var apc = new APIMethodControl();

                var temp = FillBasic(basic);

                temp.Add(logintype.ToString());
                temp.Add(customerId.ToString());

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if ((keys.Length > 1) && (keys[0] != string.Empty))
                {
                    for (var i = 0; i < keys.Length; i += 15)
                    {
                        Debug.WriteLine(string.Format("i={0},loc={1}", i, keys[i]));
                        string _bookingId = keys[i];
                        string _locationId = keys[i + 1];
                        string _returnlocationId = keys[i + 2];
                        string _pickupDate = keys[i + 3];
                        string _pickupTime = keys[i + 4];
                        string _deliveryDate = keys[i + 5];
                        string _deliveryTime = keys[i + 6];
                        string _categoryId = keys[i + 7];
                        string _productId = keys[i + 8];
                        string _renterName = keys[i + 9];
                        string _renterSurName = keys[i + 10];
                        string _coRenterName = keys[i + 11];
                        string _coRentersurName = keys[i + 12];
                        string _priceTotal = keys[i + 13];
                        string _bookingActive = keys[i + 14];


                        bookings.Add(new Booking()
                        {
                            BookingId = Helpers.StringEmptyToInt(_bookingId),
                            LocationId = Helpers.StringEmptyToInt(_locationId),
                            ReturnLocationId = Helpers.StringEmptyToInt(_returnlocationId),
                            PickupDate = _pickupDate,
                            PickupTime = _pickupTime,
                            DeliveryDate = _deliveryDate,
                            DeliveryTime = _deliveryTime,
                            CategoryId = _categoryId,
                            ProductId = Helpers.StringEmptyToInt(_productId),
                            RenterName = _renterName,
                            RenterSurName = _renterSurName,
                            CoRenterName = _coRenterName,
                            CoRenterSurName = _coRentersurName,
                            PriceTotal = _priceTotal,
                            BookingActive = Helpers.StringEmptyToInt(_bookingActive)
                        });
                    }
                }

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                }



                return Tuple.Create(apc, bookings);

            }
        }
        #endregion

        #region 12 CancelRental
        public Tuple<APIMethodControl,bool> CancelRental(BasicStructure basic, int bookingId)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
               
                var apc = new APIMethodControl();
                bool res = false;

                var temp = FillBasic(basic);

                temp.Add(bookingId.ToString());
                
                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if ((keys.Length > 1) && (keys[0] != string.Empty))
                {

                    res = keys[0] == "1" ? true : false;

                    if (keys[0].ToLower() == errorstring)
                    {
                        apc.Succes = false;
                        apc.ErrorCode = keys[1];
                        apc.ErrorMessage = keys[2];
                    }
                    else
                    {
                        apc.Succes = true;
                    }
                }

                return Tuple.Create(apc, res);

            }
        }
        #endregion

        #region 14 GetAddDefaults
        public Tuple<APIMethodControl, Defaults> GetAddDefaults(BasicStructure basic, int addId)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var def = new Defaults();

                var apc = new APIMethodControl();

                var temp = FillBasic(basic);

                temp.Add(addId.ToString());
                
                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys.Length == 5)
                {
                    def.LocationId = Helpers.StringEmptyToInt(keys[0]);
                    def.PickupDateStart = keys[1];
                    def.PickupDateEnd = keys[2];
                    def.PickupTimeStart = keys[3];
                    def.PickupTimeEnd = keys[4];
                }

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                }

                return Tuple.Create(apc, def);

            }
        }
        #endregion

        #region 15 GetFrontPageDefault
      

        public Tuple<APIMethodControl, FrontPageDefault_DON> GetFrontPageDefault(BasicStructure basic)
        {

            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var frontpagedefault = new FrontPageDefault_DON();

                var temp = FillBasic(basic);

                var apc = new APIMethodControl();

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    frontpagedefault.UpLeftProductId = keys[0];
                    frontpagedefault.UpLeftCategoryId = keys[1];
                    frontpagedefault.UpLeftText = keys[2];
                    frontpagedefault.UpLeftModel = keys[3];
                    frontpagedefault.UpLeftPriceDKK = keys[4];
                    frontpagedefault.UpLeftPriceText = keys[5];
                    frontpagedefault.UpRightProductId = keys[6];
                    frontpagedefault.UpRightCategoryId = keys[7];
                    frontpagedefault.UpRightText = keys[8];
                    frontpagedefault.UpRightModel = keys[9];
                    frontpagedefault.UpRightPriceDKK = keys[10];
                    frontpagedefault.UpRightPriceText = keys[11];
                    frontpagedefault.PrivateLoginCheck = keys[12];
                    frontpagedefault.CompanyLoginCheck = keys[13];
                    frontpagedefault.Bottom1Text = keys[14];
                    frontpagedefault.Bottom1GraphicUrl = keys[15];
                    frontpagedefault.Bottom1DestinationUrl = keys[16];
                    frontpagedefault.Bottom1ButtonText = keys[17];
                    frontpagedefault.Bottom2Text = keys[18];
                    frontpagedefault.Bottom2GraphicUrl = keys[19];
                    frontpagedefault.Bottom2DestinationUrl = keys[20];
                    frontpagedefault.Bottom2ButtonText = keys[21];
                    frontpagedefault.Bottom3Text = keys[22];
                    frontpagedefault.Bottom3GraphicUrl = keys[23];
                    frontpagedefault.Bottom3DestinationUrl = keys[24];
                    frontpagedefault.Bottom3ButtonText = keys[25];
                    frontpagedefault.LocationId = keys[26];
                    frontpagedefault.PickupDate = keys[27];
                    frontpagedefault.PickupTime = keys[28];
                    frontpagedefault.ReturnDate = keys[29];
                    frontpagedefault.ReturnTime = keys[30];


                }


                return Tuple.Create(apc, frontpagedefault); ;
            }

        }
        #endregion

        #region 16 UpdateCompanyDrivers
        public Tuple<APIMethodControl, List<CompanyDriverItem>> UpdateCompanyDrivers(BasicStructure basic, int subFunction, int customerId, string driverName, string driverSurName, string driverBirthDate, string driverLicense)
        {
             using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
             {
             
                var cmpydriver = new List<CompanyDriverItem>();
                var apc = new APIMethodControl();

                var temp = FillBasic(basic);
                
                temp.Add(subFunction.ToString());
                temp.Add(customerId.ToString());
                temp.Add(driverName.ToString());
                temp.Add(driverSurName.ToString());
                temp.Add(driverBirthDate.ToString());
                temp.Add(driverLicense.ToString());
                
                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    for (var i = 0; i < keys.Length; i += 5)
                    {
                        Debug.WriteLine(string.Format("i={0},loc={1}", i, keys[i]));
                        string _driverCustomerId = keys[i];
                        string _driverName = keys[i + 1];
                        string _driverSurName = keys[i + 2];
                        string _driverBirthDate = keys[i + 3];
                        string _driverLicense = keys[i + 4];
                        
                        cmpydriver.Add(new CompanyDriverItem()
                        {
                            CustomerId = _driverCustomerId,
                            Name = _driverName,
                            SurName = _driverSurName,
                            BirthDate = _driverBirthDate,
                            License = _driverLicense
                        });
                    }
                }

                return Tuple.Create(apc, cmpydriver);
            }
        }
#endregion

        #region 17 DibsResult
        public Tuple<APIMethodControl, DibsResultItem> DibsResult(BasicStructure basic, int bookingId, int paymentFlag, int paymentType, int paymentCode, string paymentAmount, int depositPaymentCode, string depositPaymentAmount)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var apc = new APIMethodControl();

                var dibres = new DibsResultItem();

                var temp = FillBasic(basic);

                temp.Add(bookingId.ToString());
                temp.Add(paymentFlag.ToString());
                temp.Add(paymentType.ToString());
                temp.Add(paymentCode.ToString());
                temp.Add(paymentAmount);
                temp.Add(depositPaymentCode.ToString());
                temp.Add(depositPaymentAmount);

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    dibres.Result = keys[0];
                    dibres.OptionalTxt = keys[1];
                }

                return Tuple.Create(apc, dibres);
            }
        }
        #endregion

        #region 18 GetInvalidPickupDates
        public Tuple<APIMethodControl, InvalidDateItem> GetInvalidPickupDatas(BasicStructure basic, int locationId, string categoryId, string pickupYear, string pickupMonth)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var apc = new APIMethodControl();

                var invalidDate = new InvalidDateItem();

                var temp = FillBasic(basic);

                temp.Add(locationId.ToString());
                temp.Add(categoryId);
                temp.Add(pickupYear);
                temp.Add(pickupMonth);

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys[0].ToLower() == errorstring)
                {
                     apc.Succes = false;
                     apc.ErrorCode = keys[1];
                     apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                }

                if (apc.Succes)
                {
                    if (keys[0] == "0")
                    {
                        //0 = Der er ingen invalide pickup datoer.
                        apc.MessageId = "";
                        apc.Message = "Der ingen invalide afhentnings dato'er";
                        invalidDate.InvalidDate = "";
                        invalidDate.DateId = keys[0];
                    }
                    else
                    {
                        invalidDate.InvalidDate = keys[0];
                        invalidDate.DateId = keys[1];
                    }
                }
               
                return Tuple.Create(apc, invalidDate);
            
            }
        }
        #endregion
 
        #region 19 GetInvalidReturnDates
        public Tuple<APIMethodControl, InvalidDateItem> GetInvalidReturnDates(BasicStructure basic, int locationId, string categoryId, string returnYear, string returnMonth, string pickupDate)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var apc = new APIMethodControl();

                var invalidDate = new InvalidDateItem();

                var temp = FillBasic(basic);

                temp.Add(locationId.ToString());
                temp.Add(categoryId);
                temp.Add(returnYear);
                temp.Add(returnMonth);
                temp.Add(pickupDate);

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys[0] == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                }

                if (apc.Succes)
                {
                    if (keys[0].ToLower() == "0")
                    {
                        //0 = Der er ingen invalide pickup datoer.
                        apc.MessageId = "";
                        apc.Message = "Der ingen invalide afhentnings dato'er";
                        invalidDate.InvalidDate = "";
                        invalidDate.DateId = keys[0];
                    }
                    else
                    {
                        invalidDate.InvalidDate = keys[0];
                        invalidDate.DateId = keys[1];
                    }
                }

                return Tuple.Create(apc, invalidDate);

            }
        }
         #endregion

        #region 20 GetOpenHours

        /// <summary>
        /// Function 20 GetOpenHours
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="LocationId"></param>
        /// <param name="date"></param>
        /// <param name="isPickupDate"></param>
        /// <returns></returns>
        public Tuple<APIMethodControl,OpenHours> GetOpenHours(BasicStructure basic, int LocationId, string date, int isPickupDate)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var openhours = new OpenHours();
                var apc = new APIMethodControl();

                var temp = FillBasic(basic);

                temp.Add(LocationId.ToString());
                //temp.Add(date.ToString("ddMMyyyy"));
                temp.Add(date);
                temp.Add(isPickupDate.ToString());

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    openhours.Date = keys[0];
                    openhours.Opentime = keys[1];
                    openhours.CloseTime = keys[2];
                }

                return Tuple.Create(apc, openhours);

             }

        }
        #endregion

        #region 21 PromotionUpdate
        public Tuple<APIMethodControl, List<PromotionInfo>> PromotionUpdate(BasicStructure basic, int locationId, int returnLocationId, string pickupDate, string pickupTime, string returnDate, string returnTime, string categoryId, int productId,
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
            int ExtraProdId_20, string ExtraProdCurrentNumbUnits_20)
        {

            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var promotioninfos = new List<PromotionInfo>();

                var apc = new APIMethodControl();

                var temp = FillBasic(basic);

                temp.Add(locationId.ToString());
                temp.Add(returnLocationId.ToString());
                temp.Add(pickupDate);
                temp.Add(pickupTime);
                temp.Add(returnDate);
                temp.Add(returnTime);
                temp.Add(categoryId.ToString());
                temp.Add(productId.ToString());

                temp.Add(ExtraProdId_01.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_01.ToString());

                temp.Add(ExtraProdId_02.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_02.ToString());

                temp.Add(ExtraProdId_03.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_03.ToString());

                temp.Add(ExtraProdId_04.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_04.ToString());

                temp.Add(ExtraProdId_05.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_05.ToString());

                temp.Add(ExtraProdId_06.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_06.ToString());

                temp.Add(ExtraProdId_07.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_07.ToString());

                temp.Add(ExtraProdId_08.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_08.ToString());

                temp.Add(ExtraProdId_09.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_09.ToString());

                temp.Add(ExtraProdId_10.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_10.ToString());

                temp.Add(ExtraProdId_11.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_11.ToString());

                temp.Add(ExtraProdId_12.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_12.ToString());

                temp.Add(ExtraProdId_13.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_13.ToString());

                temp.Add(ExtraProdId_14.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_14.ToString());

                temp.Add(ExtraProdId_15.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_15.ToString());

                temp.Add(ExtraProdId_16.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_16.ToString());

                temp.Add(ExtraProdId_17.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_17.ToString());

                temp.Add(ExtraProdId_18.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_18.ToString());

                temp.Add(ExtraProdId_19.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_19.ToString());

                temp.Add(ExtraProdId_20.ToString());
                temp.Add(ExtraProdCurrentNumbUnits_20.ToString());

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    for (var i = 0; i < keys.Length; i += 20)
                    {
                        promotioninfos.Add(new PromotionInfo()
                        {
                            Total = keys[i + 0],
                            DepositOnline = keys[i + 1],
                            DepositCash = keys[i + 2],
                            DepositCreditCard = keys[i + 3],
                            TotalDepositOnline = keys[i + 4],
                            TotalDepositCash = keys[i + 5],
                            TotalDepositCreditCard = keys[i + 6],
                            TotalExtraPrice = keys[i + 7],
                            TotalExclusiveTotalExtraPrice = keys[i + 8],
                            BookStatus = Helpers.StringEmptyToInt(keys[i + 9]),
                            BookStatusText = keys[i + 10],
                            PayCashOnCollectFlag = Helpers.StringEmptyToInt(keys[i + 11]),
                            PayCardOnCollectFlag = Helpers.StringEmptyToInt(keys[i + 12]),
                            PayOnlineFlag = Helpers.StringEmptyToInt(keys[i + 13]),
                            DepositDescription = keys[i + 14],
                            RentPricePrDay = keys[i + 15],
                            Promotion = keys[i + 16],
                            PromotionText = keys[i + 17],
                            TotalExclPromotion = keys[i + 18],
                            PickupText = keys[i + 19],
                            
                        });
                    }
                }
                return Tuple.Create(apc, promotioninfos);
            }



        }

        public Tuple<APIMethodControl, List<PromotionInfo>> PromotionUpdate2(BasicStructure basic, Price2 price)
        {

            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var promotioninfos = new List<PromotionInfo>();

                var apc = new APIMethodControl();

                var temp = FillBasic(basic);

                temp.Add(price.locationId.ToString());
                temp.Add(price.returnLocationId.ToString());
                temp.Add(price.pickupDate);
                temp.Add(price.pickupTime);
                temp.Add(price.returnDate);
                temp.Add(price.returnTime);
                temp.Add(price.categoryId.ToString());
                temp.Add(price.productId.ToString());

                foreach (ExtraProduct ep in price.extras)
                {
                    temp.Add(ep.id.ToString());
                    temp.Add(ep.numbUnit.ToString());

                }



                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    for (var i = 0; i < keys.Length; i += 20)
                    {
                        promotioninfos.Add(new PromotionInfo()
                        {
                            Total = keys[i + 0],
                            DepositOnline = keys[i + 1],
                            DepositCash = keys[i + 2],
                            DepositCreditCard = keys[i + 3],
                            TotalDepositOnline = keys[i + 4],
                            TotalDepositCash = keys[i + 5],
                            TotalDepositCreditCard = keys[i + 6],
                            TotalExtraPrice = keys[i + 7],
                            TotalExclusiveTotalExtraPrice = keys[i + 8],
                            BookStatus = Helpers.StringEmptyToInt(keys[i + 9]),
                            BookStatusText = keys[i + 10],
                            PayCashOnCollectFlag = Helpers.StringEmptyToInt(keys[i + 11]),
                            PayCardOnCollectFlag = Helpers.StringEmptyToInt(keys[i + 12]),
                            PayOnlineFlag = Helpers.StringEmptyToInt(keys[i + 13]),
                            DepositDescription = keys[i + 14],
                            RentPricePrDay = keys[i + 15],
                            Promotion = keys[i + 16],
                            PromotionText = keys[i + 17],
                            TotalExclPromotion = keys[i + 18],
                            PickupText = keys[i + 19],

                        });
                    }
                }
                return Tuple.Create(apc, promotioninfos);
            }
        }
        #endregion
        #region PDF 
        public  Tuple<APIMethodControl, byte[]> PDF(BasicStructure basic, int pdfType, string reservationNo, string email)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var promotioninfos = new List<PromotionInfo>();

                var apc = new APIMethodControl();

                var temp = FillBasic(basic);

                temp.Add(pdfType.ToString());
                temp.Add(reservationNo);
                if (email.Length > 0)
                    temp.Add(email);

                var str = Helpers.EncodeString(temp.ToArray());

                var text1 = context.GetData(str);

                var keys = Helpers.DecodeString(text1);

                Byte[] byteArr = null;

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    byteArr = Convert.FromBase64String(Helpers.ConvertStringArrayToString(keys));
                }
                return Tuple.Create(apc, byteArr);
            }

        }
        #endregion
        public Tuple<APIMethodControl, string> ReturnProductDefs(BasicStructure basic)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                
                var apc = new APIMethodControl();

                var temp = FillBasic(basic);

                var str = Helpers.EncodeString(temp.ToArray());

                var text1 = context.GetData(str);

                var keys = Helpers.DecodeString(text1);

                string j = "";

                if (keys[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[1];
                    apc.ErrorMessage = keys[2];
                }
                else
                {
                    apc.Succes = true;
                    
                }
                return Tuple.Create(apc, j);
            }
        
        }

    }
}
