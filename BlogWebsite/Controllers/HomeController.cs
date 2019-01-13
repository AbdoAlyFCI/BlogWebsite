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
using Microsoft.AspNetCore.Http;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Hosting;

namespace BlogWebsite.Controllers
{
    [Authorize]
    public class HomeController:Controller
    {
        public InfiniteBlogDBContext _dbcontext;
        private IMemoryCache cache;
        private readonly IHostingEnvironment environment;
        public HomeController(InfiniteBlogDBContext dBContext,IMemoryCache cache,IHostingEnvironment environment)
        {
            _dbcontext = dBContext;
            this.cache = cache;
            this.environment = environment;
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
        [HttpPost]
        public async Task<ViewResult> AccountSetting(IFormFile Pic)
        {
            if(Pic != null)
            {
                if (Pic.Length > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = Pic.OpenReadStream()) 
                    using(var ms1=new MemoryStream())
                    {
                        await fs1.CopyToAsync(ms1);
                        p1 = ms1.ToArray();
                    }
                    var imgData = new UsersImg()
                    {
                        UId = User.Identity.Name,
                        UImg = p1
                    };

                    await _dbcontext.AddAsync(imgData);
                    await _dbcontext.SaveChangesAsync();
                }
            }




            return View();
        }


        private void RetriveImg()
        {
            UsersImg ImgData = _dbcontext.UsersImg.FirstOrDefault(s => s.UId == User.Identity.Name);
            var base64 = Convert.ToBase64String(ImgData.UImg);
            var imgscr = string.Format("data:image/png;base64,{0}", base64); 
            ViewData["img"] = imgscr;
        }

     
    }
}
