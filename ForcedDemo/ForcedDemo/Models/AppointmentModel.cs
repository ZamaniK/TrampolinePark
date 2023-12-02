using ForcedDemo.Entities;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ForcedDemo.Models
{
    public class AppointmentModel : IComparable<AppointmentModel>
    {
        [Key]
        public int AppointmentID { get; set; }



        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int AccomodationPackageId { get; set; }
        public virtual AccomodationPackage Accomodation { get; set; }

        public string TimeBlockHelper { get; set; }

        [Required]
        [Display(Name = "Date for Appointment")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [MyAppointmentDateValidation(ErrorMessage = "Are you creating an appointment for the past?")]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        public int CompareTo(AppointmentModel other)
        {
            return this.Date.Date.Add(this.Time.TimeOfDay).CompareTo(other.Date.Date.Add(other.Time.TimeOfDay));
        }

        public class Email
        {
            //overload with what you want to show in the email.
            public void SendConfirmation(string email, string Name, DateTime Date, DateTime Time)
            {
                try
                {
                    var myMessage = new SendGridMessage
                    {
                        From = new EmailAddress("no-reply@homify.co.za", "EM System")
                    };


                    myMessage.AddTo(email);
                    string subject = "Created Appointment: ";
                    string body = (
                        "Dear " + Name + "<br/>"
                        + "<br/>"
                        + "Please find below your details of your recent Appointment: "
                        + "<br/>"
                        + "<br/>" + "Appointment Date     :" + Date
                        + "<br/>" + "Appointment Time     :" + Time +
                        "<br/>" +
                        "<br/>" +
                        "<br/>" +

                        "Sincerely Yours, " +
                        "<br/>" +
                        "Sizanani NetCare ");

                    myMessage.Subject = subject;
                    myMessage.HtmlContent = body;

                    var transportWeb = new SendGrid.SendGridClient("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");

                    transportWeb.SendEmailAsync(myMessage);
                }
                catch (Exception d)
                {
                    Console.WriteLine(d);
                }

            }
        }
    } 
        }