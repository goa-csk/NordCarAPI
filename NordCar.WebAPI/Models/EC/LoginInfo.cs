﻿using FluentValidation.Attributes;
using NordCar.WebAPI.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Models.EC
{
    public enum LoginType
    {
        Personal = 0,
        CustomerNo = 1, //KundeId
        DL = 2, //Driver license
        GWID = 3, //Greenway Id
        BirthDate = 4, 
        Mail = 5,
        Company = 100, //Firma
        ContractNo = 101,
        Agreement = 200 //Hvis Firma ikke går godt 
    }

    [Validator(typeof(LoginValidator))]
    public class LoginInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public LoginType LoginType { get; set; }
        public BasicStructure1 Basic { get; set; }  
       
    }
}