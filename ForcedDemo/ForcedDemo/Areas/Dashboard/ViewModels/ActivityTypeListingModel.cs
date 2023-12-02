using ForcedDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForcedDemo.Areas.Dashboard.ViewModels
{
    public class ActivityTypeListingModel
    {
        public IEnumerable<ActivityType> ActivityTypes { get; set; }
        public string SearchTerm { get; set; }
    }

    public class ActivityTypeActionModel
    {
        public int ID { get; set; }

        public string Name { get; set; } //Gym, Holiday Camp

        public string Description { get; set; }
    }
}