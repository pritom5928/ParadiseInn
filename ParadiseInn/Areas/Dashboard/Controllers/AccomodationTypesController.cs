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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listing()
        {
            AllAccomodationTypesViewModel model = new AllAccomodationTypesViewModel();
            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();

            return PartialView("_Listing", model);
        }

        public ActionResult Action()
        {
            AccomodationTypesActionViewModel model = new AccomodationTypesActionViewModel();

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(AccomodationTypesActionViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            AccomodationType newRecord = new AccomodationType();

            newRecord.Name = model.Name;
            newRecord.Description = model.Description;

            var result =  accomodationTypesService.SaveAccomodationType(newRecord);

            if (result)
            {
                jsonResult.Data = new { Success = true};
            }
            else
            {
                jsonResult.Data = new { Success = false, Message = "Unable To Add Accomodation Type!!!" };
            }

            return jsonResult;
        }
    }
}