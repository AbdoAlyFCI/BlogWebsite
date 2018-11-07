using System;
using System.Collections.Generic;

namespace BlogWebsite.Models
{
    public partial class React
    {
        public React()
        {
            FileReact = new HashSet<FileReact>();
        }

        public int RId { get; set; }
        public string RName { get; set; }

        public ICollection<FileReact> FileReact { get; set; }
    }
}
