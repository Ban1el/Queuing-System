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

        #region Account
        public async Task<APIResponse> AccountLogin(LoginModel dto)
        {
            return await apiService.POST("Account/Login", dto);
        }
        #endregion

        #region Counter
        public async Task<APIResponse> CounterLogin(CounterLoginModel dto)
        {
            return await apiService.POST("Counter/Login", dto);
        }
        public async Task<APIResponse> GetCounters()
        {
            return await apiService.GET("/counter/Get");
        }

        public async Task<APIResponse> AddCounter(CounterAddModel dto)
        {
            return await apiService.POST("/counter/Add", dto);
        }
        #endregion
    }
}