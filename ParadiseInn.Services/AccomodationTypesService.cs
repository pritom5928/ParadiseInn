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
            return db.AccomodationTypes.AsEnumerable();
        }

        public bool SaveAccomodationType(AccomodationType newRecord)
        {

            db.AccomodationTypes.Add(newRecord);

            return db.SaveChanges() > 0;
           
        }
    }
}
