using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForcedDemo.Entities
{
    public class VenueModel
    {
        public int ID { get; set; }

        public string RoomNo { get; set; }

        public string Details { get; set; }

        public bool HasBath { get; set; }

        public int AccomodationTypeID { get; set; }
        public AccomodationType AccomodationType { get; set; }
    }
}