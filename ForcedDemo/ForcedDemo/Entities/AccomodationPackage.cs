using ForcedDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForcedDemo.Entities
{
    public class AccomodationPackage
    {
        public int ID { get; set; }

        public int AccomodationTypeID { get; set; }
        public virtual AccomodationType AccomodationType { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string NoOfPeople { get; set; } //Capacity of that accomodation... rename to "Capacity" later.

        public decimal FeePerDay { get; set; }

        public virtual List<AccomodationPackagePicture> AccomodationPackagePictures { get; set; }
        public virtual List<AppointmentModel> Appointments { get; set; }

    }
}