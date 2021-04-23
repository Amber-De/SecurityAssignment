using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication1.Email
{
    public class EmailHelper
    {
        public bool SendEmail(string userEmail, string confirmationLink, string studentPassword)
        {
          
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("amberdebono10@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Confirm your email";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = confirmationLink + " /nStudent Password: " + studentPassword;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("amberdebono10@gmail.com", "nicegirl01");
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }
    }
}
