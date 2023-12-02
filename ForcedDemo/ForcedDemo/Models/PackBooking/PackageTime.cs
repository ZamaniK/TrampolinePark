using ForcedDemo.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForcedDemo.Models.PackBooking
{
    public class PackageTime
    {
        [Key]
        public int PackageTimeId { get; set; }
        public int AccomodationPackageId { get; set; }
        public virtual AccomodationPackage AccomodationPackages { get; set; }
        [Display(Name = "Time Slot")]
        public int PackageTimesId { get; set; }
        public virtual PackageTimes PackageTimes { get; set; }
    }
}