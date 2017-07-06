using FluentValidation;
using NordCar.WebAPI.Models.EC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Validators
{
    public class ReservationValidator : AbstractValidator<Reservation>
    {
        public ReservationValidator()
        {
            RuleFor(x => x.PickDropInfo).NotEmpty().WithMessage("The PickDropInfo section cannot be empty");

            RuleFor(x => x.PickDropInfo).SetValidator(new PickDropInfoValidator());

        }
    }
}