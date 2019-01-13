using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebsite.Models.LocalRepo
{
    public class localChannel
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public List<localGenralUser> Followers { get; set; }
        public localUser Owner { get; set; }
        public List<localDirectory> directory { get; set; } = new List<localDirectory>();


    }
}
