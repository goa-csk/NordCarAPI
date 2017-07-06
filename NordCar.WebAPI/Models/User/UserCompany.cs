﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Models.User
{
    public class UserCompany
    {
        public int CustomerId { get; set; }
        public int CompanyDealId { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNo { get; set; }
        public string MobilePhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public string CVRNo { get; set; }
        public bool NewsLetter { get; set; }
        public bool SMSService { get; set; }
        public string CompanyContact { get; set; }
        public string CompanyContactInfo { get; set; }

    }
}