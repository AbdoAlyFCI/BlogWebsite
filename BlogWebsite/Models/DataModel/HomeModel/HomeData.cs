using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebsite.Models.DataModel.HomeModel
{
    public class HomeData
    {
        public signUpModel newUser { get; set; }
        public logInModel currentUser { get; set; }
    }
}
