using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForcedDemo.Models.ActBooking
{
    public class ActivityTimes
    {
        [Key]
        public int ActivityTimesId { get; set; }
        [DisplayName("Time"), DataType(DataType.Time)]
        public DateTime SlotTime { get; set; }
    }
}