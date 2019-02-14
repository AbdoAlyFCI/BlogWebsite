using System;
using System.Collections.Generic;

namespace BlogWebsite.Models
{
    public partial class FileType
    {
        public FileType()
        {
            Directory = new HashSet<Directory>();
        }

        public int TNum { get; set; }
        public string TText { get; set; }

        public ICollection<Directory> Directory { get; set; }
    }
}
