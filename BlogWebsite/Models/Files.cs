using System;
using System.Collections.Generic;

namespace BlogWebsite.Models
{
    public partial class Files
    {
        public Files()
        {
            FileComment = new HashSet<FileComment>();
            FileReact = new HashSet<FileReact>();
            FileTag = new HashSet<FileTag>();
        }

        public int FId { get; set; }
        public string FName { get; set; }
        public string FCid { get; set; }
        public string FDescription { get; set; }
        public string FText { get; set; }
        public byte[] FImg { get; set; }
        public int? FView { get; set; }
        public DateTime? FPublishDate { get; set; }
        public int? FPublishState { get; set; }

        public Directory FC { get; set; }
        public ICollection<FileComment> FileComment { get; set; }
        public ICollection<FileReact> FileReact { get; set; }
        public ICollection<FileTag> FileTag { get; set; }
    }
}
