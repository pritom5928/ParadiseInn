using ParadiseInn.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParadiseInn.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<AccomodationType> accomodationTypes { get; set; }
        public IEnumerable<AccomodationPackage> accomodationPackages { get; set; }
    }
}