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
        public IHttpActionResult Counters()
        {
            CounterUtil counter = new CounterUtil();
            return Ok(new { CounterModelList = counter.Counters() });
        }

        [HttpPost]
        [Route("Add")]
        public IHttpActionResult Add([FromBody] CounterAddModel dto)
        {
            CounterUtil counterUtil = new CounterUtil();
            counterUtil.account_id = dto.account_id;
            counterUtil.name = dto.name;
            counterUtil.description = dto.description;
            counterUtil.status = CounterUtil.NotInUse;
            counterUtil.date_created = DateTime.UtcNow;
            counterUtil.is_active = true;

            int rows_affected = counterUtil.CounterAdd();

            if (rows_affected > 0)
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
