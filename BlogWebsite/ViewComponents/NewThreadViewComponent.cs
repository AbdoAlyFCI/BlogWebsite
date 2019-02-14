using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using BlogWebsite.Models.ClassDiagram;
using BlogWebsite.Models.DataModel.RegisterThreadModel;

namespace BlogWebsite.ViewComponents
{
    public class NewThreadViewComponent:ViewComponent
    {
        private InfiniteBlogDBContext _dbContext;
        private IMemoryCache cache;
        public NewThreadViewComponent(InfiniteBlogDBContext dBContext, IMemoryCache cache)
        {
            _dbContext = dBContext;
            this.cache = cache;
        }

        public IViewComponentResult Invoke()
        {
            string Cid = RouteData.Values["Cid"].ToString();
            ViewBag.Cid = Cid;
            var channelDirectory = _dbContext.Directory.Where(d => d.DOwnerId.Equals(Cid))
                                   .Select(d=>new { d.DName,d.DId}).ToList();
            RegisterThread registerThread = new RegisterThread();
            foreach (var item in channelDirectory)
            {
                registerThread.AvailbleDirectory.Add(item.DId, item.DName);
            }
            
            return View(registerThread);
        }
    }
}
