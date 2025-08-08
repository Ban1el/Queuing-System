using Newtonsoft.Json;
using QueueDashboard.Filters;
using QueueDashboard.Helpers;
using QueueDashboard.Models.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QueueDashboard.Controllers
{
    public class QueueController : Controller
    {
        APIConnection ap = new APIConnection();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> GenerateQueueNumber(string name)
        {
            QueueCreateRequestModel queueCreateRequestModel = new QueueCreateRequestModel();
            queueCreateRequestModel.name = name;

            APIResponse result = await ap.GenerateQueueNumber(queueCreateRequestModel);

            if (result.statusCode == HttpStatusCode.OK)
            {
                QueueNumberModel queue = null;
                queue = JsonConvert.DeserializeObject<QueueNumberModel>(result.content);

                return Json(new { success = true, queue_number = queue.queue_number });
            }
            else
            {
                return Json(new { success = false, message = "Failed to add counter!" });
            }
        }
    }
}