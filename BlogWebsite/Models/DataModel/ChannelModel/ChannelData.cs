using System.ComponentModel.DataAnnotations;

namespace BlogWebsite.Models.DataModel.ChannelModel
{
    public class ChannelData
    {
        [Required(ErrorMessage = "Please enter folder Name")]
        public string FolderName { get; set; }

        //public localChannel channel { get; set; }
    }
}
