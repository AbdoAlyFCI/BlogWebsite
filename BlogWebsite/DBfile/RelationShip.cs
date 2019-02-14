using System;
using System.Collections.Generic;

namespace BlogWebsite.Models
{
    public partial class RelationShip
    {
        public string RUid { get; set; }
        public string RCid { get; set; }
        public int RStateId { get; set; }
        public string ActioinUserId { get; set; }

        public Channel RC { get; set; }
        public Ustate RState { get; set; }
        public Users RU { get; set; }
    }
}
