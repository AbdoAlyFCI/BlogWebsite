using System;
using System.Collections.Generic;

namespace BlogWebsite.Models
{
    public partial class Users
    {
        public Users()
        {
            Channel = new HashSet<Channel>();
            Comment = new HashSet<Comment>();
            //Directory = new HashSet<Directory>();
            FileReact = new HashSet<FileReact>();
            RelationShip = new HashSet<RelationShip>();
        }

        public string UId { get; set; }
        public string UEmail { get; set; }
        public string UFirstName { get; set; }
        public string ULastName { get; set; }
        public string UPassword { get; set; }
        public DateTime? USignUp { get; set; }
        public DateTime? ULogIn { get; set; }
        public string UBirthDay { get; set; }

        public ICollection<Channel> Channel { get; set; }
        public ICollection<Comment> Comment { get; set; }
        //public ICollection<Directory> Directory { get; set; }
        public ICollection<FileReact> FileReact { get; set; }
        public ICollection<RelationShip> RelationShip { get; set; }
    }
}
