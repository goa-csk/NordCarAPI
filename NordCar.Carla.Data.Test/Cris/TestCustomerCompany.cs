﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NordCar.Carla.Data.Entities;
using System.Net;
using System.Net.Sockets;
using NordCar.Carla.Data.Entities.CustomerPrivate;
using NordCar.Carla.Data.Implementation;
using NordCar.Carla.Data.Repository;

namespace NordCar.Carla.Data.Test.Cris
{
    [TestClass]
    public class TestCustomerCompany
    {
        private const string ip = "192.168.16.98";
        private const int port = 1074;
        private const string logfile = @"";
        private ECAPIManagerRepository ccm = null;


        private static BasicStructure fillbasics(string booktypes)
        {
            return new BasicStructure() { Language = LanguageList.DA, BookTypes = booktypes, IPAddress = ip, CompanyDealId = "", CustomerId = "", ExtraId = "", VoucherCode = "", OrgBookNr = "", StepNr = "" };
        }

        [TestInitialize]
        public void Initialize()
        {
            ccm = new ECAPIManagerRepository(ip, port, logfile);
        }


        [TestMethod]
        [TestCategory("Carla")]
        public void HelloWorld()
        {
            var x = ccm.GetVersion(fillbasics("ECBOOK"));
            Assert.IsNotNull(x);
        }

        [TestMethod]
        [TestCategory("Carla")]
        public void GetCustomerCompanyNumbers_no_date_Test()
        {
            //Should return all customers
            var x = ccm.GetCustomerCompanyNumbers(fillbasics("ECBOOK"),null,null,null);
            string name = x.Item2.Where(c => c.CustomerCompanyNumber == "10194").FirstOrDefault().CompanyName;
            Assert.AreEqual(name,"DR");
        }

        [TestMethod]
        [TestCategory("Carla")]
        public void GetCustomerCompany()
        {
            //Should return all customers
            var x = ccm.GetCustomerCompany(fillbasics("ECBOOK"),"10012");
            string name = x.Item2.CompanyName;
            Assert.AreEqual(name, "ARLA FOODS");
        }

        [TestMethod]
        [TestCategory("Carla")]
        public void GetCustomerCompany_AllNumbers()
        {
            int step = 1000;
            int startindex = 0;
            int received = 0;
            do
            {
                //Should return all customers
                var x = ccm.GetCustomerCompanyNumbers(fillbasics("ECBOOK"), null, step, startindex);
                received = x.Item2.Count();
                startindex = startindex + received;
                var res = startindex%received == 0;
                Debug.WriteLine(string.Format("Result: {0} NoOfRecords {1}, TotalNoReceived {2}",res.ToString(),received,startindex ));
               
            } while (startindex % received == 0);

       
        }

        [TestMethod]
        [TestCategory("Carla")]
        public void GetCustomerCompany_AllNumbers_byDate()
        {
            int step = 1000;
            int startindex = 0;
            DateTime dt = new DateTime(2017,6,22,8,20,50);
            int received = 0;
            do
            {
                //Should return all customers
                var x = ccm.GetCustomerCompanyNumbers(fillbasics("ECBOOK"), dt, step, startindex);
                received = x.Item2.Count();
                startindex = startindex + received;
                var res = startindex % received == 0;
                Debug.WriteLine(string.Format("Result: {0} NoOfRecords {1}, TotalNoReceived {2}", res.ToString(), received, startindex));

            } while (startindex % received == 0);


        }

        [TestMethod]
        [TestCategory("Carla")]
        public void GetCustomerCompanyList()
        {
            //Should return all customers
            var x = ccm.GetListCustomerCompany(fillbasics("ECBOOK"),new List<string> {"10012"});

            string name = "";

            if (x.Item1.Succes)
            {
                foreach (var company in x.Item2)
                {
                    name = company.CompanyName;
                }
            }
           
            Assert.AreEqual(name, "ARLA FOODS");
        }
        [TestMethod]
        [TestCategory("Carla")]
        public void GetCustomerCompany_10645()
        {
            //Should return all customers
            var x = ccm.GetCustomerCompany(fillbasics("ECBOOK"), "10645");

            string name = x.Item2.CompanyName;
              

        }

        [TestMethod]
        [TestCategory("Carla")]
        public void CreateCustomerCompanyTest()
        {
            var x = ccm.CreateCustomerCompany(fillbasics("ECBOOK"), CustomerCompanyTestFactory.ECCreateCustomerCompany());
            Assert.IsNotNull(x);
        }

        [TestMethod]
        [TestCategory("Carla")]
        public void UpdateCustomerCompanyTest()
        {
            var x = ccm.GetCustomerCompany(fillbasics("ECBOOK"), "30069");

            x.Item2.City = "Yankeebar";

            var y = ccm.UpdateCustomerCompany(fillbasics("ECBOOK"),x.Item2);
            Assert.IsNotNull(y);
        }
    }
}
