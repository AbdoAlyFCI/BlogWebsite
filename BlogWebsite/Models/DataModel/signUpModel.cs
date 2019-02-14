using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace BlogWebsite.Models
{
    public class signUpModel
    {
        [Required(ErrorMessage = "Please enter your firstname")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        public string lastName { get; set; }

        [Required(ErrorMessage ="Please enter your Email ")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email{get;set;}

        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter your password again ")]
        [Compare("Password", ErrorMessage= "Password donot match")]
        public string confirmPassword { get; set; }

        //[Range(typeof(bool),"true","true",ErrorMessage = "You must accept the terms")]
        //public bool termsAccepted { get; set; }

        [Required(ErrorMessage = "Please  enter your Birth day")]
        public string birthDay { get; set; }

        [Required(ErrorMessage = "Please  enter your Birth month")]
        public string birthMonth { get; set; }

        [Required(ErrorMessage = "Please  enter your Birth year")]
        public string birthYear { get; set; }

        [Required(ErrorMessage ="Please select img for your profile")]
        public IFormFile Pic { get; set; }
    }
}
