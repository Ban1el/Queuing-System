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
        public async Task<APIResponse> GetKiosk()
        {
            return await apiService.GET("/kiosk/Get");
        }

        public async Task<APIResponse> GetCounters()
        {
            return await apiService.GET("/counter/Get");
        }
    }
}