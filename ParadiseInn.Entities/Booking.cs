using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParadiseInn.Entities
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        public DateTime FromDate { get; set; }

        /// <summary>
        /// Number of stay nights
        /// </summary>
        public int Duration { get; set; }
        
        [ForeignKey("Accomodation")]
        public int AccomodationId { get; set; }
        public virtual Accomodation Accomodation { get; set; }
    }
}
