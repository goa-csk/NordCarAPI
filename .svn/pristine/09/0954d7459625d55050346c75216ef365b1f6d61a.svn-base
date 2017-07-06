using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NordCar.Carla.Data.Entities;
using NordCar.Carla.Data.Entities.CustomerPrivate;
using NordCar.Carla.Data.Infrastructure;

namespace NordCar.Carla.Data.Implementation
{
    public partial class ECAPIManagerRepository
    {
        #region Method CRIS - Customer company  
        public Tuple<APIMethodControl, List<CustomerCompanyListItem>> GetCustomerCompanyNumbers(BasicStructure basic, DateTime? changeDateGreaterthan, int? maxAntal, int? startInterval)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var apc = new APIMethodControl();
                var res = new List<CustomerCompanyListItem>();

                var temp = FillBasic((int)FunctionList.GetCustomerCompanyNumbers, basic);
                if (changeDateGreaterthan != null)
                {
                    temp.Add(Helpers.ConvertDateTimeToNovicellDateString(changeDateGreaterthan.Value));
                    temp.Add(Helpers.ConvertDateTimeToNovicellTime(changeDateGreaterthan.Value));
                }
                else
                {
                    temp.Add("");
                    temp.Add("");
                }

                temp.Add(maxAntal?.ToString() ?? "");
                temp.Add(startInterval?.ToString() ?? "");

                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys.Any())
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

