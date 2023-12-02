using ForcedDemo.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ForcedDemo.Models.ActBooking
{
    [Table("ActivityTimes1")]

    public class ActivityTime
    {
        [Key]
        public int ActivityTimeId { get; set; }
        public int ActivitiesId { get; set; }
        public virtual Activvity Activities { get; set; }
        [Display(Name = "Time Slot")]
        public int ActivityTimesId { get; set; }
        public virtual ActivityTimes ActivityTimes { get; set; }
    }
}