using ForcedDemo.Entities;
using ForcedDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForcedDemo.Services
{
    public class ActivityService
    {
        public IEnumerable<Activvity> GetAllActivities()
        {
            var context = new ApplicationDbContext();
            return context.Activities.ToList();
        }

        public IEnumerable<Activvity> GetAllActivitiesByActivityType(int activityTypeID)
        {
            var context = new ApplicationDbContext();
            return context.Activities.Where(x => x.ActivityTypeID == activityTypeID).ToList();
        }

        public IEnumerable<Activvity> SearchActivity(string searchTerm, int? activityTypeID, int page, int recordSize)
        {
            var context = new ApplicationDbContext();
            var activity = context.Activities.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                activity = activity.Where(a => a.ActivityName.ToLower().Contains(searchTerm.ToLower()));
            }

            if (activityTypeID.HasValue && activityTypeID.Value > 0)
            {
                activity = activity.Where(a => a.ActivityTypeID == activityTypeID.Value);
            }

            var skip = (page - 1) * recordSize;

            return activity.OrderBy(x => x.ActivityTypeID).Skip(skip).Take(recordSize).ToList();
        }


        public int SearchActivityCount(string searchTerm, int? activityID)
        {
            var context = new ApplicationDbContext();
            var activities = context.Activities.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                activities = activities.Where(a => a.ActivityName.ToLower().Contains(searchTerm.ToLower()));
            }

            if (activityID.HasValue && activityID.Value > 0)
            {
                activities = activities.Where(a => a.ActivityTypeID == activityID.Value);
            }
            return activities.Count();
        }

        public Activvity GetActivityByID(int ID)
        {
            var context = new ApplicationDbContext();
            return context.Activities.Find(ID);
        }

        public bool SaveActivity(Activvity activvity)
        {
            var context = new ApplicationDbContext();

            context.Activities.Add(activvity);
            return context.SaveChanges() > 0;
        }

        public bool UpdateActivity(Activvity activvity)
        {
            var context = new ApplicationDbContext();
            var existingActivity = context.Activities.Find(activvity.ActivitiesId);
            context.ActivityPictures.RemoveRange(existingActivity.ActivityPictures);
            context.Entry(existingActivity).CurrentValues.SetValues(activvity);
            context.ActivityPictures.AddRange(activvity.ActivityPictures);
            return context.SaveChanges() > 0;
        }

        public bool DeleteActivity(Activvity activvity)
        {
            var context = new ApplicationDbContext();

            var existingActivity = context.Activities.Find(activvity.ActivitiesId);
            context.ActivityPictures.RemoveRange(existingActivity.ActivityPictures);
            context.Entry(existingActivity).State = System.Data.Entity.EntityState.Deleted;
            return context.SaveChanges() > 0;
        }

        public List<ActivityPictures> GetPicturesByActivityID(int activityID)
        {
            var context = new ApplicationDbContext();
            return context.Activities.Find(activityID).ActivityPictures.ToList();
        }
    }
}