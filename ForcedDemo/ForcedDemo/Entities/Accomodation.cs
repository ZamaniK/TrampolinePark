using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForcedDemo.Entities
{
    public class Accomodation
    {
        public int ID { get; set; }

        public int AccomodationPackageID { get; set; }
        public virtual AccomodationPackage AccomodationPackage { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PictureURL { get; set; }

        public List<AccomodationPicture> AccomodationPictures { get; set; }
    }
}