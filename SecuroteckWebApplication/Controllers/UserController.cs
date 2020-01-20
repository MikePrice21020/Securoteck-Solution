using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SecuroteckWebApplication.Controllers
{
    public class UserController : ApiController
    {
        // GET api/<controller>
        [ActionName("New")]
        public IHttpActionResult Get([FromUri]string username)
        {
            Models.UserDatabaseAccess userDBaccess = new Models.UserDatabaseAccess();

            if (userDBaccess.Check_user_exists(username) == true)
            {
                return Ok("True -User Does Exist! Did you mean to do a POST to create a new user?");

            }
            else
            {
                return Ok("False -User Does Not Exist! Did you mean to do a POST to create a new user?");
            }

        }



        // POST api/<controller>
        [ActionName("New")]
        public IHttpActionResult Post(HttpRequestMessage request, [FromBody] string username)
        {

            if (username != null)
            {
                Models.UserDatabaseAccess userDBaccess = new Models.UserDatabaseAccess();
                
                Models.User user = userDBaccess.CreateNewUser(username);
                return Ok(user.ApiKey.ToString());
            }
            return Ok("Oops. Make sure your body contains a string with your username and your Content-Type is Content-Type:application/json");
        }




        [ActionName("RemoveUser")]
        [CustomAuthorise]
        // DELETE api/<controller>
        public IHttpActionResult Delete([FromUri]string username)
        {
            string token = null;
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("ApiKey"))
            {
                token = headers.GetValues("ApiKey").First();
            }

            Guid tempGUID = new Guid(token);
            Models.UserDatabaseAccess userDBaccess = new Models.UserDatabaseAccess();
            if (userDBaccess.Check_Username_and_Api_exists(tempGUID, username) == true)
            {
                userDBaccess.Api_deleteUser(tempGUID);

                return Ok("True");
            }
            else
            {
                return Ok("False");
            }
        }





    }
}