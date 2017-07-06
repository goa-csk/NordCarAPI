using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOSService;
using NordCar.Carla.Data.EF.Repository;
using NordCar.Carla.Data.Repository;
using log4net;
using NordCar.Carla.Data.Implementation;
using SOSService.Data;

namespace SOSServiceTests
{
    [TestClass]
    public class Sos
    {
        
        private ILog log;
        private ICustomerAgreement custAgreement;
        private IECAPIManagerRepository ecAPIManager;
        
        [TestInitialize]
        public void init()
        {
            //Arrange
            string ip = "192.168.16.98";
            int port = 1074;
            string logfile = "test.log";

            custAgreement = new CustomerAgreementRepo();
            ecAPIManager = new ECAPIManagerRepository(ip, port, logfile);
            log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            
        }

        [TestMethod]
        public void MakeReservationTest()
        {
            /*
             * ************************************************************
             * New reservation 
             * - ECContractNumber = blank
             * - ECReservationNumber = blank
             * Changes to Registration
             * - ECReservationNumber set
             **************************************************************
             * Dato Input er UTC tid
             */
            var reservation = new Reservation(){
                CustomerReferenceNumber ="Test002_A",
                AgreementId = 0,
                ECContractNumer = "",
                ECReservationNumer = "",
                driver = new Driver() { 
                    Name = "Alain Prost",
                    Phone = "34343434",
                    address = new Address() { 
                        Street = "Highway", 
                        City="Vejen", 
                        Country = "DK", 
                        ZipCode = "2345"
                        }},
                 rental = new Rental(){ 
                     AutomaticGear = false,
                     ChilSeat = true,
                     ExtraDriver = false,
                     FuelDeposit = false,
                     MaxGOP = 1000,
                     GPS = true,
                     Towbar = false,
                     WinterTires = false,
                      CarBrand = "BMW",
                      CarModel = "3-Serie",
                      SOSCustomerInfo = "Håber Du/I bliver glad for lånebilen, hilsen SOS",
                      StationOutId = 12,
                      DateOut = "04-12-2016",
                      TimeOut = "08:00",
                      DateIn = "20-12-2016",
                      TimeIn = "09:00",
                      Delivery = true,
                      DeliveryAdress = new Address() { Street = "Leveringsvej 10", ZipCode = "8000", City = "Aarhus", Country = "DK"},
                      ECDepartmentInfo = "Skal vise gyldigt kørekort ved afhentning",
                      Pickup = false,
                      PickupAdress = null
                 }
            }
                                                          ;
            SOSService.SOSService sos = new SOSService.SOSService(custAgreement, ecAPIManager, log);

            var result = sos.MakeReservation(reservation);

            Assert.IsNotNull(result);
        }
    }
}
