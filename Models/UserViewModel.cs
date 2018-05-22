using System;
using System.ComponentModel.DataAnnotations;

namespace road_runner.Models
{
    public class Register : BaseEntity{

        [Required]
        [MinLength(2, ErrorMessage="Your first name must contain at least 2 characters.")]
        [Display(Name="first name")]
        public string first_name { get;set; }

        [Required]
        [MinLength(2, ErrorMessage="Your last name must contain at least 2 characters.")]
        [Display(Name="last name")]
        public string last_name { get;set; }

        [Required]
        [MinLength(2, ErrorMessage="Your username name must contain at least 2 characters.")]
        [RegularExpression("^[a-zA-Z]+$")]
        [Display(Name="username")]
        public string username { get;set; }

        [Required]
        [EmailAddress]
        [Display(Name="email address")]
        public string email { get;set; }

        [Required]
        [MinLength(8, ErrorMessage="Your password must contain at least 8 characters.")]
        [Compare("cw_password", ErrorMessage="Passwords don't match.")]
        [DataType(DataType.Password)]
        [Display(Name="password")]
        public string password { get;set; }

        [Required]
        [Compare("password", ErrorMessage="Passwords don't match.")]
        [DataType(DataType.Password)]
        [Display(Name="confirm password")]
        public string cw_password { get;set; }


    }

        public class Login : BaseEntity{

        [Required]
        [EmailAddress]
        
        public string email { get;set; }

        [Required]
        [MinLength(8, ErrorMessage="Your password must contain at least 8 characters.")]
        
        [DataType(DataType.Password)]
        
        public string password { get;set; }
    }

    public class UserViewModel : BaseEntity{

        public Register Reg { get;set; }
        public Login Log { get;set; }
    }
    
}