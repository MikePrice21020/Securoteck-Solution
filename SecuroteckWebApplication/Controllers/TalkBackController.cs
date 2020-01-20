using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SecuroteckWebApplication.Controllers
{
    public class TalkBackController : ApiController
    {

        [ActionName("Hello")]
        public string Get()
        {
            return "Hello World";
        }


        [ActionName("Sort")]
        public int[] Get([FromUri]int[] integers)
        {
            //return "[" + integers[0] + "," + integers[1] + "," + integers[2] + "]";
            if (integers.Length == 0)
            {
                return integers;
            }
            else
            {
                Array.Sort(integers);
                return integers;
            }
        }

    }
}
