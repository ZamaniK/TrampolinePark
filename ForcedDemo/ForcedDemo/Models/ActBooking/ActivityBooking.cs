using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ForcedDemo.Models.ActBooking
{
    public class ActivityBooking
    {
        [Key]
        public int ActivvityBookingId { get; set; }
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
        public string ActivityType { get; set; }
        [DisplayName("Time"), DataType(DataType.Time)]
        public DateTime TimeSlot { get; set; }
        public int ActivityTimeId { get; set; }
        public virtual ActivityTime ActivityTime { get; set; }
        ApplicationDbContext context = new ApplicationDbContext();

        //Activity Price
        public decimal getActivityPrice()
        {
            var id = (from a in context.ActivityTimes1
                      where a.ActivityTimeId == ActivityTimeId
                      select a.ActivitiesId).FirstOrDefault();

            var Price = (from a in context.Activities
                         where a.ActivitiesId == id
                         select a.ActivityPrice).FirstOrDefault();
            return (Price);
        }
        //Activity Type
        public string getActivityType()
        {
            var id = (from a in context.ActivityTimes1
                      where a.ActivityTimeId == ActivityTimeId
                      select a.ActivitiesId).FirstOrDefault();

            var Atype = (from a in context.Activities
                         where a.ActivitiesId == id
                         select a.ActivityName).FirstOrDefault();
            return (Atype);
        }
        //Time Slot
        public DateTime getTimeSlot()
        {
            var id = (from a in context.ActivityTimes1
                      where a.ActivityTimeId == ActivityTimeId
                      select a.ActivityTimesId).FirstOrDefault();

            var time = (from a in context.ActivityTimes
                        where a.ActivityTimesId == id
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
            var datee = context.ActivityBookings.Where(x => x.TimeSlot == datet).Count();
            if (datee > 1)
            {
                result = true;
            }
            return result;
        }
        public bool CheckBooking(ActivityBooking booking)
        {
            bool result = false;
            var dbRecords = context.ActivityBookings.Where(x => x.ArrivalDate == booking.ArrivalDate).ToList();
            foreach (var item in dbRecords)
            {
                if (booking.ActivityTimeId == booking.ActivityTimeId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public static void SendEmail(ActivityBooking booking)
        {
            var mailTo = new List<MailAddress>();
            mailTo.Add(new MailAddress(booking.CustomerEmail));

            var body = $"Good Day {booking.CustomerName} {booking.CustomerLastName}, " + "<br/>" +
                $"You booked: {booking.ActivityType}" + "<br/>" +
                $"Date: {booking.ArrivalDate}" + "<br/>" +
                $"Time: {booking.TimeSlot}" + "<br/>" +
                $"Cost: {booking.BasicPrice}";

            ForcedDemo.Models.EmailService emailService = new ForcedDemo.Models.EmailService();
            emailService.SendEmail(new EmailContent()
            {
                mailTo = mailTo,
                mailCc = new List<MailAddress>(),
                mailSubject = "Booking Confirmation!!  | Ref No.:" + booking.ActivvityBookingId,
                mailBody = body,
                mailFooter = $"<br/> Kind Regards, <br/> <b>Activity Park  </b>",
                mailPriority = MailPriority.High,
                mailAttachments = new List<Attachment>()

            });
        }
    }
}