                        for (var i = 0; i < keys.Length; i += 2)
                        {
                            res.Add(new CustomerCompanyListItem()
                            {
                                CustomerCompanyNumber = keys[i],
                                CompanyName = keys[i + 1]
                            });
                        }
                    }
                }
                else
                {
                    apc.Succes = true;
                    apc.Message = "No items returned!";
                }

                return Tuple.Create(apc, res);
            }
        }

        public Tuple<APIMethodControl, CustomerCompany> GetCustomerCompany(BasicStructure basic, string customerCompanyNumber)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var apc = new APIMethodControl();
                var res = new CustomerCompany();

                basic.CustomerId = customerCompanyNumber;
                var temp = FillBasic((int)FunctionList.GetCustomerCompany, basic);

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
                    res.CustomerCompanyNumber = keys[0];
                    res.Brand = keys[2];
                    res.CompanyName = keys[3];
                    res.AdressLine1 = keys[4];
                    res.AdressLine2 = keys[5];
                    res.AdressLine3 = keys[6];
                    res.City = keys[7];
                    res.ZipCode = keys[8];
                    res.Country = keys[9];
                    res.CompanyEmail = keys[10];
                    res.Phone = keys[11];
                    res.ContactName = keys[12];
                    res.ContactEmail = keys[13];
                    res.AgreementNumber = keys[14];
                    res.Comment = keys[15];
                    res.Status = (CustomerStatus)int.Parse(keys[16]);
                    res.CommentToStatus = keys[17];
                    res.ChangeTimeStamp = keys[18];
                    res.GW_ContractNumber = keys[19];
                    res.GW_ParentContractNumber = keys[20];
                    res.IATANumber = keys[21];
                    res.DebitorNumber = keys[22];
                    res.CVRNumber = keys[23];
                    res.EANNumber = keys[24];
                    res.Fax = keys[25];
                    res.AccountResponsableSuperiorId = keys[26];
                    res.AccountResponsableLocaleId = keys[27];
                    res.BillingAddress1 = keys[28];
                    res.BillingAddress2 = keys[29];
                    res.BillingZipcode = keys[30];
                    res.BillingCity = keys[31];
                    res.BillingCountry = keys[32];
                    res.BillingEmail = keys[33];
                    res.CRMId = keys[34];
                    res.DiscountTerms = keys[35];
                    res.DiscountPercentEmployment = keys[36];
                    res.DiscountPercentTransport = keys[37];
                    res.DiscountPercentMiscellaneous = keys[38];
                    res.Insurance = keys[39];
                    res.RequisitionNumberMandatory = keys[40];
                    res.FullCredit = keys[41];
                    res.AttentionName = keys[42];
                    res.AskNewsletter = keys[43];
                    res.Newsletter = keys[44];
                    res.Wintertires = keys[45];
                    res.Wt_StarDate = keys[46];
                    res.Wt_EndDate = keys[47];
                    res.Wt_PricePrDay = keys[48];
                    res.Wt_MaxPrice = keys[49];
                }

                return Tuple.Create(apc, res);
            }
        }

        public Tuple<APIMethodControl, List<CustomerCompany>> GetListCustomerCompany(BasicStructure basic, List<string> customerCompanyNumbers)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var apc = new APIMethodControl();
                var res = new List<CustomerCompany>();

                basic.CustomerId = "";
                var temp = FillBasic((int)FunctionList.GetCustomerCompany, basic);
                foreach (string number in customerCompanyNumbers)
                {
                    temp.Add(number);
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

                    for (var i = 0; i < keys.Length; i += 50)
                    {
                        res.Add(new CustomerCompany()
                        {
                            CustomerCompanyNumber = keys[i],
                            Brand = keys[i + 2],
                            CompanyName = keys[i + 3],
                            AdressLine1 = keys[i + 4],
                            AdressLine2 = keys[i + 5],
                            AdressLine3 = keys[i + 6],
                            City = keys[i + 7],
                            ZipCode = keys[i + 8],
                            Country = keys[i + 9],
                            CompanyEmail = keys[i + 10],
                            Phone = keys[i + 11],
                            ContactName = keys[i + 12],
                            ContactEmail = keys[i + 13],
                            AgreementNumber = keys[i + 14],
                            Comment = keys[i + 15],
                            Status = (CustomerStatus)int.Parse(keys[i + 16]),
                            CommentToStatus = keys[i+17],
                            ChangeTimeStamp = keys[i + 18],
                            GW_ContractNumber = keys[i + 19],
                            GW_ParentContractNumber = keys[i + 20],
                            IATANumber = keys[i + 21],
                            DebitorNumber = keys[i + 22],
                            CVRNumber = keys[i + 23],
                            EANNumber = keys[i + 24],
                            Fax = keys[i + 25],
                            AccountResponsableSuperiorId = keys[i + 26],
                            AccountResponsableLocaleId = keys[i + 27],
                            BillingAddress1 = keys[i + 28],
                            BillingAddress2 = keys[i + 29],
                            BillingZipcode = keys[i + 30],
                            BillingCity = keys[i + 31],
                            BillingCountry = keys[i + 32],
                            BillingEmail = keys[i + 33],
                            CRMId = keys[i + 34],
                            DiscountTerms = keys[i + 35],
                            DiscountPercentEmployment = keys[i + 36],
                            DiscountPercentTransport = keys[i + 37],
                            DiscountPercentMiscellaneous = keys[i + 38],
                            Insurance = keys[i + 39],
                            RequisitionNumberMandatory = keys[i + 40],
                            FullCredit = keys[i + 41],
                            AttentionName = keys[i + 42],
                            AskNewsletter = keys[i + 43],
                            Newsletter = keys[i + 44],
                            Wintertires = keys[i + 45],
                            Wt_StarDate = keys[i + 46],
                            Wt_EndDate = keys[i + 47],
                            Wt_PricePrDay = keys[i + 48],
                            Wt_MaxPrice = keys[i + 49],
                        });
                    }

                }

                return Tuple.Create(apc, res);
            }
        }

        public Tuple<APIMethodControl, string> CreateCustomerCompany(BasicStructure basic, CustomerCompany customerCompany)
        {

            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var apc = new APIMethodControl();
                var res = "";

                var temp = FillBasic((int)FunctionList.CreateCustomerCompany, basic);
                temp.Add(customerCompany.Brand);
                temp.Add(customerCompany.CompanyName);
                temp.Add(customerCompany.AdressLine1);
                temp.Add(customerCompany.AdressLine2);
                temp.Add(customerCompany.AdressLine3);
                temp.Add(customerCompany.City);
                temp.Add(customerCompany.ZipCode);
                temp.Add(customerCompany.Country);
                temp.Add(customerCompany.CompanyEmail);
                temp.Add(customerCompany.Phone);
                temp.Add(customerCompany.ContactName);
                temp.Add(customerCompany.ContactEmail);
                temp.Add(customerCompany.AgreementNumber);
                temp.Add(customerCompany.Comment);
                var status = (int)customerCompany.Status;
                temp.Add(status.ToString());
                temp.Add(customerCompany.CommentToStatus);
                temp.Add(customerCompany.IATANumber);
                temp.Add(customerCompany.DebitorNumber);
                temp.Add(customerCompany.CVRNumber);
                temp.Add(customerCompany.EANNumber);
                temp.Add(customerCompany.Fax);
                temp.Add(customerCompany.AccountResponsableSuperiorId);
                temp.Add(customerCompany.AccountResponsableLocaleId);
                temp.Add(customerCompany.BillingAddress1);
                temp.Add(customerCompany.BillingAddress2);
                temp.Add(customerCompany.BillingZipcode);
                temp.Add(customerCompany.BillingCity);
                temp.Add(customerCompany.BillingCountry);
                temp.Add(customerCompany.BillingEmail);
                temp.Add(customerCompany.CRMId);
                temp.Add(customerCompany.DiscountTerms);
                temp.Add(customerCompany.DiscountPercentEmployment);
                temp.Add(customerCompany.DiscountPercentTransport);
                temp.Add(customerCompany.DiscountPercentMiscellaneous);
                temp.Add(customerCompany.Insurance);
                temp.Add(customerCompany.RequisitionNumberMandatory);
                temp.Add(customerCompany.FullCredit);
                temp.Add(customerCompany.AttentionName);
                temp.Add(customerCompany.AskNewsletter);
                temp.Add(customerCompany.Newsletter);
                temp.Add(customerCompany.Wintertires);
                temp.Add(customerCompany.Wt_StarDate);
                temp.Add(customerCompany.Wt_EndDate);
                temp.Add(customerCompany.Wt_PricePrDay);
                temp.Add(customerCompany.Wt_MaxPrice);

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


        public Tuple<APIMethodControl, string> UpdateCustomerCompany(BasicStructure basic, CustomerCompany customerCompany)
        {
            var apc = new APIMethodControl();
            var res = "";

            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {


                var temp = FillBasic((int)FunctionList.UpdateCustomerCompany, basic);
                temp.Add(customerCompany.CustomerCompanyNumber);
                temp.Add(customerCompany.Brand);
                temp.Add(customerCompany.CompanyName);
                temp.Add(customerCompany.AdressLine1);
                temp.Add(customerCompany.AdressLine2);
                temp.Add(customerCompany.AdressLine3);
                temp.Add(customerCompany.City);
                temp.Add(customerCompany.ZipCode);
                temp.Add(customerCompany.Country);
                temp.Add(customerCompany.CompanyEmail);
                temp.Add(customerCompany.Phone);
                temp.Add(customerCompany.ContactName);
                temp.Add(customerCompany.ContactEmail);
                temp.Add(customerCompany.AgreementNumber);
                temp.Add(customerCompany.Comment);
                temp.Add(customerCompany.Status.ToString());
                temp.Add(customerCompany.CommentToStatus);
                temp.Add(customerCompany.IATANumber);
                temp.Add(customerCompany.DebitorNumber);
                temp.Add(customerCompany.CVRNumber);
                temp.Add(customerCompany.EANNumber);
                temp.Add(customerCompany.Fax);
                temp.Add(customerCompany.AccountResponsableSuperiorId);
                temp.Add(customerCompany.AccountResponsableLocaleId);
                temp.Add(customerCompany.BillingAddress1);
                temp.Add(customerCompany.BillingAddress2);
                temp.Add(customerCompany.BillingZipcode);
                temp.Add(customerCompany.BillingCity);
                temp.Add(customerCompany.BillingCountry);
                temp.Add(customerCompany.BillingEmail);
                temp.Add(customerCompany.CRMId);
                temp.Add(customerCompany.DiscountTerms);
                temp.Add(customerCompany.DiscountPercentEmployment);
                temp.Add(customerCompany.DiscountPercentTransport);
                temp.Add(customerCompany.DiscountPercentMiscellaneous);
                temp.Add(customerCompany.Insurance);
                temp.Add(customerCompany.RequisitionNumberMandatory);
                temp.Add(customerCompany.FullCredit);
                temp.Add(customerCompany.AttentionName);
                temp.Add(customerCompany.AskNewsletter);
                temp.Add(customerCompany.Newsletter);
                temp.Add(customerCompany.Wintertires);
                temp.Add(customerCompany.Wt_StarDate);
                temp.Add(customerCompany.Wt_EndDate);
                temp.Add(customerCompany.Wt_PricePrDay);
                temp.Add(customerCompany.Wt_MaxPrice);

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

        public Tuple<APIMethodControl, string> DeleteCustomerCompany(BasicStructure basic, string customerCompanyNumber)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var apc = new APIMethodControl();
                var res = "";

                var temp = FillBasic((int)FunctionList.DeleteCustomerCompany, basic);
                temp.Add(customerCompanyNumber);


                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                var keys = Helpers.DecodeString(text);

                if (keys.Any())
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
                        res = keys[0];

                    }
                }
                else
                {
                    apc.Succes = true;
                    apc.Message = "No items returned!";
                }

                return Tuple.Create(apc, res);

            }
        }
        #endregion

        #region Method CRIS - Customer private  

        public Tuple<APIMethodControl, List<CustomerPrivateListItem>> GetCustomerPrivateNumbers(
            BasicStructure basic, DateTime? changeDateGreaterthan, int? maxAntal, int? startInterval)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var apc = new APIMethodControl();
                var res = new List<CustomerPrivateListItem>();

                var temp = FillBasic((int)FunctionList.GetCustomerPrivateNumbers, basic);
                if (changeDateGreaterthan != null)
                {
                    temp.Add(Helpers.ConvertDateTimeToNovicellDateString(changeDateGreaterthan.Value));
                    temp.Add(Helpers.ConvertDateTimeToNovicellTime(changeDateGreaterthan.Value));
                }
                else
                {
                    temp.Add("");
                    temp.Add("");
                }

                temp.Add(maxAntal?.ToString() ?? "");
                temp.Add(startInterval?.ToString() ?? "");


                var str = Helpers.EncodeString(temp.ToArray());

                var text = context.GetData(str);

                 

                var keys = Helpers.DecodeString(text);

                if (keys.Any())
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

                        for (var i = 0; i < keys.Length; i += 3)
                        {
                            res.Add(new CustomerPrivateListItem()
                            {
                                CustomerPrivateNumber = keys[i],
                                FirstName = keys[i + 1],
                                LastName = keys[i + 2]
                            });
                        }
                    }
                }
                else
                {
                    apc.Succes = true;
                    apc.Message = "No items returned!";
                }

                return Tuple.Create(apc, res);
            }

        }

        public Tuple<APIMethodControl, CustomerPrivate> GetCustomerPrivate(BasicStructure basic, string customerPrivateNumber)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var apc = new APIMethodControl();
                var res = new CustomerPrivate();

                basic.CustomerId = customerPrivateNumber;
                var temp = FillBasic((int)FunctionList.GetCustomerPrivate, basic);

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
                    res.CustomerPrivateNumber = keys[0];
                    res.Brand = keys[2];
                    res.FirstName = keys[3];
                    res.LastName = keys[4];
                    res.AdressLine1 = keys[5];
                    res.AdressLine2 = keys[6];
                    res.AdressLine3 = keys[7];
                    res.City = keys[8];
                    res.ZipCode = keys[9];
                    res.Country = keys[10];
                    res.Email = keys[11];
                    res.Phone = keys[12];
                    res.DateOfBirth = keys[13];
                    res.DriverLicenseNumber = keys[14];
                    res.AgreementNumber = keys[15];
                    res.Comment = keys[16];
                    res.Status = (CustomerStatus)int.Parse(keys[17]);
                    res.CommentToStatus = keys[18];
                    res.ChangeTimeStamp = keys[19];
                    res.CRMId = keys[20];
                    res.CardType1 = keys[21];
                    res.CardType2 = keys[22];
                    res.CardType3 = keys[23];
                    res.CompanyReference = keys[24];
                    res.AskNewsletter = keys[25];
                    res.Newsletter = keys[26];
                    res.Wintertires = keys[27];
                    res.Wt_StarDate = keys[28];
                    res.Wt_EndDate = keys[29];
                    res.Wt_PricePrDay = keys[30];
                    res.Wt_MaxPrice = keys[31];


                }

                return Tuple.Create(apc, res);
            }
        }

        public Tuple<APIMethodControl, string> CreateCustomerPrivate(BasicStructure basic, CustomerPrivate customerPrivate)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var apc = new APIMethodControl();
                var res = "";

                var temp = FillBasic((int)FunctionList.CreateCustomerPrivate, basic);
                temp.Add(customerPrivate.Brand);
                temp.Add(customerPrivate.FirstName);
                temp.Add(customerPrivate.LastName);
                temp.Add(customerPrivate.AdressLine1);
                temp.Add(customerPrivate.AdressLine2);
                temp.Add(customerPrivate.AdressLine3);
                temp.Add(customerPrivate.City);
                temp.Add(customerPrivate.ZipCode);
                temp.Add(customerPrivate.Country);
                temp.Add(customerPrivate.Email);
                temp.Add(customerPrivate.Phone);
                temp.Add(customerPrivate.DateOfBirth);
                temp.Add(customerPrivate.DriverLicenseNumber);
                temp.Add(customerPrivate.Comment);
                temp.Add(customerPrivate.Status.ToString());
                temp.Add(customerPrivate.CommentToStatus);
                temp.Add((customerPrivate.CardType1));
                temp.Add((customerPrivate.CardType2));
                temp.Add((customerPrivate.CardType3));
                temp.Add((customerPrivate.CompanyReference));
                temp.Add(customerPrivate.AskNewsletter);
                temp.Add(customerPrivate.Newsletter);
                temp.Add(customerPrivate.Wintertires);
                temp.Add(customerPrivate.Wt_StarDate);
                temp.Add(customerPrivate.Wt_EndDate);
                temp.Add(customerPrivate.Wt_PricePrDay);
                temp.Add(customerPrivate.Wt_MaxPrice);

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

        public Tuple<APIMethodControl, string> UpdateCustomerPrivate(BasicStructure basic, CustomerPrivate customerPrivate)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var apc = new APIMethodControl();
                var res = "";

                var temp = FillBasic((int)FunctionList.UpdateCustomerPrivate, basic);
                temp.Add(customerPrivate.CustomerPrivateNumber);
                temp.Add(customerPrivate.Brand);
                temp.Add(customerPrivate.FirstName);
                temp.Add(customerPrivate.LastName);
                temp.Add(customerPrivate.AdressLine1);
                temp.Add(customerPrivate.AdressLine2);
                temp.Add(customerPrivate.AdressLine3);
                temp.Add(customerPrivate.City);
                temp.Add(customerPrivate.ZipCode);
                temp.Add(customerPrivate.Country);
                temp.Add(customerPrivate.Email);
                temp.Add(customerPrivate.Phone);
                temp.Add(customerPrivate.DateOfBirth);
                temp.Add(customerPrivate.DriverLicenseNumber);
                temp.Add(customerPrivate.Comment);
                temp.Add(customerPrivate.Status.ToString());
                temp.Add(customerPrivate.CommentToStatus);
                temp.Add((customerPrivate.CardType1));
                temp.Add((customerPrivate.CardType2));
                temp.Add((customerPrivate.CardType3));
                temp.Add((customerPrivate.CompanyReference));
                temp.Add(customerPrivate.AskNewsletter);
                temp.Add(customerPrivate.Newsletter);
                temp.Add(customerPrivate.Wintertires);
                temp.Add(customerPrivate.Wt_StarDate);
                temp.Add(customerPrivate.Wt_EndDate);
                temp.Add(customerPrivate.Wt_PricePrDay);
                temp.Add(customerPrivate.Wt_MaxPrice);

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

        public Tuple<APIMethodControl, string> DeleteCustomerPrivate(BasicStructure basic, string customerPrivateNumber)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var apc = new APIMethodControl();
                var res = "";

                var temp = FillBasic((int)FunctionList.DeleteCustomerPrivate, basic);
                temp.Add(customerPrivateNumber);


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

        public Tuple<APIMethodControl, List<CustomerPrivate>> GetListCustomerPrivate(BasicStructure basic, List<string> customerPrivateNumbers)
        {
            using (var context = WebAPIManagerFactory.CreateContext(ip7913, port7913, _logfile))
            {

                var apc = new APIMethodControl();
                var res = new List<CustomerPrivate>();

                basic.CustomerId = "";
                var temp = FillBasic((int)FunctionList.GetCustomerPrivate, basic);
                foreach (string number in customerPrivateNumbers)
                {
                    temp.Add(number);
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

                    for (var i = 0; i < keys.Length; i += 32)
                    {
                        res.Add(new CustomerPrivate()
                        {
                            CustomerPrivateNumber = keys[i],
                            Brand = keys[i + 2],
                            FirstName = keys[i + 3],
                            LastName = keys[i + 4],
                            AdressLine1 = keys[i + 5],
                            AdressLine2 = keys[i + 6],
                            AdressLine3 = keys[i + 7],
                            City = keys[i + 8],
                            ZipCode = keys[i + 9],
                            Country = keys[i + 10],
                            Email = keys[i + 11],
                            Phone = keys[i + 12],
                            DateOfBirth = keys[i + 13],
                            DriverLicenseNumber = keys[i + 14],
                            AgreementNumber = keys[i + 15],
                            Comment = keys[i + 16],
                            Status = (CustomerStatus)int.Parse(keys[i + 17]),
                            CommentToStatus = keys[i + 18],
                            ChangeTimeStamp = keys[i + 19],
                            CRMId = keys[i + 20],
                            CardType1 = keys[i + 21],
                            CardType2 = keys[i + 22],
                            CardType3 = keys[i + 23],
                            CompanyReference = keys[i + 24],
                            AskNewsletter = keys[i + 25],
                            Newsletter = keys[i + 26],
                            Wintertires = keys[i + 27],
                            Wt_StarDate = keys[i + 28],
                            Wt_EndDate = keys[i + 29],
                            Wt_PricePrDay = keys[i + 30],
                            Wt_MaxPrice = keys[i + 31]

                        });
                    }

                }

                return Tuple.Create(apc, res);
            }
        }


        #endregion
    }
}
