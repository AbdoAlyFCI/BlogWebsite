using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using BlogWebsite.Models.ClassDiagram;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Html;

namespace BlogWebsite.ViewComponents
{
    public class DiscoverThreadViewComponent: ViewComponent
    {
        private InfiniteBlogDBContext _dbContext;
        private IMemoryCache cache;
        public DiscoverThreadViewComponent(InfiniteBlogDBContext dBContext, IMemoryCache cache)
        {
            _dbContext = dBContext;
            this.cache = cache;
        }

        public IViewComponentResult Invoke()
        {
            List<ModelPeekThread> modelPeeks = new List<ModelPeekThread>();
            var Files = _dbContext.Files.ToList();
            var allData = from file in Files
                          join directory in _dbContext.Directory on file.FCid equals directory.DId
                          select new
                          {
                              Cid=directory.DOwnerId,
                              Did=directory.DId,
                              Tid=file.FId,
                              Name=file.FName,
                              Description=file.FDescription,
                              date=file.FPublishDate,
                              img=file.FImg
                              
                          };
            foreach (var file in allData)
            {
                modelPeeks.Add(new ModelPeekThread
                {
                    ID=file.Tid,
                    Name=file.Name,
                    Description=file.Description,
                    img=Infrastructure.ImageConverter.ConvertToString(file.img),
                    PublishDate=file.date,
                    directorId=file.Did,
                    CID=file.Cid
                });
            }

            return View(modelPeeks);
        }
    }
}
