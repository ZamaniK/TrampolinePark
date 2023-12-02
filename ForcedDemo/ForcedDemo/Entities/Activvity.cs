using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForcedDemo.Entities
{
    public class Activvity
    {
        [Key]
        public int ActivitiesId { get; set; }
        [Required, DisplayName("Activity Name")]
        public string ActivityName { get; set; }
        [Required, DisplayName("Description")]
        public int ActivityTypeID { get; set; }
        public virtual ActivityType ActivityType { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Currency), DisplayName("Activity Price")]
        public decimal ActivityPrice { get; set; }
        public int Capacity { get; set; }

        public virtual List<ActivityPictures> ActivityPictures { get; set; }

    }
}