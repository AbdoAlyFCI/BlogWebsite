using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using BlogWebsite.Models.DataModel.HomeModel;
using BlogWebsite.Models.LocalRepo;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using BlogWebsite.Models.ClassDiagram;

namespace BlogWebsite.Controllers
{
    public class StartController : Controller
    {
        private InfiniteBlogDBContext _dbContext;
        private IMemoryCache cache;
        public StartController(InfiniteBlogDBContext dBContext,IMemoryCache cache)
        {
            _dbContext = dBContext;
            this.cache = cache;
        }



        [AllowAnonymous,HttpGet]
        public async Task<IActionResult> Welcome()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                //PutUserDataInAction(User.Identity.Name);
                setUserInAction(User.Identity.Name);
                return RedirectToAction("MyFeed", "Home");
            }
            await
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return View();
        }

        [ValidateAntiForgeryToken]
        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> SignUp(HomeData userModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            userModel.newUser.firstName = userModel.newUser.firstName.Trim();
            userModel.newUser.lastName = userModel.newUser.lastName.Trim();
            userModel.newUser.Email = userModel.newUser.Email.Trim();
            userModel.newUser.Password = userModel.newUser.Password.Trim();

            var newUser = _dbContext.Users.SingleOrDefault(i => i.UEmail.Equals(userModel.newUser.Email));
            if (newUser != null)
            {
                throw new Exception("Email already exists.");
            }

            var hasher = new PasswordHasher<Users>();
            newUser = new Users
            {
                UId = "U_" + Convert.ToString(_dbContext.Users.ToList().Count + 1),
                UEmail = userModel.newUser.Email,
                UFirstName = userModel.newUser.firstName,
                ULastName = userModel.newUser.lastName,
                ULogIn = DateTime.Today,
                USignUp = DateTime.Today,
                UBirthDay = userModel.newUser.birthDay + "-" + userModel.newUser.birthMonth + "-" + userModel.newUser.birthYear,
                Channel = new List<Channel>(),
                Comment = new List<Comment>(),
                FileReact = new List<FileReact>(),
                RelationShip = new List<RelationShip>(),
            };
            newUser.UPassword = hasher.HashPassword(newUser, userModel.newUser.Password);





            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();
            await LogInUserAsync(newUser.UId);
            setUserInCasheForFirstTime(newUser);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> LogIn(HomeData userModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            var currentUser = _dbContext.Users.SingleOrDefault(i => i.UEmail.Equals(userModel.currentUser.Email));
            if (currentUser == null)
            {
                throw new Exception("User does not exist.");
            }
            var hasher = new PasswordHasher<Users>();
            var passwordResult = hasher.VerifyHashedPassword(currentUser, currentUser.UPassword, userModel.currentUser.Password);
            if (passwordResult != PasswordVerificationResult.Success)
            {
                throw new Exception("The password is wrong.");
            }

            await LogInUserAsync(currentUser.UId);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Welcome));
        }


        private async Task LogInUserAsync(string ID)
        {
            
            var claims = new List<Claim> {
                //new Claim(ClaimTypes.NameIdentifier,user.UId),
                new Claim(ClaimTypes.Name,ID)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
        


            var claimsIndentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIndentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);           
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
             cache.Remove(User.Identity.Name);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return RedirectToAction("Welcome", "Start");
        }

        private void PutUserDataInAction(string ID)
        {
            cache.GetOrCreate(ID,
              cacheEntry =>
              {
                  var user = _dbContext.Users.FirstOrDefault(s => s.UId.Equals(ID));
                  var channel = _dbContext.Entry(user)
                      .Collection(c => c.Channel).Query().Select(s => new { s.CId, s.CName }).ToList();
                  var getChannel = _dbContext.Channel.FirstOrDefault(s => s.COwnerId.Equals(ID));
                  var directory = _dbContext.Entry(getChannel)
                      .Collection(d => d.Directory).Query().Select(s => new { s.DName }).ToList();
                  localUser putUser = new localUser()
                  {
                      ID = user.UId,
                      firstName = user.UFirstName,
                      lastName = user.ULastName,
                      Email = user.UEmail,
                      USignUP = user.USignUp,
                      lastLogIn = user.ULogIn,
                  };

                  if (channel.Count != 0)
                  {
                      putUser.myChannel = new localChannel()
                      {
                          ID = channel[0].CId,
                          Name = channel[0].CName,
                      };
                  }

                  if (directory.Count != 0)
                  {
                      foreach (var item in directory)
                      {
                          putUser.myChannel.directory.Add(new localDirectory
                          {
                              Name = item.DName
                          });
                      }
                  }
                  UsersImg ImgData = _dbContext.UsersImg.FirstOrDefault(s => s.UId == User.Identity.Name);
                  var base64 = Convert.ToBase64String(ImgData.UImg);
                  putUser.Img = string.Format("data:image/png;base64,{0}", base64);
                  ViewData["img"] = putUser.Img;
                  return putUser;
              });
        }

        private void setUserInCasheForFirstTime(Users User)
        {
            ModelUser newUser = new ModelUser(User.UId, User.UEmail, User.UFirstName, User.ULastName, User.UBirthDay);
            cache.Set(User.UId, newUser);                    
        }

        private void setUserInAction(string ID)
        {
            cache.GetOrCreate(ID,
                cacheEntry =>
                {
                    var dbUser = _dbContext.Users.FirstOrDefault(u => u.UId.Equals(ID));
                    ModelUser modelUser = new ModelUser(dbUser.UId, dbUser.UEmail, dbUser.UFirstName, dbUser.ULastName, dbUser.UBirthDay);

                    Channel dbChannel = _dbContext.Entry(dbUser)
                                  .Collection(c => c.Channel).Query()
                                  .FirstOrDefault(c => c.COwnerId.Equals(ID));
                    if(dbChannel != null)
                    {
                        setChannelInAction(dbChannel);
                    }
                    modelUser.RegisterChannel((ModelChannel)cache.Get(dbChannel.CId));

                    UsersImg ImgData = _dbContext.UsersImg.FirstOrDefault(s => s.UId == User.Identity.Name);
                    var base64 = Convert.ToBase64String(ImgData.UImg);
                    modelUser.changeImg(string.Format("data:image/png;base64,{0}", base64));
                    return modelUser;
                });

        }


        private void setChannelInAction(Channel channel)
        {
            cache.GetOrCreate(channel.CId,
                cacheEntry =>
                {
                    ModelChannel modelChannel = new ModelChannel(channel.CId, channel.CName, channel.CDescription, channel.CTotalWatch);
                    var directories=_dbContext.Entry(channel)
                                    .Collection(d=>d.Directory).Query()
                                    .Select(d => new { d.DId, d.DName, d.DType, d.DDepth }).ToList();     //Now not support nested Directory
                    foreach (var directory in directories)
                    {
                        ModelDirectory modelDirectory = new ModelDirectory(directory.DId, directory.DName, directory.DDepth);
                        modelChannel.createDirectory(directory.DId, modelDirectory);
                    }
                    return modelChannel;
                });           
        }


    }
}