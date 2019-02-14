using System;
using System.Collections.Generic;

namespace BlogWebsite.Models
{
    public partial class FileTag
    {
        public string FId { get; set; }
        public int TId { get; set; }

        public Files F { get; set; }
        public Tags T { get; set; }
    }
}
