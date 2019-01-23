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
using Newtonsoft.Json;
using BlogWebsite.Models.ClassDiagram;

namespace BlogWebsite.Controllers
{
    [Authorize]
    public class UserController:Controller
    {
        public InfiniteBlogDBContext _dbcontext;
        private IMemoryCache cache;

        public UserController(InfiniteBlogDBContext dBContext,IMemoryCache cache)
        {
            _dbcontext = dBContext;
            this.cache = cache;
        } 


        public ViewResult MyFeed()
        {
            //var dbuser = _dbcontext.Users.FirstOrDefault(u => u.UId.Equals(User.Identity.Name));
 
            var jsonUser = HttpContext.Session.GetString(User.Identity.Name);
            var user = JsonConvert.DeserializeObject<ModelUser>(jsonUser);
            var userImg = _dbcontext.UsersImg.FirstOrDefault(u => u.UId.Equals(User.Identity.Name));
            if(userImg != null)
            {
                ViewBag.img = RetriveImg(userImg.UImg);

            }

            var userChannelRelation = _dbcontext.RelationShip.Where(u => u.ActioinUserId.Equals(user.ID) && u.RStateId == 1);
            var Userchannel = from RelationShip in _dbcontext.RelationShip
                              join channnel in _dbcontext.Channel on RelationShip.RCid equals channnel.CId
                              where RelationShip.ActioinUserId == user.ID
                              select new
                              {
                                  ID = channnel.CId,
                                  CName = channnel.CName,
                              };

            var directoryChannel = from UChannel in Userchannel
                                   join directory in _dbcontext.Directory on UChannel.ID equals directory.DOwnerId
                                   select new
                                   {
                                       CID = UChannel.ID,
                                       CName = UChannel.CName,
                                       DID = directory.DId,
                                       DName = directory.DName
                                   };

            var fileDirectory = from DChannel in directoryChannel
                                join thread in _dbcontext.Files on DChannel.DID equals thread.FCid
                                select new
                                {
                                    CID = DChannel.CID,
                                    CName = DChannel.CName,
                                    DID = DChannel.DID,
                                    DName = DChannel.DName,
                                    TID = thread.FId,
                                    TName = thread.FName,
                                    TText = thread.FText,
                                    TDate = thread.FPublishDate,
                                    TDescription = thread.FDescription,
                                    TPic = thread.FImg,
                                };
            foreach (var file in fileDirectory)
            {
                ModelThread thread = new ModelThread()
                {
                    ID = file.TID,
                    Name = file.TName,
                    Description = file.TDescription,
                    PublishDate = file.TDate,
                    directorId = file.DID,
                    directorname = file.DName,
                    Texts = file.TText,
                    CID = file.CID,
                    CName = file.CName,
                    img = RetriveImg(file.TPic)
                };
                user.addThread(thread);
            }
            ViewBag.ChannelID = user.ChannelID;
            return View(user);
        }

        public ViewResult Discover()
        {

            return View();
        }





        public ViewResult AccountSetting()
        {
            return View();
        }
        [HttpPost]
        public ViewResult AccountSetting(IFormFile Pic)
        {            
            return View();
        }









        private string RetriveImg(byte[] img)
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
