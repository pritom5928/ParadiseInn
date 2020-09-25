using ParadiseInn.Data;
using ParadiseInn.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParadiseInn.Services
{
    public class DashboardService
    {

        public bool SavePicture(Picture picture)
        {
            ProjectDbContext db = new ProjectDbContext();

            db.Pictures.Add(picture);

            return db.SaveChanges() > 0;
        }


        public IEnumerable<Picture> GetPicturesByIds(List<int> pictureIds)
        {
            ProjectDbContext db = new ProjectDbContext();

            return  pictureIds.Select(s => db.Pictures.Find(s)).ToList();
        }

    }
}
