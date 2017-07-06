using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Server.Rest.Api.Dto.Common
{
        public class RequestBase
        {
            /// <summary>
            /// Transaction ID that follows an operation throughout the system
            /// </summary>
            public Guid TransactionId { get; set; }

            /// <summary>
            /// ID of the user creating the event 
            /// </summary>
            public Guid UserId { get; set; }
        }
   
}
