using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;
using BlogWebsite.Models.DataModel;
using BlogWebsite.Models.ClassDiagram;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;
using BlogWebsite.Models.DataModel.RegisterThreadModel;
using BlogWebsite.Infrastructure;

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



        /* 1-MyChannel
         * 2-RegisterNewChannel (Get)
         * 3-RegisterNewChannel (Post)
         * 4-ChannelPanel
         * 5-Thread
         * 6-AddThread (Post)
         * 7-AddDirectory (Post)
         * 8-ChangeChannelHeaderPic (Post)
         * 8.1-ChangeChannelPic (Post)
         * 9-AddFollower (Post)
         * 10-GetSpecificDirectory
         * ***/



        /*1- Register New Channel (Get)*/
        [HttpGet,Authorize]
        public IActionResult RegisterNewChannel()
        {
            
            var userImg = _dbContext.UsersImg.FirstOrDefault(u => u.UId.Equals(User.Identity.Name));
            if (userImg != null)
            {
                ViewBag.img = Infrastructure.ImageConverter.ConvertToString(userImg.UImg);

            }
            /*1- Get user data from DB 
             *2- check if the user have channel 
             *3- if have one redirect to it
             *4- if not continue to register page
             */
            var dbtUser = _dbContext.Users.FirstOrDefault(u => u.UId.Equals(User.Identity.Name));
            var channel = _dbContext.Entry(dbtUser).Collection(u => u.Channel).Query().FirstOrDefault(u=>u.COwnerId.Equals(User.Identity.Name));
            
            //var jsonUser = HttpContext.Session.GetString(User.Identity.Name);
            //var user = JsonConvert.DeserializeObject<ModelUser>(jsonUser);


            if (channel != null)
            {
                return Redirect("/Channel/MyChannel/" + channel.CId);
                //return RedirectToAction("MyChannel", "Channel",user.ChannelID);
            }

            return View();
        }

        /*2- Register New Channel (Post)*/
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterNewChannel(channelRegister channel)
        {
            /*1- check the valid of the data that sent
             *2- build channel object
             * 3-save to DB
             */
            if (!ModelState.IsValid)
            {
                return View();
            }

            channel.ID = channel.ID.Trim();
            channel.name = channel.name.Trim();
            channel.description = channel.description.Trim();

            var newChannel = _dbContext.Channel.SingleOrDefault(s => s.CId.Equals(channel.ID));
            if (newChannel != null)
            {
                //To Change 
                return View();
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
                CIMG = ImageConverter.convertToByte(channel.Cover),
                CSIMG = ImageConverter.convertToByte(channel.Pic)
            });
            for (int i = 0; i < 4; i++)
            {
                await _dbContext.NavBar.AddAsync(new NavBar
                {
                    NID=string.Format($"N{channel.ID}{i}"),
                    NCID=channel.ID
                });  
            }

            await _dbContext.SaveChangesAsync();
            //var jsonUser = HttpContext.Session.GetString(User.Identity.Name);
            //var cuser = JsonConvert.DeserializeObject<ModelUser>(jsonUser);
            //cuser.ChannelID = channel.ID;

            return Redirect(string.Format("/Channel/MyChannel/" + channel.ID));

        }

        /*3-My Channel*/
        public IActionResult MyChannel(string Cid,int page=1)
        {

            /*1- Build channel
             *2- Return to view
            */

            var userImg = _dbContext.UsersImg.FirstOrDefault(u => u.UId.Equals(User.Identity.Name));
            if (userImg != null)
            {
                ViewBag.img = Infrastructure.ImageConverter.ConvertToString(userImg.UImg);

            }
            checkFollowing(Cid);
            buildChannelSteps(Cid);
            var channel = cache.Get(Cid);
            ViewBag.pagenum = page;
            return View("MyChannel",channel);
        }


        /*4-Channel Panel*/
        public IActionResult ChannelPanel(string Cid)
        {
            var userImg = _dbContext.UsersImg.FirstOrDefault(u => u.UId.Equals(User.Identity.Name));
            if (userImg != null)
            {
                ViewBag.img = Infrastructure.ImageConverter.ConvertToString(userImg.UImg);

            }
            if (Cid == null)
            {
                return RedirectToAction("MyChannel", "RegisterNewChannel");
            }
            ModelChannel channel;
            if (!cache.TryGetValue(Cid, out channel))
            {
                buildChannelSteps(Cid);
            }
            channel = (ModelChannel)cache.Get(Cid);
            return View(channel);
            
        }

        /*5-Thread*/
        public IActionResult Thread(string Cid, string Did, string Tid)
        {
            var userImg = _dbContext.UsersImg.FirstOrDefault(u => u.UId.Equals(User.Identity.Name));
            if (userImg != null)
            {
                ViewBag.img = Infrastructure.ImageConverter.ConvertToString(userImg.UImg);

            }

            ModelChannel channel;
            if (!cache.TryGetValue(Cid, out channel))
            {
                buildChannelSteps(Cid);
            }

            channel = (ModelChannel)cache.Get(Cid);
            checkFollowing(Cid);
            return View(channel);
        }


        /*6-Add Thread (Post)*/
        [HttpPost,ValidateAntiForgeryToken]        
        public async Task<IActionResult> AddThread(RegisterThread thread,string Cid)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Channel channel = _dbContext.Channel
                             .FirstOrDefault(c => c.CId.Equals(Cid));


            var director = _dbContext.Entry(channel)
                          .Collection(d => d.Directory).Query()
                          .FirstOrDefault(d => d.DId.Equals(thread.DataThread.directorId));

            var filesNum = _dbContext.Entry(director)
                          .Collection(d => d.Files).Query().Count();

            Files newFile = new Files{
                FId = "th"+director.DName+ channel.CId + Convert.ToString(filesNum+1),
                FName = thread.DataThread.Name,
                FCid = channel.CId,
                FDescription = thread.DataThread.Description,
                FText = thread.DataThread.Text,
                FPublishDate = DateTime.Now.Date,
                FPublishState = 1,
            };
            newFile.FileComment = new List<Comment>();
            newFile.FileReact = new List<FileReact>();
            newFile.FileTag = new List<FileTag>();
            if (thread.DataThread.Pic != null)
            {
                if (thread.DataThread.Pic.Length > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = thread.DataThread.Pic.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        await fs1.CopyToAsync(ms1);
                        p1 = ms1.ToArray();
                    }
                    newFile.FImg = p1;


                }
            }

            director.Files.Add(newFile);
            await _dbContext.SaveChangesAsync();

            return Redirect(string.Format($"/Channel/MyChannel/{Cid}"));
        }


        /*7-Add Directory (Post)*/
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFolder (createDirectoryModel Directory,string Cid)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(string.Format($"/Channel/Thread/{Cid}"));
            }
            //var getUser = (localUser)cache.Get(User.Identity.Name);
            var channellist = _dbContext.Directory.Where(s => s.DOwnerId == Cid).ToList().Count()+1;
            var newID = Cid + "D" + Convert.ToString(channellist);

            var channel = _dbContext.Channel.SingleOrDefault(s => s.CId .Equals(Cid));

            channel.Directory.Add(new Models.Directory
            {
                DId = newID,
                DDepth = 0,
                DName = Directory.DirectoryName,
                DParentId = newID,
                DType = 1,
                //DTypeNavigation = new FileType()
            });

            await _dbContext.SaveChangesAsync();

            return Redirect(string.Format($"/Channel/MyChannel/{Cid}"));
        }


        /*8-Change Channel Header Pic (Post)*/
        [HttpPost]
        public async Task<IActionResult> ChangeChannelHeaderPic(IFormFile Pic, string ID)
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

        /*9-Change Channel Pic (Post)*/
        [HttpPost]
        public async Task<IActionResult> ChangeChannelPic(IFormFile Pic, string  Cid   )
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
                    var channel = (ModelChannel)cache.Get(Cid);
                    channel.simg = imgscr;
                    var tempChange = _dbContext.Channel.SingleOrDefault(c => c.CId.Equals(Cid));

                    tempChange.CSIMG = p1;
                    await _dbContext.SaveChangesAsync();
                }
            }

            return Redirect("ChannelPanel?Cid=" + Cid);
            //return RedirectToAction("ChannelPannel", "Channel", Cid);
        }

        /*10-AddFollower (Post)*/
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFollower(string Cid)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UId.Equals(User.Identity.Name));
            var channel = _dbContext.Channel.FirstOrDefault(c => c.CId.Equals(Cid));
            user.RelationShip.Add(new RelationShip
            {
                RUid = user.UId,
                RCid = channel.CId,
                RStateId = 1,
                ActioinUserId = user.UId
            });
            await _dbContext.SaveChangesAsync();
            return Redirect(string.Format("/Channel/MyChannel/" + Cid));
        }

        [HttpPost,IgnoreAntiforgeryToken]
        public IActionResult RemoveFollower(string Cid)
        {
            var relation = _dbContext.RelationShip.FirstOrDefault(u => u.ActioinUserId.Equals(User.Identity.Name));
            _dbContext.Remove(relation);
            _dbContext.SaveChanges();

            //return RedirectToAction("MyChannel", "Channel", Cid);
            return Redirect(string.Format("/Channel/MyChannel/"+Cid));

        }

        /*11-Get specific Directory*/
        public IActionResult getSpecificDirectory(string Cid,string Did)
        {
            var userImg = _dbContext.UsersImg.FirstOrDefault(u => u.UId.Equals(User.Identity.Name));
            if (userImg != null)
            {
                ViewBag.img = Infrastructure.ImageConverter.ConvertToString(userImg.UImg);

            }
            var channelCache =(ModelChannel) cache.Get(Cid);
            ViewBag.Name = channelCache.getSpecificDirectoryThreads(Did)[0].getPeekData().directorName;
            ViewBag.Did = Did;
            checkFollowing(Cid);
            return View(channelCache);
        }

        /*12-Change Channel Description*/
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult changeChannelDescription(string Cid,string description)
        {
            var channel = _dbContext.Channel.FirstOrDefault(c => c.CId.Equals(Cid));
            channel.CDescription = description;

            var cacheChannel= (ModelChannel)cache.Get(Cid);
            cacheChannel.Description = description;

            _dbContext.SaveChanges();
            return Redirect("ChannelPanel?Cid=" + Cid);
        }

        /*13-Change channel Navbar Item*/
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult changeNavItem (string Cid,string Did,ModelNavBar item)
        {
            var navItem = _dbContext.NavBar.FirstOrDefault(n => n.NID.Equals(Did));
            navItem.NName = item.Name;
            navItem.NUrl = item.Url;
            _dbContext.SaveChanges();

            var channelCache = (ModelChannel) cache.Get(Cid);
            channelCache.ChangeNavItem(item, Did);
            return RedirectToAction("ChannelPanel", "Channel", Cid);
        }

        /*14-Get about page*/
        public ViewResult About(string Cid)
        {
            var userImg = _dbContext.UsersImg.FirstOrDefault(u => u.UId.Equals(User.Identity.Name));
            if (userImg != null)
            {
                ViewBag.img = Infrastructure.ImageConverter.ConvertToString(userImg.UImg);

            }

            var channel = (ModelChannel)cache.Get(Cid);
            checkFollowing(Cid);
            return View(channel);
        }


        /*15-Add Comment to thread*/
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult addComment(string Cid,string Did,string Tid,string Comment)
        {
            var thread = _dbContext.Files.FirstOrDefault(f => f.FId.Equals(Tid));
            var commnet = _dbContext.Entry(thread).Collection(c => c.FileComment).Query().Count();
            thread.FileComment.Add(new Models.Comment
            {
                CId = Tid + Convert.ToString(commnet) +1,
                CDepth = 0,
                CommentText = Comment,
                CUserId = User.Identity.Name,
                Date = DateTime.Now,
                CPid = Tid + Convert.ToString(commnet) +1
            });

            _dbContext.SaveChanges();
            return Redirect(string.Format($"/Channel/Thread/{Cid}/{Did}/{Tid}"));
        }

        
        public void BuildDirectorInAction(string id)
        {
            var channelCache = (ModelChannel)cache.Get(id);
            var directories = _dbContext.Directory.AsNoTracking().Where(d=>d.DOwnerId.Equals(id))
                                      .Select(f => new { f.DId, f.DName, f.DOwner, f.DDepth }).ToList();   //Now not support nested Directory

            foreach (var directory in directories)
            {
                var modelDirectory = new ModelDirectory(directory.DId, directory.DName);
                channelCache.addDirectory(modelDirectory);
            }
        }



        public void SetThreadInAction(String id)
        {
            var channelCache =(ModelChannel) cache.Get(id);
            foreach (var director in channelCache.getAllDirectory())
            {

                var files = _dbContext.Files.AsNoTracking().Where(s => s.FCid.Equals(director.ID));
                int total = files.Count();
                //files = files.OrderByDescending(e => e.FPublishDate);
                //files = files.Skip((pagenum - 1) * pageSize).Take(pageSize);
                       
                foreach (var file in files)
                {
                    string imgscr = null;
                    if (file.FImg != null)
                    {
                        var base64 = Convert.ToBase64String(file.FImg);
                        imgscr = string.Format("data:image/png;base64,{0}", base64);
                    }
                    ModelPeekThread peekThread = new ModelPeekThread()
                    {
                        ID = file.FId,
                        Name = file.FName,
                        Description = file.FDescription,
                        directorId = director.ID,
                        PublishDate = file.FPublishDate,
                        img = imgscr,
                        directorName = director.Name,
                        CID = channelCache.ID,
                    };

                    director.addThread(new ModelThread(peekThread, file.FText));
                    
                }
                channelCache.CalculateTotalPages();
                //ViewBag.CurentPage = pagenum;
                //ViewBag.ShowPrevious = pagenum > 1;
                //ViewBag.ShowNext = pagenum < total;

            }
            
        }



        private void SetChannelInAction(string Cid)
        {
            cache.GetOrCreate(Cid,
                cacheEntry =>
                {
                    var channel = _dbContext.Channel.FirstOrDefault(c => c.CId.Equals(Cid));
                    ModelChannel modelChannel = new ModelChannel(channel.CId, channel.CName, channel.CDescription, channel.CTotalWatch);
                    modelChannel.ownerID = channel.COwnerId;
                    var navbarItems = _dbContext.Entry(channel).Collection(e => e.NavBar).Query();
                    foreach (var item in navbarItems)
                    {
                        modelChannel.AddNavItem(new ModelNavBar
                        {
                            NID = item.NID,
                            Name = item.NName,
                            Url = item.NUrl
                        });
                    }
                    modelChannel.img = Infrastructure.ImageConverter.ConvertToString(channel.CIMG);
                    modelChannel.simg = Infrastructure.ImageConverter.ConvertToString(channel.CSIMG);
                    modelChannel.Followers = _dbContext.RelationShip.Where(c => c.RCid.Equals(channel.CId) && c.RStateId == 1).ToList().Count;
                    return modelChannel;
                });

        }

        private void buildChannelSteps(string id)
        {
            SetChannelInAction(id);
            BuildDirectorInAction(id);
            SetThreadInAction(id);
        }

        private void checkFollowing(string Cid)
        {
            var follow = _dbContext.RelationShip.AsNoTracking().FirstOrDefault(u => u.RCid.Equals(Cid) && u.ActioinUserId.Equals(User.Identity.Name));
            if (follow == null)
            {
                ViewBag.State = "N";
            }
            else if (follow.RStateId == 1)
            {
                ViewBag.State = "F";
            }
            else if (follow.RStateId == 2)
            {
                ViewBag.State = "B";
            }
        }
    }



}
 