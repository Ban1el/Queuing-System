using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using QueueAPI.Helpers;
using QueueAPI.Models.DTO;

namespace QueueAPI.Utils
{
    public class CounterUtil
    {
        DBConnection db = new DBConnection();
        public List<CounterModel> Counters()
        {
            DataTable dt = db.Read("sp_counters_get");

            string jsonParam = JsonConvert.SerializeObject(dt);
            var jsonResult = JsonConvert.DeserializeObject<List<CounterModel>>(jsonParam);

            return jsonResult;
        }
    }
}