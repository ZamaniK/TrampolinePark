using ForcedDemo.Entities;
using ForcedDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForcedDemo.Areas.Dashboard.ViewModels
{
    public class ActivityListingModel
    {
        public IEnumerable<Activvity> Activities { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<ActivityType> ActivityTypes { get; set; }
        public int? ActivityTypeID { get; set; }

        public Pager Pager { get; set; }
    }

    public class ActivityActionModel
    {
        public int ID { get; set; }

        public int ActivityTypeID { get; set; }
        public ActivityType ActivityType { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int NoOfPeople { get; set; } //Capacity of that accomodation... rename to "Capacity" later.
        public decimal FeePerDay { get; set; }
        public string PictureIDs { get; set; }

        public IEnumerable<ActivityType> ActivityTypes { get; set; }
        public List<ActivityPictures> ActivityPictures { get; set; }
    }
}