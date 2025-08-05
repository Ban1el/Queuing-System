using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
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
            CounterListModel counter = null;

            if (result.statusCode == HttpStatusCode.OK)
            {
               counter = JsonConvert.DeserializeObject<CounterListModel>(result.content);
            }
            else
            {
                //redirect to error page
            }

            return View(counter.counters);
        }

        [HttpPost]
        public async Task<JsonResult> Create(string name, string description)
        {
            CounterAddModel counterAddModel = new CounterAddModel();

            if (!string.IsNullOrEmpty(name))
            {
                counterAddModel.name = name;
            }
            else
            {
                return Json(new { success = false, message = "Name required" });
            }

            counterAddModel.description = description;

            if (!string.IsNullOrEmpty(Session["account_id"].ToString()))
            {
                counterAddModel.account_id = Convert.ToInt32(Session["account_id"]);
            }
            else
            {
                return Json(new { success = false, message = "Internal Error" });
            }

            APIResponse result = await ap.AddCounter(counterAddModel);

            if (result.statusCode == HttpStatusCode.OK)
            {
                return Json(new { success = true, message = "Counter added!" });
            }
            else
            {
                return Json(new { success = false, message = "Failed to add counter!" });
            }
        }
    }
}