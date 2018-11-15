using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebsite.Models.LocalRepo
{
    public class userSingleton
    {
        public static userSingleton _instance=new userSingleton();

        public localUser user { get; set; } = new localUser();
        
        private userSingleton() { }




    }
}