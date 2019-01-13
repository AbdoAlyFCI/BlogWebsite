using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
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
    }
}
