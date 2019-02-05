using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebsite.Models.ClassDiagram
{
    public class ModelChannel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? totalView { get; set; }
        public int Followers { get; set; } = 0;
        public string ownerID { get; set; }

        private List<ModelDirectory> Directories = new List<ModelDirectory>();
        private List<ModelNavBar> navBars = new List<ModelNavBar>();
        public string img { get; set; }
        public string simg { get; set; }


        public ModelChannel(string ID,string Name,string Description,int? totalView)
        {
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
            this.totalView = totalView;
        }

        public void addFollower()
        {
            Followers++;
        }

        public void removeFollower()
        {
            Followers--;
        }
        public void createDirectory(ModelDirectory directory)
        {
            if (Directories.FirstOrDefault(d=>d.ID.Equals(directory.ID))==null)
                Directories.Add(directory);
        }


        public ModelDirectory GetDirectory(string ID)
        {
            return Directories.FirstOrDefault(d => d.ID == ID);
        }

        public List<ModelDirectory> getAllDirectory()
        {
            return Directories;
        }


        public void addThread(string ID,ModelThread thread)
        {
            if(Directories.FirstOrDefault(s=>s.ID.Equals(ID))==null)
                Directories.FirstOrDefault(d=>d.ID==ID).addThread(thread);
        }


        public void removeThread(string ID,string threadID)
        {
            Directories.FirstOrDefault(d => d.ID == ID).removeThread(threadID);
        }

        public List<ModelThread> getAllThread()
        {
            List<ModelThread> threads = new List<ModelThread>();
            foreach (var Directorty in Directories)
            {

                foreach (ModelThread file in Directorty.getallThread())
                {
                    threads.Add(file);
                }

            }
            threads = threads.OrderBy(t => t.getPeekData().PublishDate).ToList();

            return threads;
        }

        public List<ModelThread> getSpecificDirectoryThreads(string Did)
        {
            return  Directories.FirstOrDefault(d => d.ID.Equals(Did)).getallThread();
        }

        public int ThreadsNum()
        {
            int files = 0;
            foreach (var item in Directories)
            {
                files += item.threadNums();
            }
            return files;
        }

        public int FollowersNum()
        {
            return Followers;
        }

        public int DirectoryNum()
        {
            return Directories.Count;
        }

        public List<ModelThread> getDraftThreads()
        {
            List<ModelThread> threads = new List<ModelThread>();

            foreach (var Directorty in Directories)
            {

                foreach (ModelThread file in Directorty.getallThread().Where(t=>t.Draft==true))
                {
                    threads.Add(file);
                }

            }
            threads = threads.OrderBy(t => t.getPeekData().PublishDate).ToList();


            return threads;
        }

        public List<ModelNavBar> getNavBar()
        {
            return navBars;
        }

        public ModelNavBar getNavObject()
        {
            return new ModelNavBar();
        }

        public void AddNavItem(ModelNavBar navBar)
        {
            navBars.Add(navBar);
        }

        public void ChangeNavItem(ModelNavBar navBar, String NID)
        {
            navBars.FirstOrDefault(n => n.NID.Equals(NID)).Name = navBar.Name;

            navBars.FirstOrDefault(n => n.NID.Equals(NID)).Url = navBar.Url;
        }
    }
}
