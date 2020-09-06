using ParadiseInn.Data;
using ParadiseInn.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParadiseInn.Services
{
    public class AccomodationTypesService
    {

        ProjectDbContext db = new ProjectDbContext();

        public IEnumerable<AccomodationType> GetAllAccomodationTypes()
        {
            return db.AccomodationTypes.ToList();
        }

        public IEnumerable<AccomodationType> SearchAccomodationTypes(string searchterm)
        {
            var allAccomodationsType = db.AccomodationTypes.AsQueryable();

            if (!string.IsNullOrEmpty(searchterm))
            {
                allAccomodationsType = allAccomodationsType.Where(s => s.Name.ToLower().Contains(searchterm.ToLower()));
            }

            return allAccomodationsType.ToList();
        }

        public bool SaveAccomodationType(AccomodationType newRecord)
        {

            db.AccomodationTypes.Add(newRecord);

            return db.SaveChanges() > 0;
           
        }

        public AccomodationType GetAccomodationTypeById(int id)
        {
            var getData = db.AccomodationTypes.Find(id);

            if (getData != null)
            {
                return getData;
            }

            return new AccomodationType();
        }

        public bool UpdateAccomodationType(AccomodationType model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;

            return db.SaveChanges() > 0;
        }

        public bool DeleteAccomodationType(AccomodationType model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Deleted;

            return db.SaveChanges() > 0;
        }
    }
}
