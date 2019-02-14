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
        private ModelPeekThread PeekThread=new ModelPeekThread();
        public string Texts { get; set; }
        //private List<ModelTag> threadTags = new List<ModelTag>();
        public bool Draft { get; set; } = false;


        private List<ModelComment> comments = new List<ModelComment>();

        public ModelThread (ModelPeekThread peekThread,string text)
        {
            this.PeekThread = peekThread;
            this.Texts = text;
        }


        public void AddComment(ModelComment comment)
        {
            if (comments.FirstOrDefault(c => c.CommentId.Equals(comment.CommentId)) == null)
            {
                comments.Add(comment);
            }

        }


        public List<ModelComment> GetComments()
        {
            return comments;
        }

    

        public void ChangeText(string Text)
        {
            this.Texts = Text;
        }

        public void ChangeDescription(string Text)
        {
            this.Texts = Text;
        }


        public ModelPeekThread getPeekData()
        {
            return PeekThread;
        }

        public void setPeekData(ModelPeekThread peekThread)
        {
            this.PeekThread = peekThread;
        }

        /*Delay*/
        //public void addTag(ModelTag tag)
        //{
        //    threadTags.Add(tag);
        //}

        //public void removeTag(ModelTag tag)
        //{
        //    threadTags.Remove(tag);
        //}
    }
}
