using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParadiseInn.Entities
{
    public class AccomodationPackage
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public int NoOfRoom { get; set; }
        public decimal FeePerNight { get; set; }

        [ForeignKey("AccomodationType")]
        public int AccomodationTypeId { get; set; }
        public virtual AccomodationType AccomodationType { get; set; }
        
    }
}
