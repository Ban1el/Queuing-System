using QueueDashboard.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace QueueDashboard.Helpers
{
    public class APIConnection
    {
        APIService apiService = new APIService();

        public async Task<APIResponse> AccountLogin(LoginModel dto)
        {
            return await apiService.POST("Account/Login", dto);
        }

        // GET: APIConnection
        public async Task<string> GetKiosk()
        {
            return await apiService.GET("/kiosk/get");
        }
    }
}