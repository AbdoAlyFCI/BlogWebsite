using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebsite.Models.ClassDiagram
{
    public class ModelDirectory
    {
        public string ID { get; set; }
        public string Name { get; set; }
        private int Depth;
        List<ModelThread> threads = new List<ModelThread>();

        public ModelDirectory(string ID,string Name,int depth)
        {
            this.ID = ID;
            this.Name = Name;
            this.Depth = depth;
        }

        public void addThread(ModelThread thread)
        {
            if (threads.FirstOrDefault(t => t.ID.Equals(thread.ID)) == null){
                threads.Add(thread);
            }
        }

        public void removeThread(string ID)
        {
           
        }

        public ModelThread GetThread (string ID)
        {
            return threads.FirstOrDefault(t => t.ID.Equals(ID));
        }

        public List<ModelThread> getallThread()
        {
            return threads;
        }

        public int threadNums()
        {
            return threads.Count;
        }
    }
}
