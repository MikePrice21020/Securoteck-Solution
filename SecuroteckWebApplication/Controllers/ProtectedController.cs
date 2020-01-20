using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace SecuroteckWebApplication.Controllers
{
    public class ProtectedController : ApiController
    {
        // POST api/<controller>
        [ActionName("Hello")]
        [HttpGet]
        [CustomAuthorise]
        public IHttpActionResult Hello()
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
            string Tempusername = userDBaccess.Retrieve_username(tempGUID);

            return Ok("Hello " + Tempusername);
        }

        [ActionName("SHA1")]
        [HttpGet]
        [CustomAuthorise]
        public IHttpActionResult Sha1([FromUri]string message)
        {

            if (!string.IsNullOrEmpty(message))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                SHA1 sha1Hash = SHA1.Create();
                byte[] bytes_hash = sha1Hash.ComputeHash(bytes);

                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte h in bytes_hash)
                {
                    var hexidecimal = h.ToString("X2");
                    stringBuilder.Append(hexidecimal);
                }

                string finalMsg = stringBuilder.ToString();


                return Ok(finalMsg);
            }
            return Ok("Bad Request");
        }






        [ActionName("SHA256")]
        [HttpGet]
        [CustomAuthorise]
        public IHttpActionResult Sha256([FromUri]string message)
        {

            if (!string.IsNullOrEmpty(message))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                SHA256 sha1Hash = SHA256.Create();
                byte[] bytes_hash = sha1Hash.ComputeHash(bytes);

                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte h in bytes_hash)
                {
                    var hexidecimal = h.ToString("X2");
                    stringBuilder.Append(hexidecimal);
                }

                string finalMsg = stringBuilder.ToString();


                return Ok(finalMsg);
            }
            return Ok("Bad Request");
        }

        [ActionName("getpublickey")]
        [HttpGet]
        [CustomAuthorise]
        public IHttpActionResult getpublickey()
        {
            return Ok(WebApiConfig.publicKey);
        }

        [ActionName("sign")]
        [HttpGet]
        [CustomAuthorise]
        public IHttpActionResult sign([FromUri]string message)
        {

            if (!string.IsNullOrEmpty(message))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                SHA1 sha1Hash = SHA1.Create();
                byte[] bytes_hash = sha1Hash.ComputeHash(bytes);
                byte[] signed_data = WebApiConfig.RSA.SignHash(bytes_hash, HashAlgorithmName.SHA1.Name);

                string finalMsg = BitConverter.ToString(signed_data);


                return Ok(finalMsg);
            }
            return Ok("Bad Request");
            
                //return Ok(WebApiConfig.publicKey);
        }
    }
}