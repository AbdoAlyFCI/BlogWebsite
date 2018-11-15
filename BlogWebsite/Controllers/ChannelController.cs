using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebsite.Models;
using BlogWebsite.Models.LocalRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
namespace BlogWebsite.Controllers
{
    public class ChannelController : Controller
    {
        private InfiniteBlogDBContext _dbContext;
        private IMemoryCache cache;

        public ChannelController(InfiniteBlogDBContext dBContext, IMemoryCache cache)
        {
            _dbContext = dBContext;
            this.cache = cache;
        }


        public IActionResult MyChannel()
        {
            var getUser =(localUser)cache.Get(User.Identity.Name);
            if (getUser.myChannel == null)
            {
                return RedirectToAction("RegisterNewChannel");
            }
            
            return View();
        }

        public ViewResult RegisterNewChannel()
        {
            return View();
        }
    }
}