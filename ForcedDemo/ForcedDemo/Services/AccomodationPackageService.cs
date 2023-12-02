using ForcedDemo.Entities;
using ForcedDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForcedDemo.Services
{
    public class AccomodationPackageService
    {
        public IEnumerable<AccomodationPackage> GetAllAccomodationPackages()
        {
            var context = new ApplicationDbContext();
            return context.AccomodationPackages.ToList();
        }

        public IEnumerable<AccomodationPackage> GetAllAccomodationPackagesByAccomodationType(int accomodationTypeID)
        {
            var context = new ApplicationDbContext();
            return context.AccomodationPackages.Where(x => x.AccomodationTypeID == accomodationTypeID).ToList();
        }

        public IEnumerable<AccomodationPackage> SearchAccomodationPackages(string searchTerm, int? accomodationTypeID, int page, int recordSize)
        {
            var context = new ApplicationDbContext();
            var accomodationPackages = context.AccomodationPackages.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodationPackages = accomodationPackages.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (accomodationTypeID.HasValue && accomodationTypeID.Value > 0)
            {
                accomodationPackages = accomodationPackages.Where(a => a.AccomodationTypeID == accomodationTypeID.Value);
            }

            var skip = (page - 1) * recordSize;

            return accomodationPackages.OrderBy(x => x.AccomodationTypeID).Skip(skip).Take(recordSize).ToList();
        }


        public int SearchAccomodationPackagesCount(string searchTerm, int? accomodationTypeID)
        {
            var context = new ApplicationDbContext();
            var accomodationPackages = context.AccomodationPackages.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodationPackages = accomodationPackages.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (accomodationTypeID.HasValue && accomodationTypeID.Value > 0)
            {
                accomodationPackages = accomodationPackages.Where(a => a.AccomodationTypeID == accomodationTypeID.Value);
            }
            return accomodationPackages.Count();
        }

        public AccomodationPackage GetAccomodationPackageByID(int ID)
        {
            var context = new ApplicationDbContext();
            return context.AccomodationPackages.Find(ID);
        }

        public bool SaveAccomodationPackage(AccomodationPackage accomodationPackage)
        {
            var context = new ApplicationDbContext();

            context.AccomodationPackages.Add(accomodationPackage);
            return context.SaveChanges() > 0;
        }

        public bool UpdateAccomodationPackage(AccomodationPackage accomodationPackage)
        {
            var context = new ApplicationDbContext();
            var existingAccomodationPackage = context.AccomodationPackages.Find(accomodationPackage.ID);
            context.AccomodationPackagePictures.RemoveRange(existingAccomodationPackage.AccomodationPackagePictures);
            context.Entry(existingAccomodationPackage).CurrentValues.SetValues(accomodationPackage);
            context.AccomodationPackagePictures.AddRange(accomodationPackage.AccomodationPackagePictures);
            return context.SaveChanges() > 0;
        }

        public bool DeleteAccomodationPackage(AccomodationPackage accomodationPackage)
        {
            var context = new ApplicationDbContext();

            var existingAccomodationPackage = context.AccomodationPackages.Find(accomodationPackage.ID); 
            context.AccomodationPackagePictures.RemoveRange(existingAccomodationPackage.AccomodationPackagePictures);
            context.Entry(existingAccomodationPackage).State = System.Data.Entity.EntityState.Deleted;
            return context.SaveChanges() > 0;
        }

        public List<AccomodationPackagePicture> GetPicturesByAccomodationPackageID(int accomodationPackageID)
        {
            var context = new ApplicationDbContext();
            return context.AccomodationPackages.Find(accomodationPackageID).AccomodationPackagePictures.ToList();
        }
    }
}