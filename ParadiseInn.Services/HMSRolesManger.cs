using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ParadiseInn.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParadiseInn.Services
{
    public class HMSRolesManger : RoleManager<IdentityRole>
    {

        public HMSRolesManger(IRoleStore<IdentityRole, string> roleStore) : base(roleStore)
        {

        }

        public static HMSRolesManger Create(IdentityFactoryOptions<HMSRolesManger> options, IOwinContext context)
        {
            return new HMSRolesManger(new RoleStore<IdentityRole>(context.Get<ProjectDbContext>()));
        }
    }
}
