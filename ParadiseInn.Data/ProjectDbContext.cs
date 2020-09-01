using Microsoft.AspNet.Identity.EntityFramework;
using ParadiseInn.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParadiseInn.Data
{
    public class ProjectDbContext : IdentityDbContext<HMSUser>
    {
        public ProjectDbContext() : base("ParadiseInnHMSConnectionString")
        {
        }
        
        public static ProjectDbContext Create()
        {
            return new ProjectDbContext();
        }

        public DbSet<AccomodationType> AccomodationTypes { get; set; }
        public DbSet<AccomodationPackage> AccomodationPackages { get; set; }
        public DbSet<Accomodation> Accomodations { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
