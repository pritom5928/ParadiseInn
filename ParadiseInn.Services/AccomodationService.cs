using ParadiseInn.Data;
using ParadiseInn.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParadiseInn.Services
{
    public class AccomodationService
    {
        ProjectDbContext db = new ProjectDbContext();

        public IEnumerable<Accomodation> GetAllAccomodations()
        {
            return db.Accomodations.ToList();
        }

        public IEnumerable<Accomodation> GetAllAccomodationsByAccomodationPackage(int accomodationPackageId)
        {
            return db.Accomodations.Where(s => s.AccomodationPackageId == accomodationPackageId).ToList();
        }

        public IEnumerable<Accomodation> SearchAccomodations(string searchterm, int? accomodationPackageId, int page, int recordSize)
        {
            var allAccomodations = db.Accomodations.AsQueryable();

            if (!string.IsNullOrEmpty(searchterm))
            {
                allAccomodations = allAccomodations.Where(s => s.AccomodationName.ToLower().Contains(searchterm.ToLower()));
            }

            if (accomodationPackageId.HasValue && accomodationPackageId.Value > 0)
            {
                allAccomodations = allAccomodations.Where(s => s.AccomodationPackageId == accomodationPackageId);
            }

            var skip = (page - 1) * recordSize;


            return allAccomodations.OrderBy(s => s.AccomodationPackageId).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchAccomodationsCount(string searchterm, int? accomodationPackageId)
        {
            var allAccomodations = db.Accomodations.AsQueryable();

            if (!string.IsNullOrEmpty(searchterm))
            {
                allAccomodations = allAccomodations.Where(s => s.AccomodationName.ToLower().Contains(searchterm.ToLower()));
            }

            if (accomodationPackageId.HasValue && accomodationPackageId.Value > 0)
            {
                allAccomodations = allAccomodations.Where(s => s.AccomodationPackageId == accomodationPackageId);
            }

            return allAccomodations.Count();
        }

        public bool SaveAccomodations(Accomodation newRecord)
        {

            db.Accomodations.Add(newRecord);

            return db.SaveChanges() > 0;

        }

        public Accomodation GetAccomodationsById(int id)
        {
            var getData = db.Accomodations.Find(id);

            if (getData != null)
            {
                return getData;
            }

            return new Accomodation();
        }

        public bool UpdateAccomodations(Accomodation model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;

            return db.SaveChanges() > 0;
        }

        public bool DeleteAccomodations(Accomodation model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Deleted;

            return db.SaveChanges() > 0;
        }
    }
}
