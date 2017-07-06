using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Carla.Data.Entities.EC
{
    public class QueueInfo
    {
        public string MessageId { get; set; }
        public string Timestamp { get; set; }
        public int Status { get; set; }
        public string ReservationNumber { get; set; }
        /// <summary>
        /// Rental Agreement Number
        /// </summary>
        public string RANumber { get; set; }
    }

   
}
