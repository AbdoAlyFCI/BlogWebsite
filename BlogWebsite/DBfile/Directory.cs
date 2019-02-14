using System;
using System.Collections.Generic;

namespace BlogWebsite.Models
{
    public partial class Directory
    {
        public Directory()
        {
            Files = new HashSet<Files>();
        }
        public string DId { get; set; }
        public string DParentId { get; set; }
        public string DName { get; set; }
        public int DDepth { get; set; }
        public string DOwnerId { get; set; }
        public int DType { get; set; }

        public Channel DOwner { get; set; }
        public FileType DTypeNavigation { get; set; }
        public ICollection<Files> Files { get; set; }

    }
}
