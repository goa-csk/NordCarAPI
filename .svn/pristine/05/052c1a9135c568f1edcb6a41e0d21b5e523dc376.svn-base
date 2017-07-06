using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NordCar.Carla.Data.Infrastructure;

namespace NordCar.Carla.Data.Test
{
    [TestClass]
    public class HelperFunctions
    {

        [TestMethod]
        public void SimpleByte2HexAscii()
        {
            Byte b = 0x5E;
            string expected = "5E";

            var res = Helpers.Bytes2HexAscii(new byte[] { b });
            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        public void SimpleHexAscii2Byte()
        {
            Byte b = 0x5E;

            var res = Helpers.HexAscii2Bytes("5E");
            Assert.AreEqual(1, res.Length);
            Assert.AreEqual(b, res[0]);
        }

        [TestMethod]
        public void FullRoundTrip()
        {
            var data = new Byte[] { 0x11, 0x00, 0x58, 0x9B, 0xFF, 0x92, 0xE6 };
            var strRes = Helpers.Bytes2HexAscii(data);

            Console.WriteLine(strRes);

            var res = Helpers.HexAscii2Bytes(strRes);

            Assert.AreEqual(data.Length, res.Length);
            for (int idx = 0; idx < data.Length; idx++)
                Assert.AreEqual(data[idx], res[idx]);
        }



    }
}
