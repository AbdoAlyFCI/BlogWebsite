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
        public ModelUser Owner { get; set; }
        public HashSet<ModelUser> Followers { get; set; }
        public HashSet<ModelUser> blockedUser { get; set; }
        private List<ModelDirectory> Directories = new List<ModelDirectory>();
        public string img { get; set; }


        public ModelChannel(string ID,string Name,string Description,int? totalView)
        {
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
            this.totalView = totalView;

            //this.Followers = Followers;
            //this.blockedUser = blockedUser;
        }

        public void addFollower(ModelUser user)
        {
            Followers.Add(user);
        }

        public void removeFollower(ModelUser user)
        {
            Followers.Remove(user);
        }

        public void addblockUser(ModelUser user)
        {
            blockedUser.Add(user);
        }

        public void removeBlockUser(ModelUser user)
        {
            blockedUser.Remove(user);
        }

        public void createDirectory(ModelDirectory directory)
        {
            if (Directories.FirstOrDefault(d=>d.ID.Equals(directory.ID))==null)
                Directories.Add(directory);
        }

        //public void removeDirectory( ID)
        //{
        //    //Directories.Where(d=>d.Name==ID).Remove();
        //}

        public ModelDirectory GetDirectory(string ID)
        {
            return Directories.FirstOrDefault(d => d.ID == ID);
        }

        public List<ModelDirectory> getAllDirectory()
        {
            return Directories;
        }
        public ModelThread createThread()
        {
            return new ModelThread();
        }

        public void createThread(string ID,ModelThread thread)
        {
            if(Directories.FirstOrDefault(s=>s.ID.Equals(ID))==null)
                Directories.FirstOrDefault(d=>d.ID==ID).addThread(thread);
        }


        public void removeThread(string ID,string threadID)
        {
            Directories.FirstOrDefault(d => d.ID == ID).removeThread(threadID);
        }

        public void setOwner(ModelUser owner)
        {
            this.Owner = owner;
        }

        public void deleteChannel()
        {

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
            threads = threads.OrderBy(t => t.PublishDate).ToList();

            return threads;
        }

       

    }
}
