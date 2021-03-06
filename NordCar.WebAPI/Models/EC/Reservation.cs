﻿using FluentValidation.Attributes;
using NordCar.WebAPI.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Models.EC
{
    public class SelectedBase
    {
        public string Id { get; set; }
        public string NumbUnit { get; set; }
    }

    [Validator(typeof(ReservationValidator))]
    public class Reservation
    {
        public string Title { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDay { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string CustomerNo { get; set; }
        /// <summary>
        /// 1 = Payment done Online
        /// 2 = Payment done with card upon pickup
        /// 3 = Payment done with cash upon pickup
        /// </summary>
        public string paymentType { get; set; }
        public PickDropInfo PickDropInfo { get; set; }
        public SelectedExtras Extra { get; set; }
        public string ProductId { get; set; }
        /// <summary>
        /// 1=OK 2=Udsolgt 3=Forespørgsel 4=Forespørgsel.m.booking.nr
        /// </summary>
        public int BookStatus { get; set; }
        public string FlightNo { get; set; }
        public string Remarks { get; set; }
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Rekvisitionnummer 
        /// </summary>
        public string RequisitionNumber { get; set; }


    }

    public class SelectedExtras
    {
        public List<SelectedBase> RecommendedExtras { get; set; }
        public List<SelectedBase> Insurance { get; set; }
        public List<SelectedBase> Mileage { get; set; }
    }
}