using FluentValidation.Attributes;
using NordCar.WebAPI.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Models.User
{
    [Validator(typeof(UserRequestValidator))]
    public class UserRequest
    {
        public string Email { get; set; }
        public BasicStructure1 Basic { get; set; }
    }
}