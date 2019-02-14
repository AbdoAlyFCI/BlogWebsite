using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using BlogWebsite.Models.ClassDiagram;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Html;
using BlogWebsite.Infrastructure;
namespace BlogWebsite.ViewComponents
{
    public class ShowThreadViewComponent: ViewComponent
    {
        private InfiniteBlogDBContext _dbContext;
        private IMemoryCache cache;
        public ShowThreadViewComponent(InfiniteBlogDBContext dBContext, IMemoryCache cache)
        {
            _dbContext = dBContext;
            this.cache = cache;
        }

        public IViewComponentResult Invoke()
        {
            string channelID=RouteData.Values["Cid"] as string;
            string directoryID=RouteData.Values["Did"] as string;
            string threadID = RouteData.Values["Tid"] as string;

            ModelChannel channel;
            if(!cache.TryGetValue(channelID,out channel))     //Check if channel in cache or not
            {
                return new HtmlContentViewComponentResult(new HtmlString("Wrong Channel"));    //not in cache
            }
            else
            {
                ModelDirectory directory = channel.GetDirectory(directoryID);                   //check if the directory is vaild or not
                if (directory == null)
                {
                    return new HtmlContentViewComponentResult(new HtmlString("Wrong directory"));    //not vaild
                }
                else
                {
                    ModelThread thread = directory.GetThread(threadID);     //check if there is thread that have the requsted id 
                    if (thread == null)
                    {
                        return new HtmlContentViewComponentResult(new HtmlString("Wrong Path"));  //not Available
                    }
                    else
                    {
                        var Comments = _dbContext.Comment.Where(c => c.FileID.Equals(threadID)).ToList();
                        foreach (var comment in Comments)
                        {
                            var user = _dbContext.Users.FirstOrDefault(u => u.UId.Equals(comment.CUserId)).UFirstName;
                            var useImg =ImageConverter.ConvertToString(_dbContext.UsersImg.FirstOrDefault(u => u.UId.Equals(comment.CUserId)).UImg);

                            thread.AddComment(new ModelComment
                            {
                                CommentId=comment.CId,
                                Comment = comment.CommentText,
                                userName = user,
                                userImg= useImg
                            });
                        }
                        return View(thread);      //return thread
                    }
                }
            }

        }
    }
}
