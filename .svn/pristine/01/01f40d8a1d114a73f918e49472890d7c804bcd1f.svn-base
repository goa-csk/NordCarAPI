using SOSService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SOSService
{
    [ServiceContract]
    public interface ISOSService
    {

        [OperationContract]
        string Version();

        [OperationContract]
        IEnumerable<Agreement> GetAgreements();

        [OperationContract]
        [FaultContract(typeof(ECConfigurationFault))]
        string MakeReservation(Reservation reservation);

        [OperationContract]
        IEnumerable<string> GetReservations();

        [OperationContract]
        [FaultContract(typeof(ECConfigurationFault))]
        IEnumerable<Location> GetStations();

        [OperationContract]
        [FaultContract(typeof(ECConfigurationFault))]
        LocationDetail GetStationDetails(int stationNo);

    }
}
