using ParadiseInn.Entities;
using ParadiseInn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParadiseInn.Areas.Dashboard.ViewModel
{
    public class AllAccomodationViewModel
    {
        public IEnumerable<Accomodation> Accomodations { get; set; }
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
        public string searchterm { get; set; }
        public int? accomodationPackageId { get; set; }

        public Pager Pager { get; set; }
    }

    public class AccomodationActionViewModel
    {
        public int Id { get; set; }
        public string AccomodationName { get; set; }
        public string Description { get; set; }
        public int AccomodationPackageId { get; set; }

        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
    }
}