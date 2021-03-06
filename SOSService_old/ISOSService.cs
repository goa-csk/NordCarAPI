﻿using System;
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
        void MakeReservation(Reservation reservation);

        [OperationContract]
        IEnumerable<Agreement> GetReservations();



    }
}
