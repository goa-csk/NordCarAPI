﻿using NordCar.Carla.Data.Entities;
using NordCar.Carla.Data.Entities.DiscountSheet;
using NordCar.Carla.Data.Entities.EC;
using NordCar.Carla.Data.Entities.Promotion;
using NordCar.Carla.Data.Infrastructure;
using NordCar.Carla.Data.Repository;
using NordCar.Carla.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NordCar.Carla.Data.Entities.MicroSite;
using NordCar.Carla.Data.Entities.CustomerPrivate;

namespace NordCar.Carla.Data.Implementation
{
    public partial class ECAPIManagerRepository : IECAPIManagerRepository
    {
        const string errorstring = "err";

        private string ip7913;
        private int port7913;
        private string _logfile;

        public ECAPIManagerRepository(string ip, int port, string logfile)
        {
            ip7913 = ip;
            port7913 = port;
            _logfile = logfile;
        }

        public Tuple<APIMethodControl, string> GetVersion(BasicStructure basic)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
               
                var apc = new APIMethodControl();

                string ret = "";

                string[] temp =  new string[1] {((int)FunctionList.Hello).ToString()};

                var str = Helpers.EncodeString(temp);

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
                    ret = keys[0] + " - " + keys[2];
                  
                    


                }

                return Tuple.Create(apc, ret);
            }
        }

        #region 17 DibsResult
        public Tuple<APIMethodControl, DibsResultItem> DibsResult(BasicStructure basic, int bookingId, int paymentFlag, int paymentType, int paymentCode, int paymentAmount, int depositPaymentCode, int depositPaymentAmount)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var apc = new APIMethodControl();

                var dibres = new DibsResultItem();

                var temp = FillBasic((int)FunctionList.ECDibsResult,basic);

                temp.Add(bookingId.ToString());
                temp.Add(paymentFlag.ToString());
                temp.Add(paymentType.ToString());
                temp.Add(paymentCode.ToString());
                temp.Add(paymentAmount.ToString());
                temp.Add(depositPaymentCode.ToString());
                temp.Add(depositPaymentAmount.ToString());

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
       
        #region 24 GetLocations
        public Tuple<APIMethodControl, List<NordCar.Carla.Data.Entities.EC.Location>>  GetLocations(BasicStructure basic, string countryId, string carGroupId)
        {

            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var mylocations = new List<NordCar.Carla.Data.Entities.EC.Location>();

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.GetLocations,basic);
                temp.Add(countryId);
                temp.Add(carGroupId);

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
                        string _locationId = keys[i];
                        string _name = keys[i + 1];
                        string _type = keys[i + 2];
                        string _lat = keys[i + 3];
                        string _long = keys[i + 4];

                        mylocations.Add(new NordCar.Carla.Data.Entities.EC.Location() { Id = _locationId, Name = _name, Type = _type, Latitude = _lat, Longitude = _long });

                    }

                    
                }
                
                return Tuple.Create(apc, mylocations);
            }
        }
        #endregion

        #region 25 GetLocationDetails
        public Tuple<APIMethodControl, Entities.EC.LocationDetail> GetLocationDetails(BasicStructure basic, string id, DateTime StartDate, int PeriodLengthInDays)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var mylocationDetails = new NordCar.Carla.Data.Entities.EC.LocationDetail();

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.GetLocationDetails, basic);

                temp.Add(id);

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString2(text);

                if (keys[0].Data[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[0].Data[1];
                    apc.ErrorMessage = keys[0].Data[2];
                }
                else
                {
                    apc.Succes = true;
                    foreach (ReceivedLine rl in keys)
                    {
                       var openhours = new List<NordCar.Carla.Data.Entities.EC.OpeningHours>();

                       string _locationId = rl.Data[0];
                       string _stationName = rl.Data[1];
                       string _type = rl.Data[2];
                       string _address = rl.Data[3];
                       string _zipCode = rl.Data[4];
                       string _city = rl.Data[5];
                       string _country = rl.Data[6];
                       string _phone = rl.Data[7];
                       string _email = rl.Data[8];
                       string _code = rl.Data[9];
                       string _lat = rl.Data[10];
                       string _long = rl.Data[11];
                        string _toptext = rl.Data[12];

                        for (int i = rl.ListInLIstIndex; i < rl.Data.Length; i += 6)
                        {
                            string _name = rl.Data[i];
                            string _open = rl.Data[i + 1];
                            string _opentime = rl.Data[i + 2];
                            string _closetime = rl.Data[i + 3];
                            string _dayNumber = rl.Data[i + 4];
                            string _extraCharges = rl.Data[i + 5];

                            openhours.Add(new NordCar.Carla.Data.Entities.EC.OpeningHours { Name = _name, Open = Helpers.ConvertStringToBool2(_open), OpeningHour = _opentime, ClosingHour = _closetime, DayOfWeek = _dayNumber, ExtraCharges = Helpers.ConvertStringToBool(_extraCharges) });
                        }


                        mylocationDetails = new NordCar.Carla.Data.Entities.EC.LocationDetail() { Id = _locationId, LocationCode = _code, Type = _type, Address = _address, ZipCode = _zipCode, Country = _country, Email = _email, Phone = _phone, StationName = _stationName, City = _city, Latitude = _lat, Longitude = _long, TopText = _toptext, OpeningHours = openhours };

                    }

                   
                    var g = GetLocationDateExceptions(basic, id, StartDate, PeriodLengthInDays);
                    mylocationDetails.Exceptions = g.Item2;

                }

                return Tuple.Create(apc, mylocationDetails);
            }
        }
        #endregion

        #region 26 GetCountries
        public Tuple<APIMethodControl, List<Entities.EC.Country>> GetCountries(BasicStructure basic)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var mycountries = new List<NordCar.Carla.Data.Entities.EC.Country>();

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.GetCountries,basic);

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

                    for (var i = 0; i < keys.Length; i += 2)
                    {
                        Debug.WriteLine(string.Format("i={0},loc={1}", i, keys[i]));
                        string _shortCountry = keys[i];
                        string _countryName = keys[i + 1];

                        mycountries.Add(new NordCar.Carla.Data.Entities.EC.Country() { Id = _shortCountry, Name = _countryName });

                    }


                }

                return Tuple.Create(apc, mycountries);
            }
        }
        #endregion

        #region 27 GetAvailableCars
        /// <summary>
        /// GetAvailableCars
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="input"></param>
        /// <returns>CarDetail</returns>
        public Tuple<APIMethodControl, List<Entities.EC.CarDetail>> GetAvailableCars(BasicStructure basic, Entities.EC.PickDropInfo input, string age)
        {
            /*Carla
             * INPUT
             * OUTPUT
             * 
             */
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var mycarDetails = new List<NordCar.Carla.Data.Entities.EC.CarDetail>();

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.GetAvailableCars,basic);

                temp.Add(input.CountryId);
                temp.Add(input.PickUp.LocationId);
                temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(input.PickUp.Date));
                temp.Add(input.PickUp.Time);
                temp.Add(input.DropOff.LocationId);
                temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(input.DropOff.Date));
                temp.Add(input.DropOff.Time);
                temp.Add(input.CarTypeId);
                temp.Add(input.CarGroupId);
                temp.Add(age);

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                //var keys = Helpers.DecodeString(text);
                var keys = Helpers.DecodeString2(text);

                if (keys[0].Data[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[0].Data[1];
                    apc.ErrorMessage = keys[0].Data[2];
                }
                else
                {
                    apc.Succes = true;
                    foreach (ReceivedLine rl in keys)
                    {

                        var options = new List<NordCar.Carla.Data.Entities.EC.Option>();
                        
                        string _carId = rl.Data[0];
                        string _size= rl.Data[1];
                        string _Typeid = rl.Data[2];
                        string _image = rl.Data[3];
                        string _price = rl.Data[4];
                        string _campaignPrice = rl.Data[5];
                        string _pickupLocation = rl.Data[6];
                        string _pickupLocationName = rl.Data[7];
                        string _pickupDate = rl.Data[8];
                        string _pickupTime = rl.Data[9];
                        string _dropoffLocation = rl.Data[10];
                        string _dropoffLocationName = rl.Data[11];
                        string _dropoffDate = rl.Data[12];
                        string _dropoffTime = rl.Data[13];
                        string _acrissCode = rl.Data[14];
                        string _productcode = rl.Data[15];
                        string _productname = rl.Data[16];
                        string _cargroup = rl.Data[17];
                        string _bookStatus = rl.Data[18];
                        string _bookStatusTekst = rl.Data[19];



                        string _TeaserText = rl.Data[20];
                        string _ProductInfo = rl.Data[21];
                        string _Duration = rl.Data[22];
                        string _PriceperDay = rl.Data[23];
                        string _ConfirmationLocationEmail = rl.Data[24];

                        string _RentalPrice  = rl.Data[25];
                        string _YoungDriverFee  = rl.Data[26];
                        string _AirportFee = rl.Data[27];


                        
                        var pickup = new NordCar.Carla.Data.Entities.EC.Trip() { LocationId = _pickupLocation, LocationName = _pickupLocationName, Date = Helpers.ConvertCarlaDateStringToNovicellDateString(_pickupDate), Time = Helpers.ConvertCarlaTimeStringToNovicellTimeString(_pickupTime) };
                        var dropoff = new NordCar.Carla.Data.Entities.EC.Trip() { LocationId = _dropoffLocation, LocationName = _dropoffLocationName, Date = Helpers.ConvertCarlaDateStringToNovicellDateString(_dropoffDate), Time = Helpers.ConvertCarlaTimeStringToNovicellTimeString(_dropoffTime) };



                        for (int i = rl.ListInLIstIndex; i < rl.Data.Length; i += 4)
                        {
                            string _text = rl.Data[i];
                            string _value = rl.Data[i + 1];
                            string _icon = rl.Data[i + 2];
                            string _optionId = rl.Data[i + 3];

                            options.Add(new NordCar.Carla.Data.Entities.EC.Option { Icon = _icon, Text = _text, Value = _value, FeatureId = _optionId });

                        }

                        mycarDetails.Add(new NordCar.Carla.Data.Entities.EC.CarDetail()
                        {
                            //CarId = _carId, //Not used anymore
                            Size = _size,
                            TypeId = _Typeid,
                            Image = _image,
                            Price = _price,
                            CampaignPrice = _campaignPrice,
                            AcrissCode = _acrissCode,
                            ProductId = _productcode,
                            ProductName = _productname,
                            CarGroupId = _cargroup,
                            bookStatus = int.Parse(_bookStatus),
                            bookStatusTekst = _bookStatusTekst,

                            TeaserText = _TeaserText,
                            ProductInfo = _ProductInfo,
                            Duration = _Duration,
                            PriceperDay = _PriceperDay,
                            ConfirmationLocationEmail = _ConfirmationLocationEmail,
                            RentalPrice = _RentalPrice,
                            YoungDriverFee = _YoungDriverFee,
                            AirportFee = _AirportFee,

                            Features = options,
                            PickUp = pickup,
                            DropOff = dropoff
                        });


                    }


                }

                return Tuple.Create(apc, mycarDetails);
            }
        }
        #endregion

        #region 28 GetCarTypes

        public Tuple<APIMethodControl, List<Entities.EC.CarType>> GetCarTypes(BasicStructure basic)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var mycartypes = new List<NordCar.Carla.Data.Entities.EC.CarType>();

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.GetCarTypes,basic);

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

                    for (var i = 0; i < keys.Length; i += 2)
                    {
                        Debug.WriteLine(string.Format("i={0},loc={1}", i, keys[i]));
                        string _id = keys[i];
                        string _name = keys[i + 1];

                        mycartypes.Add(new NordCar.Carla.Data.Entities.EC.CarType() { Id = _id, Name = _name });

                    }


                }

                return Tuple.Create(apc, mycartypes);
            }
        }

        #endregion

        #region 29 GetCarExtras
        public Tuple<APIMethodControl, Entities.EC.CarExtra> GetCarExtras(BasicStructure basic, NordCar.Carla.Data.Entities.EC.PickDropInfo input, string productId, string age)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var mycarextra = new NordCar.Carla.Data.Entities.EC.CarExtra();

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.GetCarExtras,basic);

                temp.Add(input.CountryId);
                temp.Add(input.PickUp.LocationId);
                temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(input.PickUp.Date));
                temp.Add(input.PickUp.Time);
                temp.Add(input.DropOff.LocationId);
                temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(input.DropOff.Date));
                temp.Add(input.DropOff.Time);
                temp.Add(input.CarTypeId);
                temp.Add(productId);
                temp.Add(input.CarGroupId);
                temp.Add(age);
                    
                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

           var keys = Helpers.DecodeString2(text);

           if (keys[0].Data[0].ToLower() == errorstring)
           {
               apc.Succes = false;
               apc.ErrorCode = keys[0].Data[1];
               apc.ErrorMessage = keys[0].Data[2];
           }
           else
           {
               apc.Succes = true;
               foreach (ReceivedLine line in keys)
               {
                   mycarextra.BookingStatus = line.Data[0];
                   mycarextra.BookingStatusTxt = line.Data[1];

                   mycarextra.TeaserText = line.Data[2];
                   mycarextra.ProductInfo = line.Data[3];
                   mycarextra.Duration = line.Data[4];
                   mycarextra.PriceperDay = line.Data[5];
                   mycarextra.ConfirmationLocationEmail = line.Data[6];

                   int Mileagepos = Array.IndexOf(line.Data, "Mileage");
                   int Insurancepos = Array.IndexOf(line.Data, "Insurance");
                   int Recommendedpos = Array.IndexOf(line.Data, "RecommendedExtras");

                

                   mycarextra.Milage = ControlArray(line.Data.Skip(Mileagepos).ToArray());

                   mycarextra.Insurance = ControlArray(line.Data.Skip(Insurancepos).Take(Mileagepos - Insurancepos).ToArray()); ;


                   mycarextra.RecommendedExtras = ControlArray(line.Data.Take(Insurancepos).ToArray());

                  
               }
           }
            
             return Tuple.Create(apc, mycarextra);
           }
        }
        #endregion 

        #region 30 UpdatePrice

        public Tuple<APIMethodControl,Entities.EC.PriceCalculated> UpdatePrice(BasicStructure basic, Entities.EC.PricePart input, string age)
        {

            /*************************************************************************************
            * Parameters for Carla (30 UpdatePrice)
            * ----------------------------------------------------------------------------------
            * INPUT
            * ----------------------------------------------------------------------------------
            * + [Basic Parameters]
            * + Country
            * + FromLocationId
            * + FromLocationDate
            * + FromLocationTime
            * + ToLocationId
            * + ToLocationDate
            * + ToLocationTime
            * + CarTypeId 
            * + SelectedProduct
            * + CarGroupId
            * + "RecommendedExtras" 
            * +  Id    //Sequence    
            * +  Value //
            * + "Insurance"
            * +  Id    //Sequence    
            * +  Value //
            * + "Mileage"
            * +  Id    //Sequence    
            * +  Value //
             */

            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {


                var apc = new APIMethodControl();
                var pricecalc = new Entities.EC.PriceCalculated();

                var temp = FillBasic((int)FunctionList.UpdatePrices,basic);

                temp.Add(input.PickDropInfo.CountryId);
                temp.Add(input.PickDropInfo.PickUp.LocationId);
                temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(input.PickDropInfo.PickUp.Date));
                temp.Add(input.PickDropInfo.PickUp.Time);
                temp.Add(input.PickDropInfo.DropOff.LocationId);
                temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(input.PickDropInfo.DropOff.Date));
                temp.Add(input.PickDropInfo.DropOff.Time);
                temp.Add(input.PickDropInfo.CarTypeId);
                temp.Add(input.ProductId);
                temp.Add(input.PickDropInfo.CarGroupId);
                temp.Add(age);
            
                temp.Add("RecommendedExtras");

                foreach (NordCar.Carla.Data.Entities.EC.SelectedBase ep in input.Extra.RecommendedExtras)
                {
                    temp.Add(ep.Id.ToString());
                    temp.Add(ep.NumbUnit.ToString());

                }

                temp.Add("Insurance");

                foreach (NordCar.Carla.Data.Entities.EC.SelectedBase ep in input.Extra.Insurance)
                {
                    temp.Add(ep.Id.ToString());
                    temp.Add(ep.NumbUnit.ToString());

                }

                temp.Add("Mileage");

                foreach (NordCar.Carla.Data.Entities.EC.SelectedBase ep in input.Extra.Mileage)
                {
                    temp.Add(ep.Id.ToString());
                    temp.Add(ep.NumbUnit.ToString());

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
                    string _total = keys[0];
                    string _depositOnline = keys[1];
                    string _depositCash = keys[2];
                    string _depositCreditcard = keys[3];
                    string _totalDepositOnline = keys[4];
                    string _totalDepositCash = keys[5];
                    string _totalDepositCreditcard = keys[6];
                    string _totalExtraPrice = keys[7];
                    string _totalExclusiveTotalExtraPrice = keys[8];


                    int _BookStatus = Helpers.StringEmptyToInt(keys[9]);
                    string _BookStatusTekst = keys[10];

                    string _TeaserText = keys[11];
                    string _ProductInfo = keys[12];
                    string _Duration = keys[13];
                    string _PriceperDay = keys[14];
                    string _ConfirmationLocationEmail = keys[15];
                    string _RentalPrice = keys[16];
                    string _YoungDriverFee = keys[17];
                    string _AirportFee = keys[18];

                    string _AcceptDepositOnline = keys[19];
                    string _AcceptDepositCash  = keys[20];
                    string _AcceptDepositCreditCard  = keys[21];
                    string _AcceptDepositNonestring = keys[22];
                    string _AcceptDepositNoneText = keys[23];

                    pricecalc.Total = _total;
                    pricecalc.DepositOnline = _depositOnline;
                    pricecalc.DepositCash = _depositCash;
                    pricecalc.DepositCreditCard = _depositCreditcard;
                    pricecalc.TotalDepositOnline = _totalDepositOnline;
                    pricecalc.TotalDepositCash = _totalDepositCash;
                    pricecalc.TotalDepositCreditCard = _totalDepositCreditcard;
                    pricecalc.TotalExtraPrice = _totalExtraPrice;
                    pricecalc.TotalExclusiveTotalExtraPrice = _totalExclusiveTotalExtraPrice;
                    pricecalc.RentalPrice = _RentalPrice;
                    pricecalc.YoungDriverFee = _YoungDriverFee;
                    pricecalc.AirportFee = _AirportFee;

                    pricecalc.BookStatus = _BookStatus;
                    pricecalc.BookStatusTekst = _BookStatusTekst;

                    pricecalc.TeaserText = _TeaserText;
                    pricecalc.ProductInfo = _ProductInfo;
                    pricecalc.Duration = _Duration;
                    pricecalc.PriceperDay = _PriceperDay;
                    pricecalc.ConfirmationLocationEmail = _ConfirmationLocationEmail;

                    pricecalc.AcceptDepositOnline = _AcceptDepositOnline;
                    pricecalc.AcceptDepositCash = _AcceptDepositCash;
                    pricecalc.AcceptDepositCreditCard = _AcceptDepositCreditCard;
                    pricecalc.AcceptDepositNonestring = _AcceptDepositNonestring;
                    pricecalc.AcceptDepositNoneText = _AcceptDepositNoneText;
                    
                }

                return Tuple.Create(apc, pricecalc);
            }
       }

       #endregion

        #region 31 MakeReservation
       /// <summary>
       /// 31 MakeReservation
       /// </summary>
       /// <param name="basic"></param>
       /// <param name="input"></param>
       /// <returns></returns>
       public Tuple<APIMethodControl, RentalInfo> MakeReservation(BasicStructure basic, Entities.EC.Reservation input)
       {
           /*************************************************************************************
            * Parameters for Carla (31 MakeReservation)
            * ----------------------------------------------------------------------------------
            * INPUT
            * ----------------------------------------------------------------------------------
            * + [Basic Parameters]
            * + Country
            * + FromLocationId
            * + FromLocationDate
            * + FromLocationTime
            * + ToLocationId
            * + ToLocationDate
            * + ToLocationTime
            * + CarTypeId 
            * + SelectedProduct
            * + CarGroupId
            * + Title
            * + Gender
            * + FirstName
            * + LastName
            * + BirthDay (dd-mm-yyyy)
            * + Address
            * + Address2
            * + Address3
            * + City
            * + ZipCode
            * + Country
            * + Email
            * + PaymentType
            * + BookStatus
            * + FlightNo
            * + Remarks
            * + PhoneNumber
            * + "RecommendedExtras" 
            * +  Id    //Sequence    
            * +  Value //
            * + "Insurance"
            * +  Id    //Sequence    
            * +  Value //
            * + "Mileage"
            * +  Id    //Sequence    
            * +  Value //
            * ---------------------------------------------------------------------------------
            * OUTPUT
            * ---------------------------------------------------------------------------------
           *************************************************************************************/

           using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
           {
                var apc = new APIMethodControl();
                var info = new RentalInfo();

                var temp = FillBasic((int)FunctionList.MakeReservation,basic);

                temp.Add(input.PickDropInfo.CountryId);
                temp.Add(input.PickDropInfo.PickUp.LocationId);
                temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(input.PickDropInfo.PickUp.Date));
                temp.Add(input.PickDropInfo.PickUp.Time);
                temp.Add(input.PickDropInfo.DropOff.LocationId);
                temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(input.PickDropInfo.DropOff.Date));
                temp.Add(input.PickDropInfo.DropOff.Time);
                temp.Add(input.PickDropInfo.CarTypeId);
                temp.Add(input.ProductId);
                temp.Add(input.PickDropInfo.CarGroupId); 
                temp.Add(input.Title);
                temp.Add(input.Gender);
                temp.Add(input.FirstName);
                temp.Add(input.LastName);
                temp.Add(input.BirthDay);
                temp.Add(input.Address);
                temp.Add(input.Address2);
                temp.Add(input.Address3);
                temp.Add(input.City);
                temp.Add(input.PostCode);
                temp.Add(input.Country);
                temp.Add(input.Email);
                temp.Add(input.paymentType); //1=Online,2=kontant ved fremmøde(depositum=2000),3=kort ved fremmøde(depositum=3000)
                temp.Add(input.BookStatus.ToString());
                temp.Add(input.FlightNo);
                temp.Add(input.Remarks);
                temp.Add(input.PhoneNumber);
                temp.Add(input.CustomerRefefenceNumber);

                temp.Add("RecommendedExtras");
                if (input.Extra != null)
                {
                    foreach (NordCar.Carla.Data.Entities.EC.SelectedBase ep in input.Extra.RecommendedExtras)
                    {
                        temp.Add(ep.Id.ToString());
                        temp.Add(ep.NumbUnit.ToString());

                    }
                }
                temp.Add("Insurance");
                if (input.Extra != null)
                {
                    foreach (NordCar.Carla.Data.Entities.EC.SelectedBase ep in input.Extra.Insurance)
                    {
                        temp.Add(ep.Id.ToString());
                        temp.Add(ep.NumbUnit.ToString());

                    }
                }
                temp.Add("Mileage");
                if (input.Extra != null)
                {
                    foreach (NordCar.Carla.Data.Entities.EC.SelectedBase ep in input.Extra.Mileage)
                    {
                        temp.Add(ep.Id.ToString());
                        temp.Add(ep.NumbUnit.ToString());

                    }
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
                    string commaseparated = keys[10];
                    info.CcEmail = commaseparated.Split(',').ToList();
                }

                return Tuple.Create(apc, info);
            }
        }
        #endregion

        #region 32 SearchBooking
        /// <summary>
        /// 
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="reservationNo"></param>
        /// <param name="email"></param>
        /// <param name="pickupDate"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
       public Tuple<APIMethodControl, List<NordCar.Carla.Data.Entities.EC.Booking>> SearchBooking(BasicStructure basic, string reservationNo, string email, string pickupDate, string lastName)
        {
            /*-------------------------------------------
             * OUTPUT
             * ------------------------------------------
             * 0 ReservationNo
             * 1 CarGroup
             * 2 FromLocationId
             * 3 FromLocationName
             * 4 FromDate
             * 5 FromTime
             * 6 ToLocationId
             * 7 ToLocationName
             * 8 ToDate
             * 9 ToTime
             */

            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
             {
                 List<NordCar.Carla.Data.Entities.EC.Booking> _reservationNumbers = new List<NordCar.Carla.Data.Entities.EC.Booking>();  
                
                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.SearchBooking,basic);

                temp.Add(BookingSearchType(reservationNo, email, Helpers.ConvertNovicellDateStringToCarlaDateString(pickupDate), lastName));
                temp.Add(reservationNo);
                temp.Add(email);
                if (pickupDate != "")
                    temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(pickupDate));
                else
                    temp.Add(pickupDate);
              
                temp.Add(lastName);
             
                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString2(text);

                if (keys[0].Data[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[0].Data[1];
                    apc.ErrorMessage = keys[0].Data[2];
                }
                else
                {
                    apc.Succes = true;
                    foreach (ReceivedLine line in keys)
                    {
                        var pickup = new NordCar.Carla.Data.Entities.EC.Trip() { LocationId = line.Data[2], LocationName = line.Data[3], Date = Helpers.ConvertCarlaDateStringToNovicellDateString(line.Data[4]), Time = Helpers.ConvertCarlaTimeStringToNovicellTimeString(line.Data[5]) };
                        var dropoff = new NordCar.Carla.Data.Entities.EC.Trip() { LocationId = line.Data[6], LocationName = line.Data[4], Date = Helpers.ConvertCarlaDateStringToNovicellDateString(line.Data[8]), Time = Helpers.ConvertCarlaTimeStringToNovicellTimeString(line.Data[9]) };
                        var pickdropinfo = new NordCar.Carla.Data.Entities.EC.PickDropInfo() { CarGroupId = line.Data[1], PickUp = pickup, DropOff = dropoff, CarTypeId = "", CountryId="" };
                        var newBooking = new NordCar.Carla.Data.Entities.EC.Booking() { ReservationNumber = line.Data[0], pickdropinfo = pickdropinfo };
                        
                        

                        _reservationNumbers.Add(newBooking);
                    }
                }

                return Tuple.Create(apc, _reservationNumbers);
            }
        }

      
        
        #endregion

        #region 33 CancelBooking
        public Tuple<APIMethodControl, bool> CancelBooking(BasicStructure basic, string reservationNo)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var apc = new APIMethodControl();
                bool res = false;

                var temp = FillBasic((int)FunctionList.CancelBooking,basic);

                temp.Add(reservationNo.ToString());

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

        #region 34 GetPdfBooking
        public Tuple<APIMethodControl, byte[]> GetPdfBooking(BasicStructure basic, int pdfType, string reservationNo, string email)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var promotioninfos = new List<PromotionInfo>();

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.GetPdfBooking,basic);

                temp.Add(reservationNo);

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

        #region 35 CreateAccount
        /// <summary>
        /// 35 CreateAccount
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public Tuple<APIMethodControl, NordCar.Carla.Data.Entities.EC.Account> CreateAccount(BasicStructure basic, NordCar.Carla.Data.Entities.EC.Person account)
        {
            /*************************************************************************************
             * Parameters for Carla (35 CreateAccount)
             * ----------------------------------------------------------------------------------
             * INPUT
             * ----------------------------------------------------------------------------------
             * + [Basic Parameters]
             * + AccountType
             * + Title
             * + Gender
             * + FirstName
             * + LastName
             * + Address
             * + Address2
             * + Address3
             * + City
             * + ZipCode
             * + Country
             * + Email
             * + Phone
             * + Password
             * + Driver_BirthDay
             * + Driver_BirthCity
             * + Driver_BirthCountry
             * + Driver_LicenseNumber
             * + Driver_IssueDate
             * + Driver_ExpiryDate
             * + Driver_IssueCountry
             * + Identification_IdentityNumber
             * + Identification_PassPortNumber
             * + Identification_IssueDate
             * + Identification_ExpiryDate
             * + Identification_IssueCountry
             * + FrequentTravelerProgram_Id
             * + FrequentTravelerProgram_CardNumber
             * + FrequentTravelerProgramExpiryDate
             * ---------------------------------------------------------------------------------
             * OUTPUT
             * ---------------------------------------------------------------------------------
            *************************************************************************************/

            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {


                var apc = new APIMethodControl();
                var ca = new NordCar.Carla.Data.Entities.EC.Account();

                var temp = FillBasic((int)FunctionList.CreateAccount,basic);

                temp.Add(account.AccountType);
                temp.Add(account.Title);
                temp.Add(account.Gender);
                temp.Add(account.FirstName);
                temp.Add(account.LastName);
                temp.Add(account.Address);
                temp.Add(account.Address2);
                temp.Add(account.Address3);
                temp.Add(account.City);
                temp.Add(account.PostCode);
                temp.Add(account.Country);
                temp.Add(account.Email);
                temp.Add(account.Phone);
                temp.Add(account.Password);

                if (account.Driver != null)
                {
                    temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(account.Driver.BirthDay));
                    temp.Add(account.Driver.BirthCity);
                    temp.Add(account.Driver.BirthCountry);
                    temp.Add(account.Driver.LicenseNumber);
                    temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(account.Driver.IssueDate));
                    temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(account.Driver.ExpiryDate));
                    temp.Add(account.Driver.IssueCountry);
                }
                else
                {
                    temp.Add("");
                    temp.Add("");
                    temp.Add("");
                    temp.Add("");
                    temp.Add("");
                    temp.Add("");
                    temp.Add("");
                }

                if (account.Identification != null)
                {
                    temp.Add(account.Identification.IdentityNumber);
                    temp.Add(account.Identification.PassPortNumber);
                    temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(account.Identification.IssueDate));
                    temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(account.Identification.ExpiryDate));
                    temp.Add(account.Identification.IssueCountry);
                }
                else
                {
                    temp.Add("");
                    temp.Add("");
                    temp.Add("");
                    temp.Add("");
                    temp.Add("");
                }


                temp.Add(account.FrequentTravelerProgram.Id);
                temp.Add(account.FrequentTravelerProgram.CardNumber); 
                temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(account.FrequentTravelerProgram.ExpiryDate));
                temp.Add(account.SecretQuestionId);
                temp.Add(account.SecretQuestionAnswer);

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
                    //if (!string.IsNullOrEmpty(keys[2]))
                    //{
                    //    var person = new Person() { CustomerNo = keys[2] };
                    //    customeraccount.Private = person;

                    //    apc.Succes = true;
                    int i = 2;
                    //}
                    if (!string.IsNullOrEmpty(keys[i+0]))
                    {
                        //Add PrivateInfo
                        var privateaccount = new NordCar.Carla.Data.Entities.EC.Person()
                        {
                            CustomerNo = keys[i + 0],
                            FirstName = keys[i + 1],
                            LastName = keys[i + 2],
                            Address = keys[i + 3],
                            Address2 = keys[i + 4],
                            Address3 = keys[i + 5],
                            City = keys[i + 6],
                            PostCode = keys[i + 7],
                            Country = keys[i + 8],
                            Phone = keys[i + 9],
                            Email = keys[i + 10],
                            AccountType = keys[i + 13],
                            Title = keys.Count() > i + 24 ? keys[i + 24] : "", // Mail from novicell 17-08-2015
                            Gender = keys.Count() > i + 25 ? keys[i + 25] : "", // Mail from novicell 17-08-2015
                            SecretQuestionId = keys.Count() > 26 ? keys[i + 26] : "" // Mail from novicell 17-08-2015
                        };

                        if (!string.IsNullOrEmpty(keys[i + 11]))
                        {
                            var driver = new NordCar.Carla.Data.Entities.EC.Driver
                            {
                                LicenseNumber = keys[i + 11],
                                BirthDay = keys[i + 12],
                                BirthCity = keys[i + 14],
                                BirthCountry = keys[i + 15],
                                IssueDate = keys[i + 16],
                                ExpiryDate = keys[i + 17],
                                IssueCountry = keys[i + 18]
                            };

                            privateaccount.Driver = driver;
                        }

                        if (!string.IsNullOrEmpty(keys[i + 19]) && keys[i + 19] != "0")
                        {
                            var identification = new NordCar.Carla.Data.Entities.EC.Identification
                            {
                                IdentityNumber = keys[i + 19],
                                PassPortNumber = keys[i + 20],
                                IssueDate = keys[i + 21],
                                ExpiryDate = keys[i + 22],
                                IssueCountry = keys[i + 23]
                            };

                            privateaccount.Identification = identification;
                        }

                        ca.Private = privateaccount;

                    }

                    if (!string.IsNullOrEmpty(keys[i + 27]))
                    {
                        //Add CompanyInfo
                        var company = new NordCar.Carla.Data.Entities.EC.Company()
                        {
                            Number = keys[i + 27],
                            Name = keys[i + 28],
                            Address = keys[i + 29],
                            Address2 = keys[i + 30],
                            Address3 = keys[i + 31],
                            City = keys[i + 32],
                            ZipCode = keys[i + 33],
                            Country = keys[i + 34],
                            Phone = keys[i + 35],
                            Email = keys[i + 36],
                            ContractNumber = keys[i + 37],
                            AccountType = keys[i + 38],
                            AttentionName = keys[i + 39]
                        };
                        ca.Company = company;
                    }

                    apc.Succes = true;
                  

                }
                return Tuple.Create(apc, ca);
            }
        }
        #endregion
        
        #region 36 Login

        public Tuple<APIMethodControl, NordCar.Carla.Data.Entities.EC.Account> Login(BasicStructure basic, string loginType, string userId, string password)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {


                var apc = new APIMethodControl();
                var ca = new NordCar.Carla.Data.Entities.EC.Account();

                var temp = FillBasic((int)FunctionList.ECLogin,basic);

                temp.Add(loginType);
                temp.Add(userId);
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
                    if (!string.IsNullOrEmpty(keys[0]))
                    { 
                      //Add PrivateInfo
                      var privateaccount = new NordCar.Carla.Data.Entities.EC.Person()
                        {
                            CustomerNo = keys[0],
                            FirstName = keys[1],
                            LastName = keys[2],
                            Address = keys[3],
                            Address2 = keys[4],
                            Address3 = keys[5],
                            City = keys[6],
                            PostCode = keys[7],
                            Country = keys[8],
                            Phone = keys[9],
                            Email = keys[10],
                            AccountType = keys[13],
                            Title = keys.Count() > 24 ? keys[24] : "", // Mail from novicell 17-08-2015
                            Gender = keys.Count() > 25 ? keys[25] : "", // Mail from novicell 17-08-2015
                            SecretQuestionId = keys.Count() > 26 ? keys[26] : "" // Mail from novicell 17-08-2015
                        };

                      if (!string.IsNullOrEmpty(keys[11]))
                      {
                          var driver = new NordCar.Carla.Data.Entities.EC.Driver
                          {
                              LicenseNumber = keys[11],
                              BirthDay = keys[12],
                              BirthCity = keys[14],
                              BirthCountry = keys[15],
                              IssueDate = keys[16],
                              ExpiryDate = keys[17],
                              IssueCountry = keys[18]
                          };

                          privateaccount.Driver = driver;
                      }

                      if (!string.IsNullOrEmpty(keys[19]) && keys[19] != "0")
                      {
                          var identification = new NordCar.Carla.Data.Entities.EC.Identification
                          {
                              IdentityNumber = keys[19],
                              PassPortNumber = keys[20],
                              IssueDate = keys[21],
                              ExpiryDate = keys[22],
                              IssueCountry = keys[23]
                          };

                          privateaccount.Identification = identification;
                       }

                      ca.Private = privateaccount;

                    }
                    
                    if (!string.IsNullOrEmpty(keys[27]))
                    { 
                        //Add CompanyInfo
                        var company = new NordCar.Carla.Data.Entities.EC.Company() { 
                            Number = keys[27], 
                            Name = keys[28], 
                            Address = keys[29], 
                            Address2 = keys[30], 
                            Address3 = keys[31],
                            City = keys[32],
                            ZipCode = keys[33],
                            Country = keys[34],
                            Phone = keys[35],
                            Email = keys[36],
                            ContractNumber = keys[37],
                            AccountType = keys[38],
                            AttentionName = keys[39]
                        };
                        ca.Company = company;
                    }

                    apc.Succes = true;
                  
                }

                return Tuple.Create(apc, ca);
            }
        }
        #endregion
        
        #region 37 ForgotPassword

        public Tuple<APIMethodControl, string> ForgotPassword(BasicStructure basic, string loginType, string userId)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {


                var apc = new APIMethodControl();
                string _password = "";

                var temp = FillBasic((int)FunctionList.ForgotPassword,basic);

                temp.Add(loginType);
                temp.Add(userId);
                

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
                    _password = keys[1];
                }

                return Tuple.Create(apc, _password);
            }
        }

        #endregion

        #region 38 ModifyAccount

        public Tuple<APIMethodControl, Entities.EC.Account> ModifyAccount(BasicStructure basic, Entities.EC.Person account)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var apc = new APIMethodControl();
                var ca = new NordCar.Carla.Data.Entities.EC.Account();

                var temp = FillBasic((int)FunctionList.ModifyAccount,basic);

                temp.Add(account.AccountType);
                temp.Add(account.Title);
                temp.Add(account.Gender);
                temp.Add(account.FirstName);
                temp.Add(account.LastName);
                temp.Add(account.Address);
                temp.Add(account.Address2);
                temp.Add(account.Address3);
                temp.Add(account.City);
                temp.Add(account.PostCode);
                temp.Add(account.Country);
                temp.Add(account.Email);
                temp.Add(account.Phone);
                temp.Add(account.Password);

                if (account.Driver != null)
                {
                    temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(account.Driver.BirthDay));
                    temp.Add(account.Driver.BirthCity);
                    temp.Add(account.Driver.BirthCountry);
                    temp.Add(account.Driver.LicenseNumber);
                    temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(account.Driver.IssueDate));
                    temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(account.Driver.ExpiryDate));
                    temp.Add(account.Driver.IssueCountry);
                }
                else
                {
                    temp.Add("");
                    temp.Add("");
                    temp.Add("");
                    temp.Add("");
                    temp.Add("");
                    temp.Add("");
                    temp.Add("");
                }

                if (account.Identification != null)
                {
                    temp.Add(account.Identification.IdentityNumber);
                    temp.Add(account.Identification.PassPortNumber);
                    temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(account.Identification.IssueDate));
                    temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(account.Identification.ExpiryDate));
                    temp.Add(account.Identification.IssueCountry);
                }
                else
                {
                    temp.Add("");
                    temp.Add("");
                    temp.Add("");
                    temp.Add("");
                    temp.Add("");
                }

                temp.Add(account.FrequentTravelerProgram.Id);
                temp.Add(account.FrequentTravelerProgram.CardNumber);
                temp.Add(Helpers.ConvertNovicellDateStringToCarlaDateString(account.FrequentTravelerProgram.ExpiryDate));

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
                    //Sender kan customerNo retur andre felter er tomme.
                    //if (!string.IsNullOrEmpty(keys[2]))
                    //{
                    //    var person = new Person() { CustomerNo = keys[2]};
                    //    customeraccount.Private = person;
                    //     apc.Succes = true;
                            
                    //}
                    int i = 2;
                    //}
                    if (!string.IsNullOrEmpty(keys[i + 0]))
                    {
                        //Add PrivateInfo
                        var privateaccount = new NordCar.Carla.Data.Entities.EC.Person()
                        {
                            CustomerNo = keys[i + 0],
                            FirstName = keys[i + 1],
                            LastName = keys[i + 2],
                            Address = keys[i + 3],
                            Address2 = keys[i + 4],
                            Address3 = keys[i + 5],
                            City = keys[i + 6],
                            PostCode = keys[i + 7],
                            Country = keys[i + 8],
                            Phone = keys[i + 9],
                            Email = keys[i + 10],
                            AccountType = keys[i + 13],
                            Title = keys.Count() > i + 24 ? keys[i + 24] : "", // Mail from novicell 17-08-2015
                            Gender = keys.Count() > i + 25 ? keys[i + 25] : "", // Mail from novicell 17-08-2015
                            SecretQuestionId = keys.Count() > 26 ? keys[i + 26] : "" // Mail from novicell 17-08-2015
                        };

                        if (!string.IsNullOrEmpty(keys[i + 11]))
                        {
                            var driver = new NordCar.Carla.Data.Entities.EC.Driver
                            {
                                LicenseNumber = keys[i + 11],
                                BirthDay = keys[i + 12],
                                BirthCity = keys[i + 14],
                                BirthCountry = keys[i + 15],
                                IssueDate = keys[i + 16],
                                ExpiryDate = keys[i + 17],
                                IssueCountry = keys[i + 18]
                            };

                            privateaccount.Driver = driver;
                        }

                        if (!string.IsNullOrEmpty(keys[i + 19]) && keys[i + 19] != "0")
                        {
                            var identification = new NordCar.Carla.Data.Entities.EC.Identification
                            {
                                IdentityNumber = keys[i + 19],
                                PassPortNumber = keys[i + 20],
                                IssueDate = keys[i + 21],
                                ExpiryDate = keys[i + 22],
                                IssueCountry = keys[i + 23]
                            };

                            privateaccount.Identification = identification;
                        }

                        ca.Private = privateaccount;

                    }

                    if (!string.IsNullOrEmpty(keys[i + 27]))
                    {
                        //Add CompanyInfo
                        var company = new NordCar.Carla.Data.Entities.EC.Company()
                        {
                            Number = keys[i + 27],
                            Name = keys[i + 28],
                            Address = keys[i + 29],
                            Address2 = keys[i + 30],
                            Address3 = keys[i + 31],
                            City = keys[i + 32],
                            ZipCode = keys[i + 33],
                            Country = keys[i + 34],
                            Phone = keys[i + 35],
                            Email = keys[i + 36],
                            ContractNumber = keys[i + 37],
                            AccountType = keys[i + 38],
                            AttentionName = keys[i + 39]
                        };
                        ca.Company = company;
                    }

                    apc.Succes = true;
                    
                }

                return Tuple.Create(apc, ca);
            }

        }
        #endregion

        #region 39 GetCarSpecifications
        public Tuple<APIMethodControl, List<NordCar.Carla.Data.Entities.EC.CarSpec>> GetCarSpecifications(BasicStructure basic, string countryId, string cartype, string carGroup, string age)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var mycarDetails = new List<NordCar.Carla.Data.Entities.EC.CarSpec>();

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.GetCarSpecifications,basic);

                temp.Add(cartype);
                temp.Add(carGroup);
                temp.Add(countryId);
                temp.Add(age);

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                //var keys = Helpers.DecodeString(text);
                var keys = Helpers.DecodeString2(text);

                if (keys[0].Data[0].ToLower() == errorstring)
                {
                    apc.Succes = false;
                    apc.ErrorCode = keys[0].Data[1];
                    apc.ErrorMessage = keys[0].Data[2];
                }
                else
                {
                    apc.Succes = true;
                    foreach (ReceivedLine rl in keys)
                    {

                   
                        string _carId = rl.Data[0];
                        //string _name = rl.Data[1];
                        string _size = rl.Data[1];
                        string _acriss = rl.Data[2];

                        mycarDetails.Add(new NordCar.Carla.Data.Entities.EC.CarSpec()
                        {
                            CarTypeId = _carId,
                            CarGroup = _size,
                            AcrissCode = _acriss,  
                            Features =  CarOptions(rl),
                           
                        });


                    }


                }

                    return Tuple.Create(apc, mycarDetails);
                }
            
              
        }

      
        #endregion

        #region 40,41,42 DropDownLists


        public Tuple<APIMethodControl, List<Entities.EC.DropDownListItem>> DropDownLists(int type, BasicStructure basic)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var mylist = new List<NordCar.Carla.Data.Entities.EC.DropDownListItem>();

                var apc = new APIMethodControl();

                var temp = FillBasic(type,basic);

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

                    for (var i = 0; i < keys.Length; i += 2)
                    {
                        Debug.WriteLine(string.Format("i={0},loc={1}", i, keys[i]));
                        string _id = keys[i];
                        string _name = keys[i + 1];

                        mylist.Add(new NordCar.Carla.Data.Entities.EC.DropDownListItem() { Id = _id, Name = _name });

                    }


                }

                return Tuple.Create(apc, mylist);
            }
        }
        #endregion

        #region 43 GetLocationDateExceptions
        public Tuple<APIMethodControl, List<Entities.EC.Exception>> GetLocationDateExceptions(BasicStructure basic, string LocationId, DateTime StartDate, int PeriodLengthInDays)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var ex= new List<NordCar.Carla.Data.Entities.EC.Exception>();

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.GetLocationDateExceptions,basic);

                temp.Add(LocationId);
                temp.Add(StartDate.ToString("ddMMyyyy"));
                temp.Add(PeriodLengthInDays.ToString());

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
             
                    for (var i = 0; i < keys.Length; i += 6)
                    {

                     
                            string _dayNumber = keys[i];
                            string _name = keys[i+1];
                            string _opentime = keys[i+2];
                            string _closetime = keys[i+3];
                            string _date = keys[i+4];
                            string _extraCharges = keys[i+5];

                            ex.Add(new NordCar.Carla.Data.Entities.EC.Exception {  Date = Helpers.ConvertCarlaDateStringToNovicellDateString(_date), Name = _name, OpeningHour = Helpers.ConvertCarlaTimeStringToNovicellTimeString(_opentime), ClosingHour = Helpers.ConvertCarlaTimeStringToNovicellTimeString(_closetime), DayOfWeek = _dayNumber, ExtraCharges = Helpers.ConvertStringToBool(_extraCharges) });
                        }


                 
                 

                }

                return Tuple.Create(apc, ex);
            }
        }
        #endregion

        #region 45 CheckPromotionCode
        public Tuple<APIMethodControl, string> CheckPromotionCode(BasicStructure basic)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                string restxt = "";

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.CheckPromotionCode,basic);

                
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
                    restxt = keys[0].ToString();
                




                }

                return Tuple.Create(apc, restxt);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// FillBasic
        /// </summary>
        /// <param name="basic"></param>
        /// <returns></returns>
        private List<string> FillBasic(int functionNumber, BasicStructure basic)
        {
           
            return new List<string> {
                functionNumber.ToString()
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

        /// <summary>
        /// CarOptions
        /// </summary>
        /// <param name="rl"></param>
        /// <returns></returns>
        private List<Entities.EC.Option> CarOptions(ReceivedLine rl)
        {
            var options = new List<NordCar.Carla.Data.Entities.EC.Option>();

           
   

            for (int i = rl.ListInLIstIndex; i < rl.Data.Length; i += 3)
            {
                if (i != 0)
                {
                    string _text = rl.Data[i+1];
                    string _value = rl.Data[i + 2];
                    string _optionId = rl.Data[i];
                    string _icon = "";
                    options.Add(new NordCar.Carla.Data.Entities.EC.Option { FeatureId = _optionId, Text = _text, Value = _value, Icon = _icon });

                }

            }

            return options;
        }

        /// <summary>
        /// ControlArray
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        private List<object> ControlArray(string[] Data)
        {
            const string TestTeaserText = "50KarakterXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
            var bcs = new List<object>();
            //Read from behind
            for (int i = Data.Length - 1; i > 0; i--)
            {




                if (Data[i].ToUpper() == "CHECKBOX")
                {
                    //7 strings
                    var chkbox = new Checkbox() { ProductId = Data[i - 8], Description = Data[i - 7], Enabled = Helpers.ConvertStringToBool(Data[i - 1]), Price = Data[i - 4], TypeName = "CHECKBOX", TeaserTxt = Data[i-6] };
                    bcs.Add(chkbox);
                    //Recalc
                    i = i - 8;
                }

                if (Data[i].ToUpper() == "DROPDOWN")
                {
                    //7 strings
                    var dropdown = new DropDown() { ProductId = Data[i - 8], Description = Data[i - 7], Enabled = (Data[i - 3] == "Kilometer(ikke muligt)" ? true : false), SelectedValue = int.Parse(Data[i - 3]), Min = int.Parse(Data[i - 2]), Max = int.Parse(Data[i - 1]), Price = Data[i - 4], TypeName = "DROPDOWN", TeaserTxt = Data[i-6] };
                    bcs.Add(dropdown);
                    //Recalc
                    i = i - 8;
                }
                //Bruges ikke af website
                if (Data[i].ToUpper() == "DROPDOWNEXTRA")
                {
                    var ddlitem = new DropDownList() { TypeName = "DROPDOWNEXTRA", TeaserTxt = TestTeaserText };
                    var ddllist = new List<ddlItem>();
                    int res = 0;

                    do
                    {
                        int.TryParse(Data[i - 5], out res);
                        var dditem0 = new ddlItem() { ElementId = Data[i - 3], ElementText = Data[i - 2], ElementPrice = Data[i - 1] };
                        ddllist.Add(dditem0);
                        i = i - 3;

                    } while (res <= 0);

                    ddlitem.ddlItems = ddllist;
                    ddlitem.SelectedElement = Data[i - 1];
                    ddlitem.NoOfElements = int.Parse(Data[i - 2]);

                    ddlitem.Description = Data[i - 3];
                    ddlitem.ProductId = Data[i - 4];


                    bcs.Add(ddlitem);
                }
            }
            return bcs;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reservationNo"></param>
        /// <param name="email"></param>
        /// <param name="pickupDate"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        private string BookingSearchType(string reservationNo, string email, string pickupDate, string lastName)
        {
            string returnVal = "-1";

            if (reservationNo.Trim() != "" && email.Trim() == "" && pickupDate.Trim() == "" && lastName.Trim() == "")
                returnVal = "0";

            if (reservationNo.Trim() != "" && email.Trim() != "")
                returnVal = "1";

            if (pickupDate.Trim() != "" && email.Trim() != "")
                returnVal = "2";

            if (reservationNo.Trim() != "" && lastName.Trim() != "")
                returnVal = "3";

            if (reservationNo.Trim() == "" && email.Trim() == "" && pickupDate.Trim() == "" && lastName.Trim() == "")
                returnVal = "4";

            return returnVal;
        }

        #endregion

        public Tuple<APIMethodControl, List<Entities.EC.CarTypeLocationDetails>> GetCarTypesByLocation(BasicStructure basic, string LocationId, string Country)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                string lastLocId = "";
 
                var mycartypes = new List<NordCar.Carla.Data.Entities.EC.CarTypeLocationDetails>();

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.GetCarTypesByLocation,basic);

                temp.Add(LocationId);
                temp.Add(Country);
            

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

                    for (var i = 0; i < keys.Length; i += 3)
                    {
                        var item = new NordCar.Carla.Data.Entities.EC.CarType() {Id = keys[i+1], Name = keys[i + 2]};

                        if (lastLocId != keys[i])
                        {


                            var masteritem = new NordCar.Carla.Data.Entities.EC.CarTypeLocationDetails() { LocationId = keys[i], CarTypes = new List<Entities.EC.CarType>() { item } };
                            mycartypes.Add(masteritem);
                            lastLocId = keys[i];

                        }
                        else
                        {
                            foreach (NordCar.Carla.Data.Entities.EC.CarTypeLocationDetails b in mycartypes)
                            {
                                if (b.LocationId == keys[i])
                                {
                                    b.CarTypes.Add(item);
                                }
                            }

                        }
                        
                      
                    }


                }

                return Tuple.Create(apc, mycartypes);
            }
        }


        public Tuple<APIMethodControl, ReservationText> GetReservationText(BasicStructure basic, string reservationNo)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                
                var apc = new APIMethodControl();
                var txts = new ReservationText();

                var temp = FillBasic((int)FunctionList.GetReservationText,basic);
                temp.Add(reservationNo);

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys.Length > 0)
                {
                    if (keys[0].ToLower() == errorstring)
                    {
                        apc.Succes = false;
                        apc.ErrorCode = keys[1];
                        apc.ErrorMessage = keys[2];
                    }
                    else
                    {
                        apc.Succes = true;


                        txts.CustomerSectionTxt = keys[0];
                        txts.LocationSectionTxt = keys[1];
                        txts.BillingSectionTxt = keys[2];
                        txts.TermsSectionTxt = keys[3];
                        txts.FooterSectionTxt = keys[4];

                    }
                }
                else 
                {
                    apc.Succes = false;
                    apc.ErrorCode = "";
                    apc.ErrorMessage = "Funktionen findes ikke!";
                
                }

            
                return Tuple.Create(apc, txts);
            }
        }


        public Tuple<APIMethodControl, List<QueueInfo>> GetReservationStatusQueue(BasicStructure basic, string customerAgreementNumber)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                var queueinfos = new List<QueueInfo>();

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.GetReservationStatusQueue,basic);

                temp.Add(customerAgreementNumber);

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys.Count() > 0 )
                {

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
                            string _messageId = keys[i];
                            string _timestamp = keys[i + 1];
                            string _resNumber = keys[i + 2];
                            string _status = keys[i + 3];
                            string _RaNumber = keys[i + 4];

                            queueinfos.Add(new NordCar.Carla.Data.Entities.EC.QueueInfo()
                            {
                                MessageId = _messageId,
                                Timestamp = _timestamp,
                                ReservationNumber = _resNumber,
                                Status = int.Parse(_status),
                                RANumber = _RaNumber

                            });
                        }
                    }
                 }
                else
                {
                 apc.Succes = true;
                }
              

                return Tuple.Create(apc, queueinfos);
            }

        }

        public Tuple<APIMethodControl, string> ReservationStatusQueueMessageProcessed(BasicStructure basic, string messageId, string SOSResponse)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
                string res = "";

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.ReservationStatusQueueMessageProcessed,basic);

                temp.Add(messageId);
                temp.Add(SOSResponse);

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
                    res = keys[0].ToString();
                


                }

                return Tuple.Create(apc, res);
            }
        }

        public Tuple<APIMethodControl, ResRAData> GetResRAData(BasicStructure basic, int typeId, string number)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                CultureInfo provider = CultureInfo.InvariantCulture;
                var res = new ResRAData();

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.GetResRAData,basic);

                temp.Add(typeId.ToString());
                temp.Add(number);

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
                    int i = 0;
                    res.ReservationNumber = keys[i];
                    res.RANumber = keys[i + 1];
                    res.Status = keys[i + 2];
                    res.StationNoOut = keys[i + 3];
                    res.DateOut = keys[i + 4];
                    res.TimeOut = keys[i+5];
                    res.StationNoIn = keys[i + 6];
                    res.DateIn = keys[i + 7];
                    res.TimeIn = keys[i + 8];
                    res.CarRegistrationNumber = keys[i + 9];
                    res.Brand = keys[i + 10];
                    res.Model = keys[i + 11];
                    res.FirstRegistrationDate = DateTime.ParseExact(keys[i + 12], "ddMMyyyy", provider);//Helpers.ConvertCarlaDateTimeString_ToDateTime(keys[i + 12]);
                    res.WinterTires = Helpers.ConvertStringToBool(keys[i + 13]);
                    res.Automatic = Helpers.ConvertStringToBool(keys[i + 14]);
                    res.Towbar = Helpers.ConvertStringToBool(keys[i + 15]);
                    res.ChildSeat = Helpers.ConvertStringToBool(keys[i + 16]);
                    res.GPS = Helpers.ConvertStringToBool(keys[i + 17]);
                    res.ExtraDriver = Helpers.ConvertStringToBool(keys[i + 18]);
                    res.CustomerReferenceNumber = keys[i + 19];
                    res.RACreatedTime = Helpers.ConvertCarlaDateTimeString_ToDateTime(keys[i + 20]);
                    res.InvoiceCurrencyCode = keys[i + 21];
                    res.InvoicedFuelLitre = decimal.Parse(keys[i + 22]);
                    res.InvoicedFuelPrice = decimal.Parse(keys[i + 23]);
                    res.MilageRegisteredOnContract = decimal.Parse(keys[i + 24]);
                }

                return Tuple.Create(apc, res);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basic"></param>
        /// <returns></returns>
        public Tuple<APIMethodControl, List<Promotion>> GetPromotionCodeList(BasicStructure basic)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var res = new List<Promotion>();

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.GetPromotionCodeList,basic);

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

                    for (var i = 0; i < keys.Length; i += 12)
                    {
                        res.Add(new Promotion()
                        {
                             Id = keys[i],
                             Enable = (EnablePromotion)int.Parse(keys[i +1]),
                             CountLimit = int.Parse(keys[i + 2]),
                             FromDate = Helpers.FromLocalCarlaTimeToDateTime(keys[i + 3], "0000"),
                             ToDate = Helpers.FromLocalCarlaTimeToDateTime(keys[i + 4], "0000"),
                             //discountSheets ids 
                             DiscountSheetIds = new List<string>() { keys[i + 6] , keys[i + 9] , keys[i + 10] },
                             UsedCount = int.Parse(keys[i + 5]),
                             DisplayName = keys[i + 7],
                             Description = keys[i + 8],
                             PromotionType = (PromotionType)int.Parse(keys[i + 11]),

                        });
                    }
                }

                return Tuple.Create(apc, res);
            }
        }


        public Tuple<APIMethodControl, string> PromotionCodeAdd(BasicStructure basic, PromotionAdd promotionAdd)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                string res = "";

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.AddPromotionCode,basic);
                temp.Add(promotionAdd.Id); //Not used
                temp.Add(((int)promotionAdd.Enable).ToString());
                temp.Add(promotionAdd.CountLimit.ToString());
                temp.Add(Helpers.ConvertDateTimeToCarlaDateTime(promotionAdd.FromDate));
                temp.Add(Helpers.ConvertDateTimeToCarlaDateTime(promotionAdd.ToDate));
                temp.Add(promotionAdd.IntervalStart.ToString());
                temp.Add(promotionAdd.IntervalEnd.ToString());
                temp.Add(promotionAdd.DiscountSheetIds[0]);
                temp.Add(promotionAdd.DiscountSheetIds[1]);
                temp.Add(promotionAdd.DiscountSheetIds[2]);
                temp.Add("0"); //Not used
                temp.Add(promotionAdd.DisplayName);
                temp.Add(promotionAdd.Description);

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
                    res = keys[0];
                }

                return Tuple.Create(apc, res);
            }
        }

        public Tuple<APIMethodControl, string> PromotionCodeEdit(BasicStructure basic, PromotionAdd promotionEdit)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                string res = "";

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.EditPromotionCode,basic);
                temp.Add(promotionEdit.Id);
                temp.Add(((int)promotionEdit.Enable).ToString());
                temp.Add(promotionEdit.CountLimit.ToString());
                temp.Add(Helpers.ConvertDateTimeToCarlaDateTime(promotionEdit.FromDate.ToLocalTime()));
                temp.Add(Helpers.ConvertDateTimeToCarlaDateTime(promotionEdit.ToDate.ToLocalTime()));
                temp.Add(promotionEdit.DiscountSheetIds[0]);
                temp.Add(promotionEdit.DiscountSheetIds[1]);
                temp.Add(promotionEdit.DiscountSheetIds[2]);
                temp.Add(promotionEdit.UsedCount.ToString());
                temp.Add(promotionEdit.DisplayName);
                temp.Add(promotionEdit.Description);

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
                    res = keys[0];
                    apc.Succes = true;
                }

                return Tuple.Create(apc, res);
            }

        }

        public Tuple<APIMethodControl, Promotion> GetPromotion(BasicStructure basic, string id)
        {
            //Kald Carla direkte
            var promotions = GetPromotionCodeList(basic);
            var promotion = promotions.Item2.FirstOrDefault(p => p.Id == id);
            return Tuple.Create(promotions.Item1, promotion);
        }

        public Tuple<APIMethodControl, List<DiscountSheet>> GetDiscountSheetList(BasicStructure basic)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var res = new List<DiscountSheet>();

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.GetDiscountSheetList,basic);
               
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
                        res.Add(new DiscountSheet()
                        {
                            MatchNo = keys[i],
                            Brand = keys[i + 1],
                            TypeId = keys[i + 2],
                            Description = keys[i + 3],
                            SaleFromDate = Helpers.FromLocalCarlaTimeToDateTime(keys[i + 4], "0000"),
                            SaleToDate = Helpers.FromLocalCarlaTimeToDateTime(keys[i + 5], "0000"),
                            PickupFromDate = Helpers.FromLocalCarlaTimeToDateTime(keys[i + 6], "0000"),
                            PickupToDate = Helpers.FromLocalCarlaTimeToDateTime(keys[i + 7], "0000"),
                            Id = keys[i + 8],
                        });
                    }
                }

                return Tuple.Create(apc, res);
            }

        }

        public Tuple<APIMethodControl, List<MicroSite>> GetMicroSiteList(BasicStructure basic)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var res = new List<MicroSite>();

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.ListMicrosite,basic);

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

                    for (var i = 0; i < keys.Length; i += 3)
                    {
                        res.Add(new MicroSite()
                        {
                            
                            Id = keys[i],
                            Name = keys[i+1],
                            Enabled = Helpers.ConvertStringToBool(keys[i + 2])
                        });
                    }
                }

                return Tuple.Create(apc, res);

            }
        }

        public Tuple<APIMethodControl, string> AddMicroSite(BasicStructure basic, MicroSite microsite)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var res = "";

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.AddMicrosite,basic);
                temp.Add(microsite.Id);
                temp.Add(microsite.Name);
                temp.Add(Helpers.ConvertBoolToString(microsite.Enabled));

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
                    res = keys[0];

                }

                return Tuple.Create(apc, res);

            }
        }

        public Tuple<APIMethodControl, string> EditMicroSite(BasicStructure basic, MicroSite microsite)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var res = "";

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.EditMicrosite,basic);
                temp.Add(microsite.Id);
                temp.Add(microsite.Name);
                temp.Add(Helpers.ConvertBoolToString(microsite.Enabled));

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
                    res = keys[0];

                }

                return Tuple.Create(apc, res);

            }

        }

        public Tuple<APIMethodControl, byte[]> GetDiscountSheetXls(BasicStructure basic, string discountSheetId)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {
               
                var apc = new APIMethodControl();
                //Note must be discountsheet
                var temp = FillBasic((int)FunctionList.GetDiscountSheetXls ,basic);

                temp.Add(discountSheetId);
               
                var str = Helpers.EncodeString(temp.ToArray());

                var text1 = context.GetData(str);

                var keys = Helpers.DecodeString(text1);

                Byte[] byteArr = null;

                if (keys[0].Substring(0,3).ToLower() == errorstring)
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

        public Tuple<APIMethodControl, string> DeletePromotionCode(BasicStructure basic, string id)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var res = "";

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.DeletePromotionCode,basic);
                temp.Add(id);
              
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
                    res = keys[0];

                }

                return Tuple.Create(apc, res);

            }
        }

        /// <summary>
        /// 61-GetBookTypes
        /// </summary>
        /// <param name="basic"></param>
        /// <returns></returns>
        public Tuple<APIMethodControl, List<BookType>> GetBookTypes(BasicStructure basic)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var res = new List<BookType>();

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.GetBookTypes,basic);

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

                    for (var i = 0; i < keys.Length; i += 2)
                    {
                        res.Add(new BookType()
                        {                         
                            Name = keys[i],
                            BookTypeType = (BookTypeType)int.Parse(keys[i + 1])
                        });
                    }
                }

                return Tuple.Create(apc, res);

            }
        }

        public Tuple<APIMethodControl, List<Umbrella>> GetUmbrellaPromotionList(BasicStructure basic, string UmbrellaId)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var res = new List<Umbrella>();

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.GetPromotioUmbrellaList, basic);
                temp.Add(UmbrellaId);

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

                    for (var i = 0; i < keys.Length; i += 13)
                    {
                        res.Add(new Umbrella()
                        {
                            UmbrellaId = keys[i],
                            Id = keys[i + 1],
                            Enable = (EnablePromotion)int.Parse(keys[i + 2]),
                            CountLimit = int.Parse(keys[i + 3]),
                            FromDate = Helpers.FromLocalCarlaTimeToDateTime(keys[i + 4], "0000"),
                            ToDate = Helpers.FromLocalCarlaTimeToDateTime(keys[i + 5], "0000"),
                            //discountSheets ids 
                            DiscountSheetIds = new List<string>() { keys[i + 7], keys[i + 10], keys[i + 11] },
                            UsedCount = int.Parse(keys[i + 6]),
                            DisplayName = keys[i + 8],
                            Description = keys[i + 9],
                            PromotionType = (PromotionType)int.Parse(keys[i + 12]),

                        });
                    }
                }

                return Tuple.Create(apc, res);

            }
        }

        /// <summary>
        /// 60 GetMicrositeSheetXls
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="micrositeSheetId"></param>
        /// <returns></returns>
        public Tuple<APIMethodControl, byte[]> GetMicrositeSheetXls(BasicStructure basic, string micrositeSheetId)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var apc = new APIMethodControl();

                var temp = FillBasic((int)FunctionList.GetMicrositeSheetXls, basic);

                temp.Add(micrositeSheetId);

                var str = Helpers.EncodeString(temp.ToArray());

                var text1 = context.GetData(str);

                var keys = Helpers.DecodeString(text1);

                Byte[] byteArr = null;

                if (keys[0].Substring(0, 3).ToLower() == errorstring)
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

      
    }
}
