using ParadiseInn.Areas.Dashboard.ViewModel;
using ParadiseInn.Entities;
using ParadiseInn.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParadiseInn.Areas.Dashboard.Controllers
{
    public class AccomodationPackagesController : Controller
    {

        AccomodationPackagesService accomodationPackagesService = new AccomodationPackagesService();
        AccomodationTypesService accomodationTypesService = new AccomodationTypesService();

        public ActionResult Index(string searchterm, int? accomodationTypeId)
        {
            AllAccomodationPackagesViewModel model = new AllAccomodationPackagesViewModel();
            model.AccomodationPackages = accomodationPackagesService.SearchAccomodationPackages(searchterm, accomodationTypeId);
            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();
            model.searchterm = searchterm;
            model.accomodationTypeId = accomodationTypeId;
            return View(model);
        }

        //public ActionResult Listing()
        //{
        //    AllAccomodationPackagesViewModel model = new AllAccomodationPackagesViewModel();
        //    model.AccomodationPackages = accomodationPackagesService.GetAllAccomodationPackages();

        //    return PartialView("_Listing", model);
        //}

        [HttpGet]
        public ActionResult Action(int? id)
        {
            AccomodationPackagesActionViewModel model = new AccomodationPackagesActionViewModel();

            if (id.HasValue)
            {
                var getThisAccomodationPackage = accomodationPackagesService.GetAccomodationPackageById(id.Value);

                model.Id = getThisAccomodationPackage.Id;
                model.Name = getThisAccomodationPackage.Name;
                model.NoOfRoom = getThisAccomodationPackage.NoOfRoom;
                model.FeePerNight = getThisAccomodationPackage.FeePerNight;
                model.AccomodationTypeId = getThisAccomodationPackage.AccomodationTypeId;
            }

            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(AccomodationPackagesActionViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;

            if (model.Id > 0)
            {
                var accomodationPackage = accomodationPackagesService.GetAccomodationPackageById(model.Id);

                accomodationPackage.Name = model.Name;
                accomodationPackage.NoOfRoom = model.NoOfRoom;
                accomodationPackage.FeePerNight = model.FeePerNight;
                accomodationPackage.AccomodationTypeId = model.AccomodationTypeId;

                result = accomodationPackagesService.UpdateAccomodationPackage(accomodationPackage);
            }
            else
            {
                AccomodationPackage newRecord = new AccomodationPackage();

                newRecord.Name = model.Name;
                newRecord.NoOfRoom = model.NoOfRoom;
                newRecord.FeePerNight = model.FeePerNight;
                newRecord.AccomodationTypeId = model.AccomodationTypeId;

                result = accomodationPackagesService.SaveAccomodationPackage(newRecord);
            }

            if (result)
            {
                jsonResult.Data = new { Success = true };
            }
            else
            {
                jsonResult.Data = new { Success = false, Message = "Unable to perform action on  Accomodation Package!!!" };
            }

            return jsonResult;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            AccomodationPackagesActionViewModel model = new AccomodationPackagesActionViewModel();

            var accomodationPackage = accomodationPackagesService.GetAccomodationPackageById(id);

            model.Id = accomodationPackage.Id;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccomodationPackagesActionViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;

            var accomodationPackage = accomodationPackagesService.GetAccomodationPackageById(model.Id);

            result = accomodationPackagesService.DeleteAccomodationPackage(accomodationPackage);

            if (result)
            {
                jsonResult.Data = new { Success = true };
            }
            else
            {
                jsonResult.Data = new { Success = false, Message = "Unable to perform action on  Accomodation Package!!!" };
            }

            return jsonResult;
        }
    }
}