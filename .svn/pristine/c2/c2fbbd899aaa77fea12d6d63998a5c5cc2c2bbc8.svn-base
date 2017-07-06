using NordCar.Carla.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace NordCar.Carla.Data.Entities
{
    public class WebAPIManagerFactory
    {
       
        //private static string ipAddress = string.Empty;
        //private static int portNumber = -1;

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        static WebAPIManagerFactory()
        {
            
          //  ipAddress = ip;
          //  portNumber = port;

        }
        #endregion

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static SocketToCarla CreateContext(string ipAddress, int portNumber, string logfile)
        {
            return new SocketToCarla(ipAddress, portNumber, logfile);
           
        }

        #endregion
    }
}