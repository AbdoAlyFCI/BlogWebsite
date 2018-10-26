using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebsite.Controllers
{
    public class HomeController:Controller
    {
        public ViewResult Index()
        {
            return View();
        }

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
