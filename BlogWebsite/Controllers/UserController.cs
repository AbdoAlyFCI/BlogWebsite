using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BlogWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using BlogWebsite.Models.ClassDiagram;
using System.Threading.Tasks;
using System.IO;
using BlogWebsite.Models.DataModel;
using Microsoft.AspNetCore.Identity;
using BlogWebsite.Infrastructure;
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

        //int pageSize = 6;


        public ViewResult MyFeed(int page=1)
        {
            var dbuser = _dbcontext.Users.FirstOrDefault(u => u.UId.Equals(User.Identity.Name));

            //var jsonUser = HttpContext.Session.GetString(User.Identity.Name);
            ModelUser user = new ModelUser();
            var userImg = _dbcontext.UsersImg.FirstOrDefault(u => u.UId.Equals(User.Identity.Name));
            if(userImg != null)
            {
                ViewBag.img = Infrastructure.ImageConverter.ConvertToString(userImg.UImg);

            }

            var userChannelRelation = _dbcontext.RelationShip.Where(u => u.ActioinUserId.Equals(User.Identity.Name) && u.RStateId == 1);
            var Userchannel = from RelationShip in _dbcontext.RelationShip
                              join channnel in _dbcontext.Channel on RelationShip.RCid equals channnel.CId
                              where RelationShip.ActioinUserId == User.Identity.Name
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
            int total = fileDirectory.Count();
            int totalPages = (int)Math.Ceiling(decimal.Divide(total, GeneralData.pageSize));
            fileDirectory = fileDirectory.OrderByDescending(f => f.TDate);
            fileDirectory = fileDirectory.Skip((page - 1) * GeneralData.pageSize)
                                       .Take(GeneralData.pageSize);


            foreach (var file in fileDirectory)
            {
                ModelPeekThread peekThread = new ModelPeekThread()
                {
                    ID = file.TID,
                    Name = file.TName,
                    Description = file.TDescription,
                    PublishDate = file.TDate,
                    directorId = file.DID,
                    directorName = file.DName,
                    CID = file.CID,
                    Cname = file.CName,
                    img = Infrastructure.ImageConverter.ConvertToString(file.TPic)
                };
                user.addThread(new ModelThread(peekThread,file.TText));
            }

            ViewBag.CurentPage = page;
            ViewBag.ShowPrevious = page > 1;
            ViewBag.ShowNext = page < totalPages;



            //ViewBag.ChannelID = user.ChannelID;
            return View(user);
        }



        public ViewResult Discover()
        {
            var userImg = _dbcontext.UsersImg.FirstOrDefault(u => u.UId.Equals(User.Identity.Name));
            if (userImg != null)
            {
                ViewBag.img = Infrastructure.ImageConverter.ConvertToString(userImg.UImg);

            }
            return View();
        }



        
        
        public ViewResult AccountSetting()
        {
            var userImg = _dbcontext.UsersImg.FirstOrDefault(u => u.UId.Equals(User.Identity.Name));
            if (userImg != null)
            {
                ViewBag.img = Infrastructure.ImageConverter.ConvertToString(userImg.UImg);

            }
            var dbUser = _dbcontext.Users.FirstOrDefault(u => u.UId.Equals(User.Identity.Name));
            ModelUser user = new ModelUser(dbUser.UId, dbUser.UEmail, dbUser.UFirstName, dbUser.ULastName, dbUser.UBirthDay);
            var dbuserImg = _dbcontext.UsersImg.FirstOrDefault(u => u.UId.Equals(User.Identity.Name));
            user.img = Infrastructure.ImageConverter.ConvertToString(dbuserImg.UImg);
            return View(user);
        }




        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult changeUserPic(IFormFile Pic)
        {

            if (Pic != null)
            {
                if (Pic.Length > 0)
                {
                    byte[] p1 = Infrastructure.ImageConverter.convertToByte(Pic);
                    var imgUser = _dbcontext.UsersImg.FirstOrDefault(u => u.UId.Equals(User.Identity.Name));
                    imgUser.UImg = p1;
                    _dbcontext.SaveChanges();
                }
            }
            return RedirectToAction("AccountSetting", "User");

        }

        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult passwordChange(passwordModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AccountSetting", "User");
            }
            var hasher = new PasswordHasher<Users>();
            Users user = new Users();
            user.UPassword = hasher.HashPassword(user, model.Password);

            var dbuser = _dbcontext.Users.FirstOrDefault(u => u.UId.Equals(User.Identity.Name));
            dbuser.UPassword = user.UPassword;
            _dbcontext.SaveChanges();
            return RedirectToAction("AccountSetting", "User");

        }








    }
}
