using ParadiseInn.Data;
using ParadiseInn.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParadiseInn.Services
{
    public class AccomodationPackagesService
    {
        ProjectDbContext db = new ProjectDbContext();

        public IEnumerable<AccomodationPackage> GetAllAccomodationPackages()
        {
            return db.AccomodationPackages.ToList();
        }

        public IEnumerable<AccomodationPackage> SearchAccomodationPackages(string searchterm, int? accomodationTypeId)
        {
            var allAccomodationPackages = db.AccomodationPackages.AsQueryable();

            if (!string.IsNullOrEmpty(searchterm))
            {
                allAccomodationPackages = allAccomodationPackages.Where(s => s.Name.ToLower().Contains(searchterm.ToLower()));
            }

            if (accomodationTypeId.HasValue && accomodationTypeId.Value > 0)
            {
                allAccomodationPackages = allAccomodationPackages.Where(s => s.AccomodationTypeId == accomodationTypeId);
            }

            return allAccomodationPackages.ToList();
        }

        public bool SaveAccomodationPackage(AccomodationPackage newRecord)
        {

            db.AccomodationPackages.Add(newRecord);

            return db.SaveChanges() > 0;

        }

        public  AccomodationPackage GetAccomodationPackageById(int id)
        {
            var getData = db.AccomodationPackages.Find(id);

            if (getData != null)
            {
                return getData;
            }

            return new AccomodationPackage();
        }

        public bool UpdateAccomodationPackage(AccomodationPackage model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;

            return db.SaveChanges() > 0;
        }

        public bool DeleteAccomodationPackage(AccomodationPackage model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Deleted;

            return db.SaveChanges() > 0;
        }
    }
}
