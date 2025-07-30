using QueueAPI.Models;
using QueueAPI.Models.DTO;
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
    [RoutePrefix("api/queue")]
    public class QueueController : ApiController
    {
        [HttpPost]
        [Route("add")]
        public IHttpActionResult Add([FromBody] QueueCreateRequestModel user_details)
        {
            if (ModelState.IsValid)
            {
                using (var context = new QueueDBEntities())
                {
                    var queue_count = context.Queues
                        .Where(x => DbFunctions.TruncateTime(x.date_created) == DateTime.Today)
                        .Count() + 1;

                    Queue queue = new Queue();
                    queue.name = user_details.name;
                    queue.queue_number = queue_count.ToString("D4");
                    queue.status = "Pending";
                    queue.date_created = DateTime.Now;

                    context.Queues.Add(queue); 
                    context.SaveChanges(); 
                }

                return Ok();
            }
            else
            {
                return BadRequest();
            }   
        }
    }
}
