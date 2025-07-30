using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace QueueDashboard.Models.DTO
{
    public class APIResponse
    {
        public HttpStatusCode statusCode { get; set; }
        public string content { get; set; }
    }
}