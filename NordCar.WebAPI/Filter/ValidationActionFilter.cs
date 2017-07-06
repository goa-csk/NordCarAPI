using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace NordCar.WebAPI.Filter
{
    //public class ValidationActionFilter : IActionFilter
    //{
    //    public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
    //    {
    //        if (!actionContext.ModelState.IsValid)
    //        {
    //            var errors = actionContext.ModelState
    //                .Where(e => e.Value.Errors.Count > 0)
    //                .Select(e => new Error
    //                {
    //                    Name = e.Key,
    //                    Message = e.Value.Errors.First().ErrorMessage
    //                }).ToArray();

    //            HttpResponseMessage response = new HttpResponseMessage();
    //            response.StatusCode = HttpStatusCode.BadRequest;
    //            response.Content = new StringContent(errors[0].Message);
    //            actionContext.Response = response;
    //        }
    //    }
    //}
    //public class ValidateViewModelAttribute : IActionFilter
    //{
    //    public override void OnActionExecuting(HttpActionContext actionContext)
    //    {
    //        if (actionContext.ActionArguments.Any(kv => kv.Value == null))
    //        {
    //            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Arguments cannot be null");
    //        }

    //        if (actionContext.ModelState.IsValid == false)
    //        {
    //            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
    //        }
    //    }
    //}
}