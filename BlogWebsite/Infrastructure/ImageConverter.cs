using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebsite.Infrastructure
{
    public static class ImageConverter
    {
        public static byte[] convertToByte(IFormFile Pic)
        {
            byte[] p1 = null;
            using (var fs1 = Pic.OpenReadStream())
            using (var ms1 = new MemoryStream())
            {
                fs1.CopyTo(ms1);
                p1 = ms1.ToArray();
            }
            return p1;
        }

        public static string ConvertToString(Byte[] img)
        {
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

