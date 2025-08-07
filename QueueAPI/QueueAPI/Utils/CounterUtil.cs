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

        #region properties
        public int account_id { get; set; }
        public int counter_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public DateTime date_created { get; set; }
        public DateTime date_modified { get; set; }
        public bool is_active { get; set; }
        public bool logged_in { get; set; }
        #endregion

        #region constants
        public const string InUse = "In-Use";
        public const string NotInUse = "Not In-Use";
        #endregion

        public int CounterAdd()
        {
            int rows_affected = 0;

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@account_id", account_id),
                new SqlParameter("@name", name),
                new SqlParameter("@description", description),
                new SqlParameter("@status", status),
                new SqlParameter("@date_created", date_created),
                new SqlParameter("@is_active", is_active)
            };

            rows_affected = db.Execute("sp_counter_add", parameters);

            return rows_affected;
        }

        public List<CounterModel> Counters()
        {
            DataTable dt = db.Read("sp_counters_get");

            string jsonParam = JsonConvert.SerializeObject(dt);
            var jsonResult = JsonConvert.DeserializeObject<List<CounterModel>>(jsonParam);

            return jsonResult;
        }

        public CounterModel GetCounterByID()
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@counter_id", counter_id),
            };


            DataTable dt = db.Read("sp_counterById_get", parameters);

            string jsonParam = JsonConvert.SerializeObject(dt);
            var jsonResult = JsonConvert.DeserializeObject<List<CounterModel>>(jsonParam);

            return jsonResult.FirstOrDefault();
        }
    }
}