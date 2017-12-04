using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;


namespace MyCVLtd
{
    public class MyCVConfig
    {
        public static bool MailFunction(string body, string recepient, string subject)
        {
            bool x = false;

            try
            {
                const string fromAddress = "noreply.mycvltd@gmail.com";
                string toAddress = recepient;
                var mail = new MailMessage();
                mail.To.Add(toAddress);
                mail.Subject = subject;
                mail.From = new MailAddress(fromAddress);
                mail.Body = body;
                mail.IsBodyHtml = true;
                var client = new SmtpClient
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("noreply.mycvltd@gmail.com", "mycvltd123"),
                    Port = 587,
                    Host = "smtp.gmail.com",
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true
                };
                client.Send(mail);
                x = true;
            }
            catch (Exception ex2)
            {
                ex2.Data.Clear();
            }
            return x;
        }
    }
}