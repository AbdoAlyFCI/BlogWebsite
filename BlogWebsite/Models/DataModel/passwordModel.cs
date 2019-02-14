using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebsite.Models.DataModel
{
    public class passwordModel
    {
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter your password again ")]
        [Compare("Password", ErrorMessage = "Password donot match")]
        public string confirmPassword { get; set; }
    }
}
