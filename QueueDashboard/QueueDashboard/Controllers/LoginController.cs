using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using QueueDashboard.Helpers;
using QueueDashboard.Models.DTO;
using System.Net;
using Newtonsoft.Json;

namespace QueueDashboard.Controllers
{
    public class LoginController : Controller
    {
        APIConnection ap = new APIConnection();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {

            }
            
            if (string.IsNullOrEmpty(password))
            {

            }

            APIResponse result = await ap.AccountLogin(new LoginModel { username = username, password = password });

            if (result.statusCode == HttpStatusCode.OK)
            {
                TokenModel tokenModel = JsonConvert.DeserializeObject<TokenModel>(result.content);


                var cookie = new HttpCookie("AuthToken", tokenModel.token)
                {
                    HttpOnly = true,
                    Secure = true, 
                    Expires = DateTime.Now.AddHours(1)
                };

                Response.Headers.Add("Set-Cookie",
                    $"{cookie.Name}={cookie.Value}; expires={cookie.Expires:R}; path=/; HttpOnly; Secure; SameSite=Strict");
            }


            return RedirectToAction("Index", "Home");
        }
    }
}