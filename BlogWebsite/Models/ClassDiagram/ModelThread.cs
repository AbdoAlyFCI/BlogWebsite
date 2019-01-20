using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BlogWebsite.Models.ClassDiagram
{
    public class ModelThread
    {
        
        public string ID { get; set; }
        //[Required(ErrorMessage ="Please enter a name for the thread")]
        public string Name { get; set; }
        
        public string Texts { get; set; }
        public ModelUser Owner { get; set; }
        public string Description { get; set; }
        public List<ModelUser> Like { get; set; }
        public List<ModelUser> Dislike { get; set; }
        public DateTime? PublishDate { get; set; }
        public string directorname { get; set; }
        public string directorId { get; set; }
        public IFormFile Pic { get; set; }
        public string img { get; set; }

        private List<ModelTag> threadTags = new List<ModelTag>();

        public void addLike(ModelUser user)
        {
            Like.Add(user);
        }

        public void addDislike(ModelUser user)
        {
            Dislike.Add(user);
        }

        public void removeLike(ModelUser user)
        {
            Like.Remove(user);
        }

        public void removeDislike(ModelUser user)
        {
            Dislike.Remove(user);
        }

        public List<ModelUser> getLikeList()
        {
            return Like;
        }

        public List<ModelUser> getDisLikeList()
        {
            return Dislike;
        }

        public void ChangeText(string Text)
        {
            this.Texts = Text;
        }

        public void ChangeDescription(string Text)
        {
            this.Texts = Text;
        }

        public void addTag(ModelTag tag)
        {
            threadTags.Add(tag);
        }

        public void removeTag(ModelTag tag)
        {
            threadTags.Remove(tag);
        }
    }
}
