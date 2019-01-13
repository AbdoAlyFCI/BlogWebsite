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
using BlogWebsite.Models.DataModel;
using BlogWebsite.Models.DataModel.ChannelModel;
using BlogWebsite.Models.ClassDiagram;

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
            var getUser =(ModelUser)cache.Get(User.Identity.Name);
            if (getUser.getMyChannel() == null)
            {
                return RedirectToAction("RegisterNewChannel");
            }

            //localChannel localchannel = new localChannel
            //{
            //    directory = getUser.myChannel.directory
            //};
             
            return View(getUser.getMyChannel());
        }

        public IActionResult RegisterNewChannel()
        {
            var DetectUser = _dbContext.Channel.SingleOrDefault(s => s.COwnerId.Equals(User.Identity.Name));
            if (DetectUser != null)
            {
                return RedirectToAction("MyChannel","Channel");
            }
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task <IActionResult> RegisterNewChannel(channelRegister channel)
        {
       
            if (!ModelState.IsValid)
            {
                return View();
            }
            channel.ID = channel.ID.Trim();
            channel.name = channel.name.Trim();
            channel.description = channel.description.Trim();

            var newChannel = _dbContext.Channel.SingleOrDefault(s => s.CId.Equals(channel.ID));
            if(newChannel != null)
            {
                throw new Exception("dgfg");
            }

            var user = _dbContext.Users
                .SingleOrDefault(s => s.UId.Equals(User.Identity.Name));
            user.Channel.Add(new Channel
            {
                CId = channel.ID,
                CDescription = channel.description,
                CName = channel.name,
                CFollowers = 0,
                CTotalWatch = 0,
                RelationShip = new List<RelationShip>(),
                //Files = new List<Files>(),
            });
            await _dbContext.SaveChangesAsync();

            return View("MyChannel");
        }

        public ViewResult ChannelPanel()
        {
            return View();
        }

        public ViewResult NewSubject()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ViewResult AddThread(Thread thread)
        {
            var df = ViewData["text"];
            return View();
        }

        public IActionResult Thread()
        {
            var getUser = (ModelUser)cache.Get(User.Identity.Name);
            if (getUser.getMyChannel() == null)
            {
                return RedirectToAction("RegisterNewChannel");
            }

            //localChannel localchannel = new localChannel
            //{
            //    directory = getUser.myChannel.directory
            //};

            return View(getUser.getMyChannel());
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ViewResult> AddFolder (ChannelData Folder)
        {
            if (!ModelState.IsValid)
            {
                return View("MyChannel");
            }
            var getUser = (localUser)cache.Get(User.Identity.Name);
            var channellist = _dbContext.Directory.Where(s => s.DOwnerId == getUser.myChannel.ID).ToList().Count()+1;
            var newID = getUser.myChannel.ID + "D" + Convert.ToString(channellist);

            var channel = _dbContext.Channel.SingleOrDefault(s => s.COwnerId .Equals( getUser.ID));
            channel.Directory.Add(new Directory
            {
                DId = newID,
                DDepth = 0,
                DName = Folder.FolderName,
                DParentId = newID,
                DType = 1,
                //DTypeNavigation = new FileType()
            });

            await _dbContext.SaveChangesAsync();

            return View("MyChannel");
        }
    }



}