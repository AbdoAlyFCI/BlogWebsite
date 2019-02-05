using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebsite.Models.ClassDiagram
{
    public class ModelComment
    {
        public string CommentId { get; set; }
        public string userName { get; set; }
        public string Comment { get; set; }
        public string userImg { get; set; }

        List<ModelComment> nestedComment = new List<ModelComment>();

        public void addNestedComment(ModelComment comment)
        {
            nestedComment.Add(comment);
        }
    }
}
