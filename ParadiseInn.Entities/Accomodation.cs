using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParadiseInn.Entities
{
    public class Accomodation
    {
        [Key]
        public int Id { get; set; }

        public string AccomodationName { get; set; }
        public string Description { get; set; }


        [ForeignKey("AccomodationPackage")]
        public int AccomodationPackageId { get; set; }
        public virtual AccomodationPackage AccomodationPackage { get; set; }

        public ICollection<AccomodationPicture> AccomodationPictures { get; set; }
    }
}
