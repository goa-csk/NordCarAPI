using FluentValidation;
using NordCar.WebAPI.Models.EC;
using NordCar.WebAPI.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Validators
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.Basic).NotEmpty().WithMessage("The basic section cannot be empty");
            RuleFor(x => x.Basic).SetValidator(new BasicStructure1Validator());
        }
    }
}