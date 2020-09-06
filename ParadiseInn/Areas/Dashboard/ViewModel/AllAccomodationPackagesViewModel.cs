using ParadiseInn.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParadiseInn.Areas.Dashboard.ViewModel
{
    public class AllAccomodationPackagesViewModel
    {
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
        public string searchterm { get; set; }
        public int? accomodationTypeId { get; set; }
    }

    public class AccomodationPackagesActionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NoOfRoom { get; set; }
        public decimal FeePerNight { get; set; }
        public int AccomodationTypeId { get; set; }

        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
    }
}