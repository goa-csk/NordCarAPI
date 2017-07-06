using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using static NordCar.WebAPI.Messages.MyExceptionHandler;

namespace NordCar.WebAPI.Filter
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
               // var err = new Models.APIMethodControl() { ErrorCode = "", ErrorMessage = "", Succes = false };
               // var content = new NotFoundJSONActionResult(err, actionContext.Request, HttpStatusCode.BadRequest);
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
              //  actionContext.Response = (HttpResponse)content;
            }
        }
    }
}