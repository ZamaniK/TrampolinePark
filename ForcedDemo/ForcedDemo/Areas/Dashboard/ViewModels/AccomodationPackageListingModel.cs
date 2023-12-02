using ForcedDemo.Entities;
using ForcedDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForcedDemo.Areas.Dashboard.ViewModels
{
    public class AccomodationPackageListingModel
    {
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
        public int? AccomodationTypeID { get; set; }

        public Pager Pager { get; set; }
    }

    public class AccomodationPackageActionModel
    {
        public int ID { get; set; }

        public int AccomodationTypeID { get; set; }
        public AccomodationType AccomodationType { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string NoOfPeople { get; set; } //Capacity of that accomodation... rename to "Capacity" later.
        public decimal FeePerDay { get; set; }
        public string PictureIDs { get; set; }

        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
        public List<AccomodationPackagePicture> AccomodationPackagePictures { get; set; }
    }
}