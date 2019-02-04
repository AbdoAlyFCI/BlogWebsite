using BlogWebsite.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebsite.ViewComponents
{
    public class followBlockChannelViewComponent :ViewComponent
    {
        private InfiniteBlogDBContext _dbContext;
        private IMemoryCache cache;
        public followBlockChannelViewComponent(InfiniteBlogDBContext dBContext, IMemoryCache cache)
        {
            _dbContext = dBContext;
            this.cache = cache;
        }

        public IViewComponentResult Invoke(string s)
        {
            List<stateUser> stateUsers = new List<stateUser>();
            string ID = User.Identity.Name;
            int state = 0;
            if (s == "F")
                state = 1;
            else if(s=="B")
                state = 2;

            var channels = _dbContext.RelationShip.Where(c=> c.ActioinUserId.Equals(ID) && c.RStateId == state)
                                                  .Select(c=>new { c.RCid })
                                                  .ToList();
            if (channels.Count == 0)
            {
                if (s == "F") { 
                return new HtmlContentViewComponentResult(new HtmlString("you are not <B>followe</B> any channel yet"));
                }
                else 
                {
                    return new HtmlContentViewComponentResult(new HtmlString("you are not <B>Block</B> any channel yet"));

                }
            }
            else
            {
                foreach (var channel in channels)
                {
                    var temp = _dbContext.Channel.FirstOrDefault(c => c.CId.Equals(channel.RCid));
                    stateUsers.Add(new stateUser(temp.CId, temp.CName, Infrastructure.ImageConverter.ConvertToString(temp.CSIMG)));

                }

                return View(stateUsers);
            }
        }
    }
}
