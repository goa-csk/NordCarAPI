using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.WebAPI.Tests
{
    public class Helpers
    {
        public static string ConvertDateTimeToCarlaDateTime(DateTime dtstr)
        {
            return dtstr.ToString("ddMMyyyy");
        }

        public static string ConvertCarlaDateStringToNovicellDateString(string dtstr)
        {
            return string.Format("{0}-{1}-{2}", dtstr.Substring(0, 2), dtstr.Substring(2, 2), dtstr.Substring(4, 4));
        }

        public static string ConvertNovicellDateStringToCarlaDateString(string dtstr)
        {
            return dtstr.Replace("-","");
        }

        public static string ConvertDateTimeToNovicellDateString(DateTime dtstr)
        {
            return dtstr.ToString("dd-MM-yyyy");
        }

        public static string ConvertDateTimeToNovicellTime(DateTime dtstr)
        {
            return dtstr.ToString("hh:mm");
        }
    }
}
