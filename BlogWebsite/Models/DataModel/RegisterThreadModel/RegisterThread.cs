using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebsite.Models.DataModel.RegisterThreadModel
{
    public class RegisterThread
    {


        public DataThread DataThread { get; set; }
        public Dictionary<string, string> AvailbleDirectory = new Dictionary<string, string>();

    }
}
