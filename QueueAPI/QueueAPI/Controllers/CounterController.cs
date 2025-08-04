using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QueueAPI.Filters;
using QueueAPI.Models.DTO;
using QueueAPI.Utils;

namespace QueueAPI.Controllers
{
    [AuthenticationFilter]
    [RoutePrefix("api/counter")]
    public class CounterController : ApiController
    {
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult counters()
        {
            CounterUtil counter = new CounterUtil();
            return Ok(new { CounterModelList = counter.Counters() });
        }
    }
}
