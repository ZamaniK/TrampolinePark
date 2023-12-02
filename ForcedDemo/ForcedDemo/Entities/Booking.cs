using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForcedDemo.Entities
{
    public class Booking
    {
        public int ID { get; set; }


        public int AccomodationID { get; set; }
        public Accomodation Accomodation { get; set; }

        public DateTime FromDate { get; set; }

        public string Duration { get; set; }
    }
}