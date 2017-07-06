﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Carla.Data.Entities.EC
{

   

    public class Identification
    {
        public string IdentityNumber { get; set; }
        public string PassPortNumber { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string IssueCountry { get; set; }
    }

    public class Driver
    {
        public string BirthDay { get; set; }
        public string BirthCity { get; set; }
        public string BirthCountry { get; set; }
        public string LicenseNumber { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string IssueCountry { get; set; }
    }

    public class FrequentTravelerProgram
    {
        public string Id { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
     }

    public class Person
    {
        public string CustomerNo { get; set; }
        public string AccountType { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string SecretQuestionId { get; set; }
        public string SecretQuestionAnswer { get; set; }
        public Driver Driver { get; set; }
        public Identification Identification { get; set; }
        public FrequentTravelerProgram FrequentTravelerProgram { get; set; }
    }

    public class Account 
    {
        public Person Private { get; set; }
        public Company Company { get; set; }
       
    }

    public class Company
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ContractNumber { get; set; }
        public string AccountType { get; set; }
        public string AttentionName { get; set; }
    }
}
