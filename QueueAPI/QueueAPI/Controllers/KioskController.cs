using QueueAPI.Models.DTO;
using QueueAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

namespace QueueAPI.Controllers
{
    [RoutePrefix("api/kiosk")]
    public class KioskController : ApiController
    {
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get()
        {
            return Ok();
        }
    }
}