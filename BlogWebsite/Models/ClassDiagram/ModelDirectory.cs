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
        public int totalNum { get; set; } = 0;
        List<ModelThread> threads = new List<ModelThread>();

        public ModelDirectory(string ID,string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }

        public void addThread(ModelThread thread)
        {
            if (threads.FirstOrDefault(t => t.getPeekData().ID.Equals(thread.getPeekData().ID)) == null){
                threads.Add(thread);
                totalNum++;
            }
        }

        public void removeThread(string ID)
        {
           
        }

        public ModelThread GetThread (string ID)
        {
            return threads.FirstOrDefault(t => t.getPeekData().ID.Equals(ID));
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
