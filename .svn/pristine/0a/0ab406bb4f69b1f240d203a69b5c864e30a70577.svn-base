using System;
using System.CodeDom;
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
    public class TestCustomerPrivate
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
        public void GetCustomerPrivateNumbers_Nordcar_no_date_Test()
        {
            //Should return all customers
            var x = ccm.GetCustomerPrivateNumbers(fillbasics("NordCar"), null,null,null);
            string name = x.Item2.FirstOrDefault(c => c.CustomerPrivateNumber == "10002").FirstName;
            Assert.AreEqual(name, "TEST");
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
        public void GetCustomerPrivateNumbers_no_date_Test()
        {
            //Should return all customers
            var x = ccm.GetCustomerPrivateNumbers(fillbasics("ECBOOK"),null,null,null);
            string name = x.Item2.FirstOrDefault(c => c.CustomerPrivateNumber == "10002").FirstName;
            Assert.AreEqual(name,"TEST");
        }

        [TestMethod]
        [TestCategory("Carla")]
        public void GetCustomerPrivate_AllNumbers()
        {
            int step = 1000;
            int startindex = 0;
            int received = 0;
            do
            {
                //Should return all customers
                var x = ccm.GetCustomerPrivateNumbers(fillbasics("ECBOOK"), null, step, startindex);
                received = x.Item2.Count();
                startindex = startindex + received;
                var res = startindex % received == 0;
                Debug.WriteLine(string.Format("Result: {0} NoOfRecords {1}, TotalNoReceived {2}", res.ToString(), received, startindex));

            } while (startindex % received == 0);


        }

        [TestMethod]
        [TestCategory("Carla")]
        public void GetCustomerPrivate_AllNumbers_byDate()
        {
            int step = 1000;
            int startindex = 0;
            DateTime dt = new DateTime(2017, 6, 28, 15, 9, 0);
            int received = 0;
            do
            {
                //Should return all customers
                var x = ccm.GetCustomerPrivateNumbers(fillbasics("ECBOOK"), dt, step, startindex);
                received = x.Item2.Count();
                startindex = startindex + received;
                var res = startindex % received == 0;
                Debug.WriteLine(string.Format("Result: {0} NoOfRecords {1}, TotalNoReceived {2}", res.ToString(), received, startindex));
                var y = x.Item2.Select(v => v.CustomerPrivateNumber).ToArray();
             
                var cust = ccm.GetListCustomerPrivate(fillbasics("ECBOOK"), y.ToList());
                var g = cust.Item2;

            } while (startindex % received == 0);


        }

        [TestMethod]
        [TestCategory("Carla")]
        public void GetCustomerPrivate()
        {
            //Should return all customers
            var x = ccm.GetCustomerPrivate(fillbasics("ECBOOK"),"10565");
            string name = x.Item2.FirstName;
            Assert.AreEqual(name, "ANNETTE");
        }

        [TestMethod]
        [TestCategory("Carla")]
        public void GetListCustomerPrivate()
        {
            //Should return all customers
            var x = ccm.GetListCustomerPrivate(fillbasics("ECBOOK"),new List<string>() {"10565"});
            string name = x.Item2[0].FirstName;
            Assert.AreEqual(name, "ANNETTE");
        }

        [TestMethod]
        [TestCategory("Carla")]
        public void Delete_CustomerPrivateTest()
        {
            var x = ccm.CreateCustomerPrivate(fillbasics("ECBOOK"), CustomerPrivateTestFactory.ECCreateCustomer());
            var custno = x.Item2;
            Assert.IsNotNull(x);
            var y = ccm.DeleteCustomerPrivate(fillbasics("ECBOOK"),custno);
            Assert.AreEqual(y.Item1.Succes, true);

        }

        [TestMethod]
        [TestCategory("Carla")]
        public void UpdateCustomerPrivate()
        {
            //Should return all customers
            var x = ccm.GetCustomerPrivate(fillbasics("ECBOOK"), "10565");
            string name = x.Item2.FirstName;
            Assert.AreEqual(name, "ANNETTE");
            x.Item2.CommentToStatus = x.Item2.CommentToStatus + "0";
            var y = ccm.UpdateCustomerPrivate(fillbasics("ECBOOK"), x.Item2);
            Assert.AreEqual(y.Item1.Succes, true);
        }

    }
}
