using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebsite.Models
{
    public class stateUser
    {
        private string ID;
        private string Name;
        private string Img;

        public stateUser(string ID, string Name,string Img)
        {
            this.ID = ID;
            this.Name = Name;
            this.Img = Img;
        }

        public (string, string, string) getAllDate()
        {
            return (ID, Name, Img);
        }
    }
}
