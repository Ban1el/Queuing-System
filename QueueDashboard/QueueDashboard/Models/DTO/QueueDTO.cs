using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QueueDashboard.Models.DTO
{

    public class QueueCreateRequestModel
    {
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        [Required(ErrorMessage = "Please enter name.")]
        [RegularExpression(@"\S+", ErrorMessage = "Name cannot be empty or whitespace.")]
        public string name { get; set; }
    }

    public class QueueNumberModel
    {
        public string queue_number { get; set; }
    }
}