using System;
using System.Collections.Generic;

namespace BlogWebsite.Models
{
    public partial class Tags
    {
        public Tags()
        {
            FileTag = new HashSet<FileTag>();
        }

        public int TId { get; set; }
        public string TText { get; set; }

        public ICollection<FileTag> FileTag { get; set; }
    }
}
