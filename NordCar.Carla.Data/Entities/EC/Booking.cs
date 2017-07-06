using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Carla.Data.Entities.EC
{
    public class Booking
    {
        public string ReservationNumber { get; set; }
        public PickDropInfo pickdropinfo { get; set; }
    }
}
