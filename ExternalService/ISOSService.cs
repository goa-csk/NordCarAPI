using ExternalService.Services.SOSService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ExternalService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ISOSService
    {

        [OperationContract]
        string Version();

        [OperationContract]
        IEnumerable<Agreement> GetAgreements();

        [OperationContract]
        void MakeReservation(Reservation reservation);

        [OperationContract]
        IEnumerable<Agreement> GetReservations();


       
    }
   
}
