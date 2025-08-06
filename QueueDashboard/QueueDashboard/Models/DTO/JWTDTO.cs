using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueueDashboard.Models.DTO
{
    public class AuthenticatedUserModel
    {
        public string Username { get; set; }
        public List<string> Roles { get; set; }
    }
}