using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SOSService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class SOSService : ISOSService
    {
        private const string SOSVersionString = "v0.0";
        private static DateTime _serverBuildTime = DateTime.MinValue;

        private static DateTime ServerBuildTime
        {
            get
            {
                if (_serverBuildTime == DateTime.MinValue)
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    var fileInfo = new FileInfo(assembly.Location);
                    _serverBuildTime = fileInfo.LastWriteTime;
                }
                return _serverBuildTime;
            }
        }


        public string Version()
        {
            return string.Format("{2} ({0}, {1})", Environment.MachineName, ServerBuildTime, SOSVersionString);
        }

        public IEnumerable<Agreement> GetAgreements()
        {
            throw new NotImplementedException();
        }

        public void MakeReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Agreement> GetReservations()
        {
            throw new NotImplementedException();
        }
    }
}
