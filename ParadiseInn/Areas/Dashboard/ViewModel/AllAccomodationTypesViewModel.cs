using ParadiseInn.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParadiseInn.Areas.Dashboard.ViewModel
{
    public class AllAccomodationTypesViewModel
    {
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
    }
    public class AccomodationTypesActionViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

}