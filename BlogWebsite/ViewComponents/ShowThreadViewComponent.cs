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
                        return View(thread);      //return thread
                    }
                }
            }

        }
    }
}
