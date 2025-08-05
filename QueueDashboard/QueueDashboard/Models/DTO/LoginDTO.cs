using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QueueDashboard.Models.DTO
{
    public class LoginModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class TokenModel
    {
        public string token { get; set; }
        public int account_id { get; set; }
    }
}