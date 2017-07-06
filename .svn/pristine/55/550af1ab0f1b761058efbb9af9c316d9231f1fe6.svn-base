using FluentValidation;
using NordCar.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Validators
{
    public class BasicStructure1Validator : AbstractValidator<BasicStructure1>
    {
        public BasicStructure1Validator()
        {
            RuleFor(x => x.BookTypes).NotEmpty().WithMessage("BookType cannot be empty");
        }
    }
}