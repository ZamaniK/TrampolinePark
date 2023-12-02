using ForcedDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForcedDemo.ViewModels
{
    public class EventViewModel
    {
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
    }
}