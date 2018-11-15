using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogWebsite.Models;
using BlogWebsite.Models.DataModel.HomeModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using BlogWebsite.Models.LocalRepo;
using Microsoft.Extensions.Caching.Memory;

namespace BlogWebsite.Controllers
{
    [Authorize]
    public class HomeController:Controller
    {
        public InfiniteBlogDBContext _dbcontext;
        private IMemoryCache cache;

        public HomeController(InfiniteBlogDBContext dBContext,IMemoryCache cache)
        {
            _dbcontext = dBContext;
            this.cache = cache;
        } 


        public ViewResult MyFeed()
        {
            
            return View(cache.Get(User.Identity.Name));
        }

        public ViewResult Channel()
        {
            return View();
        }

        public ViewResult RegisterNewChannel()
        {
            return View();
        }

        public ViewResult AccountSetting()
        {
            return View();
        }
    }
}
