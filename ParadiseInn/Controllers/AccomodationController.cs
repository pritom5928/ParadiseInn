using ParadiseInn.Services;
using ParadiseInn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParadiseInn.Controllers
{
    public class AccomodationController : Controller
    {
        AccomodationTypesService accomodationTypesService = new AccomodationTypesService();
        AccomodationPackagesService accomodationPackagesService = new AccomodationPackagesService();
        AccomodationService accomodationService = new AccomodationService();

        // GET: Accomodation
        public ActionResult Index(int accomodationTypeId, int? accomodationPacakageId)
        {
            AccomodationViewModel model = new AccomodationViewModel();

            model.AccomodationPackages = accomodationPackagesService.GetAllAccomodationPackagesbyAccomodationType(accomodationTypeId);

            model.AccomodationType = accomodationTypesService.GetAccomodationTypeById(accomodationTypeId);

            model.selectedAccomodationPackageId = accomodationPacakageId.HasValue ? accomodationPacakageId : model.AccomodationPackages.FirstOrDefault() != null ? model.AccomodationPackages.FirstOrDefault().Id : 0;

            model.Accomodations = accomodationService.GetAllAccomodationsByAccomodationPackage(model.selectedAccomodationPackageId.Value);

            return View(model);
        }

        public ActionResult Details(int accomodationPackageId)
        {
            AccomodationPackageDetailsViewModel model = new AccomodationPackageDetailsViewModel();

            model.AccomodationPackage = accomodationPackagesService.GetAccomodationPackageById(accomodationPackageId);

            return View(model);
        }

        [HttpPost]
        public ActionResult CheckAvailablity(CheckAccomodationAvailablityViewModel model)
        {
            

            return View();
        }
    }
}