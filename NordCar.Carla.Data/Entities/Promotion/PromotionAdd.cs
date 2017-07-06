using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Carla.Data.Entities.Promotion
{
    public class PromotionAdd : Promotion
    {
        public int IntervalStart { get; set; }
        public int IntervalEnd { get; set; }
    }
}
