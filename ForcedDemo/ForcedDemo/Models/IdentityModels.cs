using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using ForcedDemo.Entities;
using ForcedDemo.Models.ActBooking;
using ForcedDemo.Models.PackBooking;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ForcedDemo.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Accomodation> Accomodations { get; set; }
        public DbSet<AccomodationPicture> AccomodationPictures { get; set; }
        public DbSet<AccomodationPackage> AccomodationPackages { get; set; }
        public DbSet<AccomodationPackagePicture> AccomodationPackagePictures { get; set; }
        public DbSet<AccomodationType> AccomodationTypes { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<VenueModel> VenueModels { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<PageDTO> Pages { get; set; }
        public DbSet<SidebarDTO> Sidebar { get; set; }
        public DbSet<CategoryDTO> Categories { get; set; }
        public DbSet<ProductDTO> Products { get; set; }
        public DbSet<OrderDTO> Orders { get; set; }
        public DbSet<AppointmentModel> Appointments { get; set; }
        public DbSet<OrderDetailsDTO> OrderDetails { get; set; }
        public DbSet<AdministrationModel> Administrations { get; set; }
        public DbSet<Activvity> Activities { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<ActivityPictures> ActivityPictures { get; set; }
        public DbSet<PackageBooking> PackageBookings { get; set; }
        public DbSet<PackageTime> PackageTimes1 { get; set; }
        public DbSet<PackageTimes> PackageTimes { get; set; }
        public DbSet<ActivityBooking> ActivityBookings { get; set; }
        public DbSet<ActivityTime> ActivityTimes1 { get; set; }
        public DbSet<ActivityTimes> ActivityTimes { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}