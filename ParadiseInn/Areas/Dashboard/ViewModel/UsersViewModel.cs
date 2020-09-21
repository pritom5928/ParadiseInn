using Microsoft.AspNet.Identity.EntityFramework;
using ParadiseInn.Entities;
using ParadiseInn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParadiseInn.Areas.Dashboard.ViewModel
{
    public class AllUsersViewModel
    {
        public IEnumerable<HMSUser> Users { get; set; }
        public string searchterm { get; set; }

        public string RoleId { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }

        public Pager Pager { get; set; }
    }

    public class UsersActionViewModel
    {
        public string Id { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        //public string RoleId { get; set; }
        //public IdentityRole Role { get; set; }
        
        //public IEnumerable<IdentityRole> Roles { get; set; }
    }



    public class UsersRolesViewModel
    {
        public string UserId { get; set; }

        public IEnumerable<IdentityRole> UserRoles { get; set; }

       public IEnumerable<IdentityRole> Roles { get; set; }
    }
}