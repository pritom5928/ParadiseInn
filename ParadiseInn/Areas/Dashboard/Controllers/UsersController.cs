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
                return _roleManager ?? HttpContext.GetOwinContext().GetUserManager<HMSRolesManger>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public UsersController()
        {
        }

        public UsersController(HMSUserManager userManager, HMSSignInManager signInManager, HMSRolesManger rolesManger)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = rolesManger;
        }
        
        public async Task<ActionResult> Index(string searchterm, string roleId, int? page)
        {
            int recordSize = 5;
            page = page ?? 1;

            AllUsersViewModel model = new AllUsersViewModel();
            model.searchterm = searchterm;
            model.RoleId = roleId;
            model.Users = await SearchUsers(searchterm, roleId, page.Value, recordSize);
            model.Roles = RoleManager.Roles.ToList();

            var totalRecords = await SearchUsersCount(searchterm, roleId); 

            model.Pager = new Pager(totalRecords, page, recordSize);
            return View(model);
        }

        //public ActionResult Listing()
        //{
        //    AllAccomodationViewModel model = new AllAccomodationViewModel();
        //    model.Accomodations = accomodationService.GetAllAccomodations();

        //    return PartialView("_Listing", model);
        //}

        #region: Service related methods && helper function
        public async Task<IEnumerable<HMSUser>> SearchUsers(string searchterm, string roleId, int page, int recordSize)
        {
            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchterm))
            {
                users = users.Where(s => s.Email.ToLower().Contains(searchterm.ToLower()) || s.UserName.ToLower().Contains(searchterm.ToLower()));
            }

            if (!string.IsNullOrEmpty(roleId) && roleId != "0")
            {
                var role = await RoleManager.FindByIdAsync(roleId);
                var thisRoleUserIds = role.Users.Select(s => s.UserId).ToList();
                
                users = users.Where(s => thisRoleUserIds.Contains(s.Id));
            }

            var skip = (page - 1) * recordSize;
            return users.OrderBy(s => s.Email).Skip(skip).Take(recordSize).ToList();
        }

        public async Task<int> SearchUsersCount(string searchterm, string roleId)
        {
            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchterm))
            {
                users = users.Where(s => s.Email.ToLower().Contains(searchterm.ToLower()) || s.UserName.ToLower().Contains(searchterm.ToLower()));
            }

            if (!string.IsNullOrEmpty(roleId) && roleId != "0")
            {
                var role = await RoleManager.FindByIdAsync(roleId);
                var thisRoleUserIds = role.Users.Select(s => s.UserId).ToList();

                users = users.Where(s => thisRoleUserIds.Contains(s.Id));
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

        [HttpGet]
        public async Task<ActionResult> UserRoles(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            var UserRoleIds = user.Roles.Select(s => s.RoleId).ToList();

            UsersRolesViewModel model = new UsersRolesViewModel();

            model.UserId = id;
            model.Roles = RoleManager.Roles.Where(s => !UserRoleIds.Contains(s.Id)).ToList();

            model.UserRoles = RoleManager.Roles.Where(s => UserRoleIds.Contains(s.Id)).ToList();

            return PartialView("_UserRoles", model);
        }

        [HttpPost]
        public async Task<JsonResult> UserRoles(UsersActionViewModel model)
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

        

        [HttpPost]
        public async Task<JsonResult> UserRoleOperation(string userId, string roleId, bool isDelete = false)
        {
            JsonResult json = new JsonResult();

            var user = await UserManager.FindByIdAsync(userId);

            var role = await RoleManager.FindByIdAsync(roleId);

            if (user != null && role != null)
            {
                IdentityResult result = null;

                if (!isDelete)
                {
                     result = await UserManager.AddToRoleAsync(userId, role.Name);
                }
                else
                {
                     result = await UserManager.RemoveFromRoleAsync(userId, role.Name);
                }

                json.Data = new { Success = result.Succeeded,  Message = string.Join(",", result.Errors) };
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid operation" };
            }


            return json;
        }
    }
}