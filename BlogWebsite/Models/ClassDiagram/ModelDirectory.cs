using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebsite.Models.ClassDiagram
{
    public class ModelDirectory
    {
        private string ID;
        public string Name { get; set; }
        private int Depth;
        List<Thread> threads { get; set; }

        public ModelDirectory(string ID,string Name,int depth)
        {
            this.ID = ID;
            this.Name = Name;
            this.Depth = depth;
        }

        public void addThread(Thread thread)
        {
            threads.Add(thread);
        }

        public void removeThread(string ID)
        {
           
        }

        public Thread GetThread (string ID)
        {
            return new Thread();
        }
    }
}
