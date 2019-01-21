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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;

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

        //[Route("Channel")]
        public  IActionResult MyChannel(string id)
        {
            /*1-Check if the Channel in the cache or not 
             *2-if it in cache get it 
             * 3-if not, build it from database
             * 4-if not exist in database retutn wrong id (this channel not avalible)
             */
            //cache.Remove(id);

            SetChannelInAction(id);
            BuildDirectorInAction(id);
            SetThreadInAction(id);
            var channel = cache.Get(id);
            return View("MyChannel", channel);
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

        //[Route("ChannelPannel")]
        public ViewResult ChannelPanel(string id)
        {
            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        
        public async Task<ViewResult> AddThread(ModelThread thread)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            //var dbuser = _dbContext.Users.FirstOrDefault(u => u.UId.Equals(User.Identity.Name));

            Channel channel = _dbContext.Channel
                             .FirstOrDefault(c => c.CId.Equals(thread.ID));


            var director = _dbContext.Entry(channel)
                          .Collection(d => d.Directory).Query()
                          .FirstOrDefault(d => d.DId.Equals(thread.directorId));
            var filesNum = _dbContext.Entry(director)
                          .Collection(d => d.Files).Query().Count();
            Files newFile = new Files{
                FId = "th"+director.DName+ channel.CId + Convert.ToString(filesNum+1),
                FName = thread.Name,
                FCid = channel.CId,
                FDescription = thread.Description,
                FText = thread.Texts,
                FPublishDate = DateTime.Now.Date,
                FPublishState = 1,
            };
            newFile.FileComment = new List<FileComment>();
            newFile.FileReact = new List<FileReact>();
            newFile.FileTag = new List<FileTag>();
            if (thread.Pic != null)
            {
                if (thread.Pic.Length > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = thread.Pic.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        await fs1.CopyToAsync(ms1);
                        p1 = ms1.ToArray();
                    }
                    newFile.FImg = p1;


                }
            }
            //var channelCache =(ModelChannel)cache.Get(channel.CId);
  
            //channelCache.createThread(thread.directorId, thread);
            //var userChace = (ModelUser)cache.Get(User.Identity.Name);
            //userChace.RegisterChannel(channelCache);
            director.Files.Add(newFile);
            await _dbContext.SaveChangesAsync();
            return View("MyChannel");
        }


        //[Route("Thread")]
        public IActionResult Thread(string Cid,string Did,string Tid)
        {
            var channel = (ModelChannel)cache.Get(Cid);

            return View(channel);
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
            channel.Directory.Add(new Models.Directory
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

        private  void SetChannelInAction(string ID)
        {
            cache.GetOrCreate(ID,
                cacheEntry =>
                {
                    var channel = _dbContext.Channel.AsNoTracking().FirstOrDefault(c => c.CId.Equals(ID));
                    ModelChannel modelChannel = new ModelChannel(channel.CId, channel.CName, channel.CDescription, channel.CTotalWatch);
                    modelChannel.img = RetriveImg(channel.CIMG);
                    return modelChannel;
                });

        }
        public void BuildDirectorInAction(string id)
        {
            var channelCache = (ModelChannel)cache.Get(id);
            var directories = _dbContext.Directory.AsNoTracking().Where(d=>d.DOwnerId.Equals(id))
                                      .Select(f => new { f.DId, f.DName, f.DOwner, f.DDepth }).ToList();   //Now not support nested Directory

            foreach (var directory in directories)
            {
                var modelDirectory = new ModelDirectory(directory.DId, directory.DName, directory.DDepth);
                channelCache.createDirectory(modelDirectory);
            }
        }



        public void SetThreadInAction(String id)
        {
            var channelCache =(ModelChannel) cache.Get(id);
            foreach (var director in channelCache.getAllDirectory())
            {
               
                var files = _dbContext.Files.AsNoTracking().Where(s => s.FCid.Equals(director.ID)).ToList();
                foreach (var file in files)
                {
                    string imgscr = null;
                    if (file.FImg != null)
                    {
                        var base64 = Convert.ToBase64String(file.FImg);
                        imgscr = string.Format("data:image/png;base64,{0}", base64);
                    }
                   
                    director.addThread(new ModelThread
                    {
                        ID = file.FId,
                        Name = file.FName,
                        Description = file.FDescription,
                        directorId = director.ID,
                        PublishDate = file.FPublishDate,
                        Texts = file.FText,
                        img= imgscr

                    });
                }
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> ChangeChannelHeaderPic(IFormFile Pic,string ID)
        {
            if (Pic != null)
            {
                if (Pic.Length > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = Pic.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        await fs1.CopyToAsync(ms1);
                        p1 = ms1.ToArray();
                    }
                    var base64 = Convert.ToBase64String(p1);
                    var imgscr = string.Format("data:image/png;base64,{0}", base64);
                    var channel = (ModelChannel)cache.Get(ID);
                    channel.img = imgscr;
                    var tempChange = _dbContext.Channel.SingleOrDefault(c => c.CId.Equals(ID));
                    
                    tempChange.CIMG = p1;
                    await _dbContext.SaveChangesAsync();
                }
            }

            return RedirectToAction("MyChannel", "Channel", ID);
        }

        //private void RetriveImg()
        //{
        //    UsersImg ImgData = _dbContext.UsersImg.FirstOrDefault(s => s.UId == User.Identity.Name);
        //    var base64 = Convert.ToBase64String(ImgData.UImg);
        //    //var imgscr = string.Format("data:image/png;base64,{0}", base64);
        //    //ViewData["img"] = imgscr;
        //}

        private  string RetriveImg(byte[] img)
        {
            //UsersImg ImgData = _dbcontext.UsersImg.FirstOrDefault(s => s.UId == User.Identity.Name);
            if (img == null)
            {
                return "non";
            }
            var base64 = Convert.ToBase64String(img);
            var imgscr = string.Format("data:image/png;base64,{0}", base64);
            return imgscr;
        }
    }



}