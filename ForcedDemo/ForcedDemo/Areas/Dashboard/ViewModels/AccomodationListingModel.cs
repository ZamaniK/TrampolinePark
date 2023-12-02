using ForcedDemo.Entities;
using ForcedDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForcedDemo.Areas.Dashboard.ViewModels
{
    public class AccomodationListingModel
    {

        public IEnumerable<Accomodation> Accomodations { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
        public int? AccomodationPackageID { get; set; }

        public Pager Pager { get; set; }
    }

    public class AccomodationActionModel
    {

        public int ID { get; set; }

        public int AccomodationPackageID { get; set; }
        public AccomodationPackage AccomodationPackage { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }

    }
}

