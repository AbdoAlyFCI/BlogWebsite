using BlogWebsite.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebsite.Models.ClassDiagram
{
    public class ModelUser
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string birthDate { get; set; }
        public string ChannelID { get; set; }
        public string img;
        public List<string> followedChannel { get; set; }
        private List<ModelThread> threads = new List<ModelThread>();

        public ModelUser() { }
        public ModelUser(String ID,String Email,string firstName,string lastName,string birthDate)
        {
            this.ID = ID;
            this.Email = Email;
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDate = birthDate;
            //this.userChannel = userChannel;
        }

        public void ChangeFirstName(string firstName)
        {
            this.firstName = firstName;
        }

        public void ChangeLastName(string lastName)
        {
            this.lastName = lastName;
        }

        //public void RegisterChannel(ModelChannel channel)
        //{
        //    if(userChannel == null)
        //    {
        //        userChannel = channel;
        //    }
        //}

        //public ModelChannel getMyChannel()
        //{
        //    return userChannel;
        //}

        public void DeleteChannel()
        {

        }

        public void followChannel(/*parameter*/)
        {

        }

        public void unFollowChannel(/*parameter*/)
        {

        }

        public void like()
        {

        }
        public void Dislike()
        {

        }
        
        public List<ModelThread> orderThread()
        {
            //threads = threads.OrderByDescending(t => ).ToList();

            return threads;
        }

        public void addThread(ModelThread thread)
        {
            threads.Add(thread);
        }

        public void changeImg(string img)
        {
            this.img = img;
        }
        public void deleteAccount()
        {

        }
        public passwordModel changePassword()
        {
            return new passwordModel();
        }

    }
}
