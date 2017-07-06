using FluentValidation;
using NordCar.WebAPI.Models.EC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Validators
{
    public class PersonalAccountValidator : AbstractValidator<PersonalAccount>
    {
        public PersonalAccountValidator()
        {
            RuleFor(x => x.basic).NotEmpty().WithMessage("The basic section cannot be empty");

            RuleFor(x => x.basic).SetValidator(new BasicStructure1Validator());
        }
    }
}