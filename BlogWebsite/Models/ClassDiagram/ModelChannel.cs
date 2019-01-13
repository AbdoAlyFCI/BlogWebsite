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
        private Dictionary<string, ModelDirectory> Directories = new Dictionary<string, ModelDirectory>();


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

        public void createDirectory(string ID, ModelDirectory directory)
        {
            Directories.Add(ID, directory);
        }

        public void removeDirectory(string ID)
        {
            Directories.Remove(ID);
        }

        public ModelDirectory GetDirectory(string ID)
        {
            return Directories[ID];
        }

        public Dictionary<string,ModelDirectory> getAllDirectory()
        {
            return Directories;
        }
        public Thread createThread()
        {
            return new Thread();
        }

        public void createThread(string ID,Thread thread)
        {
            Directories[ID].addThread(thread);
        }


        public void removeThread(string ID,string threadID)
        {
            Directories[ID].removeThread(threadID);
        }

        public void setOwner(ModelUser owner)
        {
            this.Owner = owner;
        }

        public void deleteChannel()
        {

        }

       

    }
}
