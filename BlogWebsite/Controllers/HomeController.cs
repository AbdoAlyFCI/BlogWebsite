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

namespace BlogWebsite.Controllers
{
   
    public class HomeController:Controller
    {
        public InfiniteBlogDBContext _dbcontext;

        public HomeController(InfiniteBlogDBContext dBContext)
        {
            _dbcontext = dBContext;
        } 

        //[AllowAnonymous,HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    if (User.Identity.IsAuthenticated == true)
        //    {
        //        return RedirectToAction("MyFeed");
        //    }

        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //    return View();
        //}



        //[ValidateAntiForgeryToken]
        //[AllowAnonymous, HttpPost]
        //public async Task<IActionResult> SignUp(HomeData userModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View("Index");
        //    }
        //    userModel.newUser.firstName = userModel.newUser.firstName.Trim();
        //    userModel.newUser.lastName = userModel.newUser.lastName.Trim();
        //    userModel.newUser.Email = userModel.newUser.Email.Trim();
        //    userModel.newUser.Password = userModel.newUser.Password.Trim();

        //    var newUser = _dbcontext.Users.SingleOrDefault(i => i.UEmail.Equals(userModel.newUser.Email));
        //    if(newUser != null)
        //    {
        //        throw new Exception("Email already exists.");
        //    }

        //    var hasher = new PasswordHasher<Users>();
        //    newUser = new Users
        //    {
        //        UId = "U_"+Convert.ToString(_dbcontext.Users.ToList().Count+1),
        //        UEmail = userModel.newUser.Email,
        //        UFirstName = userModel.newUser.firstName,
        //        ULastName = userModel.newUser.lastName,
        //        ULogIn = DateTime.Today,
        //        USignUp = DateTime.Today,
        //        UBirthDay = userModel.newUser.birthDay +"-"+userModel.newUser.birthMonth +"-"+userModel.newUser.birthYear,
        //        Channel = new List<Channel>(),
        //        Comment = new List<Comment>(),
        //        Directory = new List<Directory>(),
        //        FileReact = new List<FileReact>(),
        //        RelationShip = new List<RelationShip>(),
                

        //    };
        //    newUser.UPassword = hasher.HashPassword(newUser, userModel.newUser.Password);


           


        //    await _dbcontext.Users.AddAsync(newUser);
        //    await _dbcontext.SaveChangesAsync();
        //    await LogInUserAsync(newUser);
        //    return RedirectToAction("Index","Home");
        //}


        

        //[AllowAnonymous,HttpPost]
        //public async Task<IActionResult>LogIn(HomeData userModel)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return View("Index");
        //    }
        //    var currentUser = _dbcontext.Users.SingleOrDefault(i => i.UEmail.Equals(userModel.currentUser.Email));
        //    if(currentUser == null)
        //    {
        //        throw new Exception("User does not exist.");
        //    }
        //    var hasher = new PasswordHasher<Users>();
        //    var passwordResult = hasher.VerifyHashedPassword(currentUser, currentUser.UPassword, userModel.currentUser.Password);
        //    if (passwordResult != PasswordVerificationResult.Success)
        //    {
        //        throw new Exception("The password is wrong.");
        //    }

        //    await LogInUserAsync(currentUser);
        //    return RedirectToAction("Index", "Home");
        //}



        //[ValidateAntiForgeryToken]
        //[HttpPost] 
        //public async Task<IActionResult> LogOut()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return RedirectToAction("Index", "Home");
        //}




        //private async Task LogInUserAsync(Users user)
        //{
        //    var claims = new List<Claim> {
        //        new Claim(ClaimTypes.NameIdentifier,user.UId),
        //        new Claim(ClaimTypes.Name,user.UFirstName),
        //        new Claim(ClaimTypes.Email,user.UEmail)
        //    };

        //    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //    var principal = new ClaimsPrincipal(identity);


        //    var claimsIndentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //    var claimsPrincipal = new ClaimsPrincipal(claimsIndentity);
        //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

        //    await _dbcontext.SaveChangesAsync();

        //}



















       [Authorize(CookieAuthenticationDefaults.AuthenticationScheme)]
        public ViewResult MyFeed()
        {
            return View();
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
