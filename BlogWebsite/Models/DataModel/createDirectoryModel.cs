using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebsite.Models.DataModel
{
    public class createDirectoryModel
    {
        [Required(ErrorMessage ="Enter the Directory name")]
        public string DirectoryName { get; set; }
    }
}
