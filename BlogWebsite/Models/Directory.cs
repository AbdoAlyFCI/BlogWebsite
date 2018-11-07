using System;
using System.Collections.Generic;

namespace BlogWebsite.Models
{
    public partial class Directory
    {
        public int DId { get; set; }
        public int? DParentId { get; set; }
        public string DName { get; set; }
        public int DDepth { get; set; }
        public string DOwnerId { get; set; }
        public int DType { get; set; }

        public Users DOwner { get; set; }
        public FileType DTypeNavigation { get; set; }
    }
}
