using System;
using System.Collections.Generic;

namespace BlogWebsite.Models
{
    public partial class Comment
    {
        public Comment()
        {
            FileComment = new HashSet<FileComment>();
        }

        public int CId { get; set; }
        public int CPid { get; set; }
        public string CUserId { get; set; }
        public int CDepth { get; set; }

        public Users CUser { get; set; }
        public ICollection<FileComment> FileComment { get; set; }
    }
}
