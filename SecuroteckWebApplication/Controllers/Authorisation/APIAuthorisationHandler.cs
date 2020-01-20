using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using SecuroteckWebApplication.Models;

namespace SecuroteckWebApplication.Controllers
{
    public class APIAuthorisationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync (HttpRequestMessage request, CancellationToken cancellationToken)
        {
            IEnumerable<string> headerIE;
            if (request.Headers.TryGetValues("ApiKey", out headerIE))
            {
                string headerString = headerIE.First();

                Models.UserDatabaseAccess userDBaccess = new Models.UserDatabaseAccess();
                try
                {
                    Guid api = new Guid(headerString);
                    if (userDBaccess.Check_Api_exists(api))
                    {
                        User user = userDBaccess.Api_exists_Return_Username(api);

                        Thread.CurrentPrincipal = new ClaimsPrincipal(new[]
                        {
                        new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, user.UserName)}, "ApiKey")});

                    }
                }
                catch (FormatException)
                {

                }

            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}