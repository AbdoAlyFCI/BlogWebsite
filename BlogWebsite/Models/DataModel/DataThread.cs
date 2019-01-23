using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BlogWebsite.Models.DataModel
{
    public class DataThread
    {
        [Required(ErrorMessage ="Please enter a name to the thread")]
        [Range(1,50,ErrorMessage = "You pass the Max limit for name Title")]
        public string Name { get; set; }

        public string Text { get; set; }
        [Range(1, 220, ErrorMessage = "You pass the Max limit for name Title")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Please select the directory")]
        public string directorId { get; set; }

        [Required(ErrorMessage ="Please select Image for tread")]
        public IFormFile Pic { get; set; }



    }
}
