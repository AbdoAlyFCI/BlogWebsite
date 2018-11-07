using System;
using System.Collections.Generic;

namespace BlogWebsite.Models
{
    public partial class FileReact
    {
        public int FId { get; set; }
        public string UId { get; set; }
        public int RId { get; set; }

        public Files F { get; set; }
        public React R { get; set; }
        public Users U { get; set; }
    }
}
