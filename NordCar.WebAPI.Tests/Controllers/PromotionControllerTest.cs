using Microsoft.VisualStudio.TestTools.UnitTesting;
using NordCar.Carla.Data.Implementation;
using NordCar.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace NordCar.WebAPI.Tests.Controllers
{
    [TestClass]
    public class PromotionControllerTest
    {
        private PromotionController controller = null;

        [TestInitialize]
        public void init()
        {
            //Arrange
            string ip = "192.168.16.98";
            int port = 1074;
            string logfile = "test.log";

            var rep = new ECAPIManagerRepository(ip, port, logfile);
            controller = new PromotionController(rep);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
        }

        #region 50 GetPromotionCodeList
        /// <summary>
        /// GetPromotionCodeList_All
        /// </summary>
        [TestMethod]
        public void GetPromotionCodeList_All()
        {
            //Act
            var response = controller.GetPromotionCodeList("ECBOOK");
            
            //Assert
            Assert.IsNotNull(response);
        }
        #endregion

    }
}
