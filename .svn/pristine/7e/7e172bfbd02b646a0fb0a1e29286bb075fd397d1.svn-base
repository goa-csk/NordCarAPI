using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NordCar.Carla.Data.Entities;
using NordCar.Carla.Data.Entities.EC;
using NordCar.Carla.Data.Implementation;

namespace NordCar.Carla.Data.Test.TxBook
{
    [TestClass]
    public class TestCars
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
        public void GetAvaiableCars()
        {
            var tripstart = new Trip() {Date = "20-05-2017", LocationId = "1", Time = "1000"};
            var tripend = new Trip() { Date = "22-05-2017", LocationId = "1", Time = "1000"};
            var pickdrop = new PickDropInfo() {CarGroupId = "", CarTypeId = "", CountryId = "Udlandet",DropOff = tripend, PickUp = tripstart};
            var x = ccm.GetAvailableCars(fillbasics("TXBOOK"),pickdrop,"28");

            Assert.IsNotNull(x.Item2);
        }
    }
}
