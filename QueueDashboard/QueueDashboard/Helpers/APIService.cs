using Newtonsoft.Json;
using QueueDashboard.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace QueueDashboard.Helpers
{
    public class APIService
    {
        string APIurl = ConfigurationManager.AppSettings["APIURL"];
        string token = HttpContext.Current.Request["AuthToken"];
        public async Task<APIResponse> GET(string url)
        {
            APIResponse result = new APIResponse();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await client.GetAsync(APIurl + url);

                result.statusCode = response.StatusCode;
                result.content = await response.Content.ReadAsStringAsync();
            }
            return result;
        }

        public async Task<APIResponse> POST(string url, object data)
        {
            APIResponse result = new APIResponse();
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await client.PostAsync(APIurl + url, content);

                result.statusCode = response.StatusCode;
                result.content = await response.Content.ReadAsStringAsync();
            }
            return result;
        }
    }
}