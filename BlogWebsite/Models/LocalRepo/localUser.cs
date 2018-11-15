using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebsite.Models.LocalRepo
{
    public class localUser:localGenralUser
    {
        public DateTime? USignUP { get; set; }
        public DateTime? lastLogIn { get; set; }
        public List<int> UserChannel = new List<int>();
        public localChannel myChannel { get; set; }

        
        public void SubscibeToChannel()
        {
            
        }

        public int showMyChannel()
        {
            return 0;
        }


    }
}
