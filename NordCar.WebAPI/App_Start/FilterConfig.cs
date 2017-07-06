using System.Web;
using System.Web.Mvc;
using NordCar.WebAPI.Filter;

namespace NordCar.WebAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new ValidateViewModelAttribute());
           // filters.Add(new ValidationActionFilter());
        }
    }
}
