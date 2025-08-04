using Newtonsoft.Json;
using QueueAPI.Helpers;
using QueueAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QueueAPI.Utils
{
    public class AccountUtil: DBConnection
    {
        #region properties
        public string username;
        public string password;
        #endregion

        DBConnection db = new DBConnection();
        public AccountModel Login()
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@username", username),
                new SqlParameter("@password", password)
            };

            DataTable dt = db.Read("sp_login_validate", parameters);

            string jsonParam = JsonConvert.SerializeObject(dt);
            var jsonResult = JsonConvert.DeserializeObject<List<AccountModel>>(jsonParam);

            return jsonResult.FirstOrDefault();
        }

        public AccountModel AccountGet()
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@username", username)
            };

            DataTable dt = db.Read("sp_account_get", parameters);

            string jsonParam = JsonConvert.SerializeObject(dt);
            var jsonResult = JsonConvert.DeserializeObject<List<AccountModel>>(jsonParam);

            return jsonResult.FirstOrDefault();
        }
    }
}