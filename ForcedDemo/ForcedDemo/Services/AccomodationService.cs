using ForcedDemo.Entities;
using ForcedDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForcedDemo.Services
{
    public class AccomodationService
    {
        public IEnumerable<Accomodation> GetAllAccomodations()
        {
            var context = new ApplicationDbContext();
            return context.Accomodations.ToList();

        }

        public IEnumerable<Accomodation> GetAllAccomodationsByAccomodationPackage(int accomodationPackageID)
        {
            var context = new ApplicationDbContext();
            return context.Accomodations.Where(x => x.AccomodationPackageID == accomodationPackageID).ToList();
        }


        public IEnumerable<Accomodation> SearchAccomodations(string searchTerm, int? accomodationPackageID, int page, int recordSize)
        {
            var context = new ApplicationDbContext();
            var accomodations = context.Accomodations.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodations = accomodations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (accomodationPackageID.HasValue && accomodationPackageID.Value > 0)
            {
                accomodations = accomodations.Where(a => a.AccomodationPackageID == accomodationPackageID.Value);
            }

            var skip = (page - 1) * recordSize;

            return accomodations.OrderBy(x => x.AccomodationPackageID).Skip(skip).Take(recordSize).ToList();
        }


        public int SearchAccomodationsCount(string searchTerm, int? accomodationPackageID)
        {
            var context = new ApplicationDbContext();
            var accomodations = context.Accomodations.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodations = accomodations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (accomodationPackageID.HasValue && accomodationPackageID.Value > 0)
            {
                accomodations = accomodations.Where(a => a.AccomodationPackageID == accomodationPackageID.Value);
            }



            return accomodations.Count();
        }

        public Accomodation GetAccomodationByID(int ID)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Accomodations.Find(ID);
            }
        }

        public bool SaveAccomodation(Accomodation accomodations)
        {
            var context = new ApplicationDbContext();

            context.Accomodations.Add(accomodations);
            return context.SaveChanges() > 0;
        }

        public bool UpdateAccomodation(Accomodation accomodations)
        {
            var context = new ApplicationDbContext();

            context.Entry(accomodations).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges() > 0;
        }

        public bool DeleteAccomodation(Accomodation accomodations)
        {
            var context = new ApplicationDbContext();

            context.Entry(accomodations).State = System.Data.Entity.EntityState.Deleted;
            return context.SaveChanges() > 0;
        }
    }
}

