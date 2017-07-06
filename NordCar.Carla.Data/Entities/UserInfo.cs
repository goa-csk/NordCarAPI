using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.Carla.Data.Entities
{
    
    public class UserInfo
    {
        public string UserName { get; set; }
        public string StationNo { get; set; }
        public bool MemberOfGroup { get; set; }
    }
}