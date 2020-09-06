using ParadiseInn.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParadiseInn.Areas.Dashboard.ViewModel;
using ParadiseInn.Entities;

namespace ParadiseInn.Areas.Dashboard.Controllers
{
    public class AccomodationTypesController : Controller
    {
        AccomodationTypesService accomodationTypesService = new AccomodationTypesService();
        public ActionResult Index(string searchterm)
        {
            AllAccomodationTypesViewModel model = new AllAccomodationTypesViewModel();
            model.AccomodationTypes = accomodationTypesService.SearchAccomodationTypes(searchterm);
            model.searchterm = searchterm;
            return View(model);
        }

        //public ActionResult Listing()
        //{
        //    AllAccomodationTypesViewModel model = new AllAccomodationTypesViewModel();
        //    model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();

        //    return PartialView("_Listing", model);
        //}

        [HttpGet]
        public ActionResult Action(int? id)
        {
            AccomodationTypesActionViewModel model = new AccomodationTypesActionViewModel();

            if (id.HasValue)
            {
                var getThisAccomodationType = accomodationTypesService.GetAccomodationTypeById(id.Value);

                model.Id = getThisAccomodationType.Id;
                model.Name = getThisAccomodationType.Name;
                model.Description = getThisAccomodationType.Description;
            }
            

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(AccomodationTypesActionViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;

            if (model.Id > 0) 
            {
                var accomodationType = accomodationTypesService.GetAccomodationTypeById(model.Id);

                accomodationType.Name = model.Name;
                accomodationType.Description = model.Description;

                result = accomodationTypesService.UpdateAccomodationType(accomodationType);
            }
            else
            {
                AccomodationType newRecord = new AccomodationType();

                newRecord.Name = model.Name;
                newRecord.Description = model.Description;

                result = accomodationTypesService.SaveAccomodationType(newRecord);
            }

            if (result)
            {
                jsonResult.Data = new { Success = true};
            }
            else
            {
                jsonResult.Data = new { Success = false, Message = "Unable to perform action on  Accomodation Type!!!" };
            }

            return jsonResult;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            AccomodationTypesActionViewModel model = new AccomodationTypesActionViewModel();

            var accomodationType = accomodationTypesService.GetAccomodationTypeById(id);

            model.Id = accomodationType.Id;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccomodationTypesActionViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;

            var accomodationType = accomodationTypesService.GetAccomodationTypeById(model.Id);

            result = accomodationTypesService.DeleteAccomodationType(accomodationType);

            if (result)
            {
                jsonResult.Data = new { Success = true };
            }
            else
            {
                jsonResult.Data = new { Success = false, Message = "Unable to perform action on  Accomodation Type!!!" };
            }

            return jsonResult;
        }
    }
}