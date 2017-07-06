using NordCar.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Messages
{
    public class OverviewResponse
    {
        public List<CarOverviewItem> carOverview;
    }
}