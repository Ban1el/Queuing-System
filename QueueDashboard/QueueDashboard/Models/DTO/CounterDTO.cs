using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueueDashboard.Models.DTO
{
    public class CounterModel
    {
        public int counter_id { get; set; }
        public int account_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public DateTime date_created { get; set; }
        public DateTime? date_modified { get; set; }
        public bool is_active { get; set; }
    }

    public class CounterListModel
    {
        public List<CounterModel> counters { get; set; }
    }

    public class CounterAddModel
    {
        public int account_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}