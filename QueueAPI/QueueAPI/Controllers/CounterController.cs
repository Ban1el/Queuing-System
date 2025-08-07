using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QueueAPI.Filters;
using QueueAPI.Models.DTO;
using QueueAPI.Utils;
using QueueAPI.Models;
using QueueAPI.Helpers;

namespace QueueAPI.Controllers
{
    [AuthenticationFilter]
    [RoutePrefix("api/counter")]
    public class CounterController : ApiController
    {
        CounterUtil counterUtil = new CounterUtil();

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login([FromBody] CounterLoginModel dto)
        {
            try
            {
                counterUtil.counter_id = dto.counter_id;
                CounterModel counter = counterUtil.GetCounterByID();

                if (counter != null)
                {
                    var roles = new string[] { Roles.Counter };
                    var jwtSecurityToken = JWTAuthentication.GenerateJwtToken(counter.name, roles.ToList());
                    var validUserName = JWTAuthentication.ValidateToken(jwtSecurityToken);
                    return Ok(new { token = jwtSecurityToken, counter_id = counter.counter_id });
                }

                return BadRequest("Counter not found");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Counters()
        {
            CounterUtil counter = new CounterUtil();
            return Ok(new { counters = counter.Counters() });
        }

        [RoleAuthorize(Roles.Admin)]
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
