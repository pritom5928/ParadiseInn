using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ParadiseInn.Areas.Dashboard.ViewModel;
using ParadiseInn.Entities;
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
    public class UsersController : Controller
    {
        private HMSSignInManager _signInManager;
        private HMSUserManager _userManager;

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

        public UsersController()
        {
        }

        public UsersController(HMSUserManager userManager, HMSSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        
        AccomodationService accomodationService = new AccomodationService();
        AccomodationPackagesService accomodationPackagesService = new AccomodationPackagesService();

        public ActionResult Index(string searchterm, string roleId, int? page)
        {
            int recordSize = 1;

            page = page ?? 1;

            AllUsersViewModel model = new AllUsersViewModel();
            
            model.searchterm = searchterm;
            model.RoleId = roleId;
            model.Users = SearchUsers(searchterm, roleId, page.Value, recordSize);

            var totalRecords = SearchUsersCount(searchterm, roleId); //accomodationService.SearchAccomodationsCount(searchterm, roleId);

            model.Pager = new Pager(totalRecords, page, recordSize);
            return View(model);
        }

        public ActionResult Listing()
        {
            AllAccomodationViewModel model = new AllAccomodationViewModel();
            model.Accomodations = accomodationService.GetAllAccomodations();

            return PartialView("_Listing", model);
        }

        #region: Service related methods && helper function
        public IEnumerable<HMSUser> SearchUsers(string searchterm, string roleId, int page, int recordSize)
        {
            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchterm))
            {
                users = users.Where(s => s.Email.ToLower().Contains(searchterm.ToLower()) || s.UserName.ToLower().Contains(searchterm.ToLower()));
            }

            if (!string.IsNullOrEmpty(roleId))
            {
                //users = users.Where(s => s.Email.ToLower().Contains(searchterm.ToLower()) || s.UserName.ToLower().Contains(searchterm.ToLower()));
            }
            

            var skip = (page - 1) * recordSize;


            return users.OrderBy(s => s.Email).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchUsersCount(string searchterm, string roleId)
        {
            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchterm))
            {
                users = users.Where(s => s.Email.ToLower().Contains(searchterm.ToLower()) || s.UserName.ToLower().Contains(searchterm.ToLower()));
            }

            if (!string.IsNullOrEmpty(roleId))
            {
                //users = users.Where(s => s.Email.ToLower().Contains(searchterm.ToLower()) || s.UserName.ToLower().Contains(searchterm.ToLower()));
            }

            return users.Count();
        }
        #endregion
        [HttpGet]
        public async Task<ActionResult> Action(string id)
        {
            UsersActionViewModel model = new UsersActionViewModel();

            if (!string.IsNullOrEmpty(id))
            {
                var user = await UserManager.FindByIdAsync(id);

                model.Id = user.Id;
                model.FullName = user.FullName;
                model.Email = user.Email;
                model.UserName = user.UserName;
                model.Country = user.Country;
                model.City = user.City;
                model.Address = user.Address;
            }

            //model.Roles = accomodationPackagesService.GetAllAccomodationPackages();

            return PartialView("_Action", model);
        }

        [HttpPost]
        public async Task<JsonResult> Action(UsersActionViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.Id))
            {
                var user = await UserManager.FindByIdAsync(model.Id);

                user.Id = model.Id;
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;

                result = await UserManager.UpdateAsync(user);
            }
            else
            {
                var user = new HMSUser();
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;

                result = await UserManager.CreateAsync(user);

            }

            jsonResult.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };

            return jsonResult;
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            UsersActionViewModel model = new UsersActionViewModel();

            var user = await UserManager.FindByIdAsync(id);

            model.Id = user.Id;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(UsersActionViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.Id))
            {
                var user = await UserManager.FindByIdAsync(model.Id);

                result = await UserManager.DeleteAsync(user);

                jsonResult.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            }
            else
                jsonResult.Data = new { Success = false, Message = "Invalid User." };


            return jsonResult;
        }
    }
}