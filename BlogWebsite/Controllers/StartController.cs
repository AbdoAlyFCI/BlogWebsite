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
using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;
using BlogWebsite.Models.ClassDiagram;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using BlogWebsite.Infrastructure;
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
                PutUserInSession(User.Identity.Name);
                return RedirectToAction("MyFeed", "User");
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
                return View("Welcome");
            }
            userModel.newUser.firstName = userModel.newUser.firstName.Trim();
            userModel.newUser.lastName = userModel.newUser.lastName.Trim();
            userModel.newUser.Email = userModel.newUser.Email.Trim();
            userModel.newUser.Password = userModel.newUser.Password.Trim();
            var newUser = _dbContext.Users.SingleOrDefault(i => i.UEmail.Equals(userModel.newUser.Email));
            if (newUser != null)
            {
                return View("Welcome");
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

            var userImg = new UsersImg
            {
                UId = newUser.UId,
                UImg = ImageConverter.convertToByte(userModel.newUser.Pic)
            };



            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.UsersImg.AddAsync(userImg);
            await _dbContext.SaveChangesAsync();
            await LogInUserAsync(newUser.UId);
            return RedirectToAction("MyFeed", "User");
        }

        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> LogIn(HomeData userModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Welcome");
            }
            var currentUser = _dbContext.Users.SingleOrDefault(i => i.UEmail.Equals(userModel.currentUser.Email));
            if (currentUser == null)
            {
                //Will Change Soon

                //throw new Exception("User does not exist.");
            }
            var hasher = new PasswordHasher<Users>();
            var passwordResult = hasher.VerifyHashedPassword(currentUser, currentUser.UPassword, userModel.currentUser.Password);
            if (passwordResult != PasswordVerificationResult.Success)
            {
                //Will Change Soon
                //throw new Exception("The password is wrong.");
                return View("Welcome");

            }

            await LogInUserAsync(currentUser.UId);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Welcome));
        }


        private async Task LogInUserAsync(string ID)
        {
            
            var claims = new List<Claim> {
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
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);            
            return RedirectToAction("Welcome", "Start");
        }


        private void PutUserInSession(string ID)
        {
            var DBuser = _dbContext.Users.FirstOrDefault(U => U.UId.Equals(ID));
            var sessionUser = new ModelUser(DBuser.UId, DBuser.UEmail, DBuser.UFirstName, DBuser.ULastName, DBuser.UBirthDay);
            var userChannel = _dbContext.Entry(DBuser)
                             .Collection(u => u.Channel).Query()
                             .FirstOrDefault(U => U.COwnerId.Equals(ID));
            var userImg = _dbContext.UsersImg.FirstOrDefault(u => u.UId.Equals(ID));
            if(userImg != null)
            {
                ViewBag.img = Infrastructure.ImageConverter.ConvertToString(userImg.UImg);
            }

            if (userChannel != null)
            {
                sessionUser.ChannelID=userChannel.CId;
            }
            var jsonUser= JsonConvert.SerializeObject(sessionUser);
            HttpContext.Session.SetString(ID, jsonUser);
        }


    }
}