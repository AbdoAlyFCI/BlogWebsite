using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BlogWebsite.Models.LocalRepo;

namespace BlogWebsite.Models.DataModel.ChannelModel
{
    public class ChannelData
    {
        [Required(ErrorMessage ="Please enter folder Name")]
        public string FolderName { get; set; }

        public localChannel channel { get; set; }
    }
}
