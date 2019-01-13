using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using BlogWebsite.Models.ClassDiagram;

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
            //var data = _dbContext.Users.FirstOrDefault(s => s.UFirstName.Equals("Abdo"));
            
            return View(new Thread());
        }
    }
}
