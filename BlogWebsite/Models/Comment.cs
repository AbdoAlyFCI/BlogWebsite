using System;
using System.Collections.Generic;

namespace BlogWebsite.Models
{
    public partial class Comment
    {
        public Comment()
        {
            //FileComment = new HashSet<FileComment>();
        }

        public string CId { get; set; }
        public string CPid { get; set; }
        public string CUserId { get; set; }
        public int CDepth { get; set; }
        public string CommentText { get; set; }
        public string FileID { get; set; }
        public Users CUser { get; set; }
        public Files file { get; set; }
        public DateTime Date { get; set; }
        //public ICollection<FileComment> FileComment { get; set; }
    }
}
