using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QueuKiosk.Controllers
{
    public class KioskController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddQueue(string name)
        {
            return Json(new { success = true, message = "Queue number generated!" });
        }
    }
}