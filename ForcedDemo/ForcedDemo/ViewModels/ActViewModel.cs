using ForcedDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForcedDemo.ViewModels
{
    public class ActViewModel
    {
        public IEnumerable<ActivityType> ActivityTypes { get; set; }
        public IEnumerable<Activvity> Activities { get; set; }
    }
}