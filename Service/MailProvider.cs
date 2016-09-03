using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MailProvider
    {
        public void SendMailToUser( User user)
        {
            try
            {
                var fromAddress = new MailAddress("aleksandra.novkova.test@gmail.com");
                var fromPassword = "Aleksandra!6123";
                var toAddress = new MailAddress(user.Email.ToString());

                string subject = "Нов корисник";
                string body = "Bodi";

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })


                    smtp.Send(message);
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public void SuccessReservation(Rent rent)
        {
            try
            {
                var fromAddress = new MailAddress("aleksandra.novkova.test@gmail.com");
                var fromPassword = "Aleksandra!6123";
                var toAddress = new MailAddress("aleksandra_novkova@hotmail.com");

                string subject = "Нов корисник";
                string body = "Bodi";

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })


                    smtp.Send(message);
            }

            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
