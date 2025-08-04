using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using QueueDashboard.Filters;
using QueueDashboard.Helpers;
using QueueDashboard.Models.DTO;

namespace QueueDashboard.Controllers
{
    [AuthorizationFilter]
    public class CounterController : Controller
    {
        APIConnection ap = new APIConnection();
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> List()
        {
            APIResponse result = await ap.GetCounters();
            CounterModelList counter = null;

            if (result.statusCode == HttpStatusCode.OK)
            {
               counter = JsonConvert.DeserializeObject<CounterModelList>(result.content);
            }
            else
            {
                //redirect to error page
            }

            return View(counter.counters);
        }
    }
}