using ParadiseInn.Entities;
using ParadiseInn.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParadiseInn.Areas.Dashboard.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadPictures()
        {
            JsonResult result = new JsonResult();

            var picList = new List<Picture>();

            var imgFiles = Request.Files;

            var dashboradService = new DashboardService();

            for (int i = 0; i < imgFiles.Count; i++)
            {
                var picture = imgFiles[i];

                var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);

                var filePath = Server.MapPath("~/Images/site/") + fileName;

                picture.SaveAs(filePath);

                var newPicture = new Picture();

                newPicture.URL = fileName;


                if (dashboradService.SavePicture(newPicture))
                {
                    picList.Add(newPicture);
                }
            }

            result.Data = picList;

            return result;
        }
    }
}