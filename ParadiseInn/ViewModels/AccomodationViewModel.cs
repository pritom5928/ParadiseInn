using ParadiseInn.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParadiseInn.ViewModels
{
    public class AccomodationViewModel
    {
        public int? selectedAccomodationPackageId { get; set; }

        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
        public IEnumerable<Accomodation> Accomodations { get; set; }

        public AccomodationType AccomodationType { get; set; }
    }

    public class AccomodationPackageDetailsViewModel
    {
        public AccomodationPackage AccomodationPackage { get; set; }
    }
}