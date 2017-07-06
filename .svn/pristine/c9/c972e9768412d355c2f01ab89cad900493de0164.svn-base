using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace NordCar.WebAPI
{
    class ExecutionTimeFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            actionContext.Request.Properties[actionContext.ActionDescriptor.ActionName] = Stopwatch.StartNew();
            Trace.WriteLine(DateTime.Now + " Method :" + actionContext.ActionDescriptor.ActionName + " called"); 
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            Stopwatch stopwatch = (Stopwatch)actionExecutedContext.Request.Properties[actionExecutedContext.ActionContext.ActionDescriptor.ActionName];
            Trace.WriteLine("Method :" + actionExecutedContext.ActionContext.ActionDescriptor.ActionName + "-Elapsed = " + stopwatch.Elapsed);
          
        }
    }
}
