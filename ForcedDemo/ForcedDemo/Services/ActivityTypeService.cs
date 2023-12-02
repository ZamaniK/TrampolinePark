using ForcedDemo.Entities;
using ForcedDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForcedDemo.Services
{
    public class ActivityTypeService
    {
        public IEnumerable<ActivityType> GetAllActivityTypes()
        {
            var context = new ApplicationDbContext();
            return context.ActivityTypes.ToList();
        }

        public IEnumerable<ActivityType> SearchActivityTypes(string searchTerm)
        {
            var context = new ApplicationDbContext();
            var activityTypes = context.ActivityTypes.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                activityTypes = activityTypes.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return activityTypes.ToList();
        }

        public ActivityType GetActivityTypeByID(int ID)
        {
            var context = new ApplicationDbContext();

            return context.ActivityTypes.Find(ID);
        }

        public bool SaveActivityType(ActivityType activityType)
        {
            var context = new ApplicationDbContext();

            context.ActivityTypes.Add(activityType);
            return context.SaveChanges() > 0;
        }

        public bool UpdateActivityType(ActivityType activityType)
        {
            var context = new ApplicationDbContext();

            context.Entry(activityType).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges() > 0;
        }

        public bool DeleteActivityType(ActivityType activityType)
        {
            var context = new ApplicationDbContext();

            context.Entry(activityType).State = System.Data.Entity.EntityState.Deleted;
            return context.SaveChanges() > 0;
        }
    }
}