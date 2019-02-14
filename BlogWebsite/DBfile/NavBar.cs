using System;
using System.Collections.Generic;


namespace BlogWebsite.Models
{
    public class NavBar
    {
        public string NID { get; set; }
        public string NName { get; set; }
        public string NUrl { get; set; }
        public string NCID { get; set; }

        public Channel NC { get; set; }
    }
}
