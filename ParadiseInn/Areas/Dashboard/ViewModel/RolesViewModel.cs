using Microsoft.AspNet.Identity.EntityFramework;
using ParadiseInn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParadiseInn.Areas.Dashboard.ViewModel
{
    public class AllRolesViewModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; }
        public string searchterm { get; set; }
        

        public Pager Pager { get; set; }
    }


    public class RolesActionViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }


        //public string RoleId { get; set; }
        //public IdentityRole Role { get; set; }

        //public IEnumerable<IdentityRole> Roles { get; set; }
    }
}