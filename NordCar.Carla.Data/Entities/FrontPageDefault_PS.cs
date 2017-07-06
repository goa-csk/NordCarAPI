using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Carla.Data.Entities
{
    public class FrontPageDefault_PS
    {
        public string PickUpDate { get; set; }
        public string PickupTime { get; set; }
        public string ReturnDate { get; set; }
        public string ReturnTime { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int DefaultLocationNo { get; set; }
        public string DefaultPickupTime { get; set; }
        public string DefaultReturnTime { get; set; }

    }
}
