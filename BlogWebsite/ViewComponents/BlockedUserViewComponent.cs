using BlogWebsite.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebsite.Infrastructure;

namespace BlogWebsite.ViewComponents
{
    public class BlockedUserViewComponent: ViewComponent
    {
        private InfiniteBlogDBContext _dbContext;
        private IMemoryCache cache;
        public BlockedUserViewComponent(InfiniteBlogDBContext dBContext, IMemoryCache cache)
        {
            _dbContext = dBContext;
            this.cache = cache;
        }

        public IViewComponentResult Invoke()
        {
            List<stateUser> stateUsers = new List<stateUser>();
            string ID = RouteData.Values["Cid"] as string;

            var users = _dbContext.RelationShip.Where(s => s.ActioinUserId.Equals(ID) && s.RStateId == 2).Select(u=>new { u.RUid }).ToList();
            if (users.Count == 0)
            {
                return new HtmlContentViewComponentResult(new HtmlString("No blocked user"));   
            }
            else
            {
                foreach (var user in users)
                {
                    string uid = user.RUid;
                    var Name = _dbContext.Users.FirstOrDefault(u => u.UId.Equals(uid));
                    var img = _dbContext.UsersImg.FirstOrDefault(u => u.UId.Equals(uid)).UImg;
                    string simg = ImageConverter.ConvertToString(img);
                    stateUsers.Add(new stateUser(uid, Name.UFirstName+" "+Name.ULastName, simg));
                }

                return View(stateUsers);
            }
        }
    }
}
