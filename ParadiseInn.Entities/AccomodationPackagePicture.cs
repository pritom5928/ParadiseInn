using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParadiseInn.Entities
{
    public class AccomodationPackagePicture
    {
        [Key]
        public int Id { get; set; }
        
        public int AccomodationPackageId { get; set; }

        [ForeignKey("Picture")]
        public int PictureId { get; set; }
        public virtual Picture Picture { get; set; }
    }
}
