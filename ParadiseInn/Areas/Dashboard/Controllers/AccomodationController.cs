using ParadiseInn.Areas.Dashboard.ViewModel;
using ParadiseInn.Entities;
using ParadiseInn.Services;
using ParadiseInn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParadiseInn.Areas.Dashboard.Controllers
{
    public class AccomodationController : Controller
    {
        AccomodationService accomodationService = new AccomodationService();
        AccomodationPackagesService accomodationPackagesService = new AccomodationPackagesService();

        public ActionResult Index(string searchterm, int? accomodationPackageId, int? page)
        {
            int recordSize = 5;

            page = page ?? 1;

            AllAccomodationViewModel model = new AllAccomodationViewModel();
            model.Accomodations = accomodationService.SearchAccomodations(searchterm, accomodationPackageId, page.Value, recordSize);
            model.AccomodationPackages = accomodationPackagesService.GetAllAccomodationPackages();
            model.searchterm = searchterm;
            model.accomodationPackageId = accomodationPackageId;

            var totalRecords = accomodationService.SearchAccomodationsCount(searchterm, accomodationPackageId);

            model.Pager = new Pager(totalRecords, page, recordSize);
            return View(model);
        }

        public ActionResult Listing()
        {
            AllAccomodationViewModel model = new AllAccomodationViewModel();
            model.Accomodations = accomodationService.GetAllAccomodations();

            return PartialView("_Listing", model);
        }

        [HttpGet]
        public ActionResult Action(int? id)
        {
            AccomodationActionViewModel model = new AccomodationActionViewModel();

            if (id.HasValue)
            {
                var getThisAccomodation = accomodationService.GetAccomodationsById(id.Value);

                model.Id = getThisAccomodation.Id;
                model.AccomodationName = getThisAccomodation.AccomodationName;
                model.Description = getThisAccomodation.Description;
                model.AccomodationPackageId = getThisAccomodation.AccomodationPackageId;
            }

            model.AccomodationPackages = accomodationPackagesService.GetAllAccomodationPackages();

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(AccomodationActionViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;

            if (model.Id > 0)
            {
                var accomodation = accomodationService.GetAccomodationsById(model.Id);

                accomodation.AccomodationName = model.AccomodationName;
                accomodation.Description = model.Description;
                accomodation.AccomodationPackageId = model.AccomodationPackageId;

                result = accomodationService.UpdateAccomodations(accomodation);
            }
            else
            {
                Accomodation newRecord = new Accomodation();

                newRecord.AccomodationName = model.AccomodationName;
                newRecord.Description = model.Description;
                newRecord.AccomodationPackageId = model.AccomodationPackageId;

                result = accomodationService.SaveAccomodations(newRecord);
            }

            if (result)
            {
                jsonResult.Data = new { Success = true };
            }
            else
            {
                jsonResult.Data = new { Success = false, Message = "Unable to perform action on  Accomodation!!!" };
            }

            return jsonResult;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            AccomodationActionViewModel model = new AccomodationActionViewModel();

            var accomodation = accomodationService.GetAccomodationsById(id);

            model.Id = accomodation.Id;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccomodationActionViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;

            var accomodation = accomodationService.GetAccomodationsById(model.Id);

            result = accomodationService.DeleteAccomodations(accomodation);

            if (result)
            {
                jsonResult.Data = new { Success = true };
            }
            else
            {
                jsonResult.Data = new { Success = false, Message = "Unable to perform action on  Accomodation!!!" };
            }

            return jsonResult;
        }
    }
}