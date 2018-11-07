using System;
using System.Collections.Generic;

namespace BlogWebsite.Models
{
    public partial class FileComment
    {
        public int FId { get; set; }
        public int CId { get; set; }

        public Comment C { get; set; }
        public Files F { get; set; }
    }
}
