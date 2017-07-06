using FluentValidation;
using NordCar.WebAPI.Models.EC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Validators
{
    public class LoginValidator : AbstractValidator<LoginInfo>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Basic).NotEmpty().WithMessage("The basic section cannot be empty");
            RuleFor(x => x.Basic).SetValidator(new BasicStructure1Validator());
        }
    }
}