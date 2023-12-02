using ForcedDemo.Models.PackBooking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ForcedDemo.Models.PackBooking
{
    public class PackageBooking
    {
        [Key]
        public int PackageBookingId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd - MM - yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Arrival Date")]
        public DateTime ArrivalDate { get; set; }
        [Display(Name = "No. People")]
        public int numOfHours { get; set; }
        [Display(Name = "Basic Price")]
        [DataType(DataType.Currency)]
        public decimal BasicPrice { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLastName { get; set; }
        public string Status { get; set; }
        public string AccomodationPackage { get; set; }
        public string AccomodationType { get; set; }

        [DisplayName("Time"), DataType(DataType.Time)]
        public DateTime TimeSlot { get; set; }
        public int PackageTimeId { get; set; }
        public virtual PackageTime PackageTime { get; set; }
        ApplicationDbContext context = new ApplicationDbContext();

        //Activity Price
        public decimal getActivityPrice()
        {
            var id = (from a in context.PackageTimes1
                      where a.PackageTimeId == PackageTimeId
                      select a.AccomodationPackageId).FirstOrDefault();

            var Price = (from a in context.AccomodationPackages
                         where a.ID == id
                         select a.FeePerDay).FirstOrDefault();
            return (Price);
        }
        //Package 
        public string getPackage()
        {
            var id = (from a in context.PackageTimes1
                      where a.PackageTimeId == PackageTimeId
                      select a.AccomodationPackageId).FirstOrDefault();

            var Atype = (from a in context.AccomodationPackages
                         where a.ID == id
                         select a.Name).FirstOrDefault();
            return (Atype);
        }
        //Package Type
        public string getPackageType()
        {
            var id = (from a in context.PackageTimes1
                      where a.PackageTimeId == PackageTimeId
                      select a.AccomodationPackageId).FirstOrDefault();

            var Atype = (from a in context.AccomodationPackages
                         where a.ID == id
                         select a.AccomodationType.Name).FirstOrDefault();
            return (Atype);
        }
        //Time Slot
        public DateTime getTimeSlot()
        {
            var id = (from a in context.PackageTimes1
                      where a.PackageTimeId == PackageTimeId
                      select a.AccomodationPackageId).FirstOrDefault();

            var time = (from a in context.PackageTimes
                        where a.PackageTimesId == id
                        select a.SlotTime).FirstOrDefault();
            return time;
        }
        //Customer Name

        public string getCustomerName(string CusEmail)
        {
            var Fname = (from c in context.Users
                         where c.Email == CusEmail
                         select c.Email).FirstOrDefault();
            return (Fname);
        }
        //Customer Last Name
        public string getCustomerLastName(string CusEmail)
        {
            var Lname = (from c in context.Users
                         where c.Email == CusEmail
                         select c.Email).FirstOrDefault();
            return (Lname);
        }

        public bool CheckBooking(DateTime datet)
        {
            bool result = false;
            //var datee = context.ActivityBookings.Where(x => x.ArrivalDate == ArrivalDate).Count();
            var datee = context.PackageBookings.Where(x => x.TimeSlot == datet).Count();
            if (datee > 1)
            {
                result = true;
            }
            return result;
        }
        public bool CheckBooking(PackageBooking booking)
        {
            bool result = false;
            var dbRecords = context.PackageBookings.Where(x => x.ArrivalDate == booking.ArrivalDate).ToList();
            foreach (var item in dbRecords)
            {
                if (booking.PackageTimeId == booking.PackageTimeId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public static void SendEmail(PackageBooking booking)
        {
            var mailTo = new List<MailAddress>();
            mailTo.Add(new MailAddress(booking.CustomerEmail));

            var body = $"Good Day {booking.CustomerName} {booking.CustomerLastName}, " + "<br/>" +
                $"You booked: {booking.AccomodationPackage}" + "<br/>" +
                $"Date: {booking.ArrivalDate}" + "<br/>" +
                $"Time: {booking.TimeSlot}" + "<br/>" +
                $"Cost: {booking.BasicPrice}";

            ForcedDemo.Models.EmailService emailService = new ForcedDemo.Models.EmailService();
            emailService.SendEmail(new EmailContent()
            {
                mailTo = mailTo,
                mailCc = new List<MailAddress>(),
                mailSubject = "Booking Confirmation!!  | Ref No.:" + booking.PackageBookingId,
                mailBody = body,
                mailFooter = $"<br/> Kind Regards, <br/> <b>Activity Park  </b>",
                mailPriority = MailPriority.High,
                mailAttachments = new List<Attachment>()

            });
        }
    }
}
