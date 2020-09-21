using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using ParadiseInn.Areas.Dashboard.ViewModel;
using ParadiseInn.Services;
using ParadiseInn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ParadiseInn.Areas.Dashboard.Controllers
{
    public class RolesController : Controller
    {
        private HMSSignInManager _signInManager;
        private HMSUserManager _userManager;
        private HMSRolesManger _roleManager;

        public HMSSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<HMSSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        
        public HMSUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<HMSUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public HMSRolesManger RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<HMSRolesManger>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public RolesController()
        {
        }

        public RolesController(HMSUserManager userManager, HMSSignInManager signInManager, HMSRolesManger roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }


        public ActionResult Index(string searchterm, int? page)
        {
            int recordSize = 3;

            page = page ?? 1;

            AllRolesViewModel model = new AllRolesViewModel();

            model.searchterm = searchterm;
            model.Roles = SearchRoles(searchterm, page.Value, recordSize);

            var totalRecords = SearchRolesCount(searchterm);
            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        public ActionResult Listing()
        {
            AllAccomodationViewModel model = new AllAccomodationViewModel();
            //model.Accomodations = accomodationService.GetAllAccomodations();

            return PartialView("_Listing", model);
        }

        #region: Service related methods && helper function
        public IEnumerable<IdentityRole> SearchRoles(string searchterm, int page, int recordSize)
        {
            var roles = RoleManager.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(searchterm))
            {
                roles = roles.Where(s => s.Name.ToLower().Contains(searchterm.ToLower()));
            }

            var skip = (page - 1) * recordSize;

            return roles.OrderBy(s => s.Name).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchRolesCount(string searchterm)
        {
            var roles = RoleManager.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(searchterm))
            {
                roles = roles.Where(s => s.Name.ToLower().Contains(searchterm.ToLower()));
            }

            return roles.Count();
        }
        #endregion
        [HttpGet]
        public async Task<ActionResult> Action(string id)
        {
            RolesActionViewModel model = new RolesActionViewModel();

            if (!string.IsNullOrEmpty(id))
            {
                var role = await RoleManager.FindByIdAsync(id);

                model.Id = role.Id;
                model.Name = role.Name;
            }
            

            return PartialView("_Action", model);
        }

        [HttpPost]
        public async Task<JsonResult> Action(RolesActionViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            IdentityResult result = null;
            var user = await UserManager.FindByIdAsync(model.Id);

            if (!string.IsNullOrEmpty(model.Id))
            {
                var role = await RoleManager.FindByIdAsync(model.Id);

                role.Name = model.Name;

                result = await RoleManager.UpdateAsync(role);
            }
            else
            {
                var role = new IdentityRole();

                role.Name = model.Name;

                result = await RoleManager.CreateAsync(role);

            }

            jsonResult.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };

            return jsonResult;
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            RolesActionViewModel model = new RolesActionViewModel();
            
            var role = await RoleManager.FindByIdAsync(id);

            model.Id = role.Id;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(RolesActionViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.Id))
            {
                var role = await RoleManager.FindByIdAsync(model.Id);

                result = await RoleManager.DeleteAsync(role);

                jsonResult.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            }
            else
                jsonResult.Data = new { Success = false, Message = "Invalid User." };


            return jsonResult;
        }
    }
}