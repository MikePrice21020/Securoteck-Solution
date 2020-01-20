using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SecuroteckWebApplication.Controllers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CustomAuthoriseAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if(!Thread.CurrentPrincipal.Identity.IsAuthenticated) // If the principle.identity on the current thread is not authenticated
            {
                // Respond with an 'Unauthorised' status code and error
                actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized. Check ApiKey in Header is correct.");
            }
        }

    }
}