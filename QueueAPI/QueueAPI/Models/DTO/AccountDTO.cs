using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QueueAPI.Models.DTO
{
    public class AccountCreateRequestModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mobile Number is required")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Mobile Number must contain only digits")]
        public string MobileNumber { get; set; }
    }

    public class AccountModel
    {
        public int account_id { get; set; }
        public int usertype_id { get; set; }
        public string usertype_name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set;}
        public string email { get; set; }
        public string mobile_number { get; set;}
        public DateTime? date_created { get; set; }
        public DateTime? date_modified { get; set;}
        public bool is_active { get; set; }
        public string password_salt { get; set; }
    }

    public class LoginRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}