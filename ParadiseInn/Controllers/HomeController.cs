using ParadiseInn.Services;
using ParadiseInn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParadiseInn.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AccomodationTypesService accomoTypeService = new AccomodationTypesService();
            AccomodationPackagesService accomodationPackagesService = new AccomodationPackagesService();

            HomeViewModel model = new HomeViewModel();
            model.accomodationTypes = accomoTypeService.GetAllAccomodationTypes();
            model.accomodationPackages = accomodationPackagesService.GetAllAccomodationPackages();
            
            return View(model);
        }
        
    }
}