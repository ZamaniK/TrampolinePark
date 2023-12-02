using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForcedDemo.Models.PackBooking
{
    public class PackageTimes
    {
        [Key]
        public int PackageTimesId { get; set; }
        [DisplayName("Time"), DataType(DataType.Time)]
        public DateTime SlotTime { get; set; }
    }
}