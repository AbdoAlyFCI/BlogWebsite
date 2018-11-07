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

namespace BlogWebsite.Controllers
{
    public class StartController : Controller
    {
        private InfiniteBlogDBContext _dbContext;
        public StartController(InfiniteBlogDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        [AllowAnonymous,HttpGet]
        public async Task<IActionResult> Welcome()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                return RedirectToAction("MyFeed", "Home");
            }
            await
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return View();
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

            await LogInUserAsync(currentUser);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Welcome));
        }


        private async Task LogInUserAsync(Users user)
        {
            var claims = new List<Claim> {
                //new Claim(ClaimTypes.NameIdentifier,user.UId),
                new Claim(ClaimTypes.Name,user.UFirstName),
                new Claim(ClaimTypes.Email,user.UEmail)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);


            var claimsIndentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIndentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            

        }
    }
}