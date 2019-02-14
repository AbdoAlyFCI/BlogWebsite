using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BlogWebsite.Models.DataModel
{
    public class channelRegister
    {
        [Required(ErrorMessage ="Please enter channel identifier")]
        public string ID { get; set; }

        [Required(ErrorMessage ="Please enter channel name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Please enter channel description")]
        public string description { get; set; }

        [Required(ErrorMessage = "Please select cover img for your channel")]
        public IFormFile Cover { get; set; }

        [Required(ErrorMessage = "Please select img for your channel")]
        public IFormFile Pic { get; set; }
    }
}
