using System;
using System.Collections.Generic;

namespace BlogWebsite.Models
{
    public partial class Ustate
    {
        public Ustate()
        {
            RelationShip = new HashSet<RelationShip>();
        }

        public int SId { get; set; }
        public string SText { get; set; }

        public ICollection<RelationShip> RelationShip { get; set; }
    }
}
