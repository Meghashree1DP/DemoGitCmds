﻿using System;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Threading.Tasks;

namespace BankOfSuccessCS.Models
{
    public class EmailNotification : INotification
    {
        public void Update(string loc)
        {
            MailMessage message = new MailMessage();
            string mailId = ConfigurationManager.AppSettings["MailId"];
            string pw = ConfigurationManager.AppSettings["Password"];
            message.From = new MailAddress(mailId);
            string mail = loc.Split(',')[0];
            message.To.Add(new MailAddress(mail));
            message.Subject = "Account Statement";
            message.Body = "PFA Your Account Statement";
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            var attachment = new Attachment(ConfigurationManager.AppSettings["StatementLogPath"]);
            message.Attachments.Add(attachment);
            Task.Run(() =>
            {
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(mailId, pw);
                    smtpClient.Send(message);
                }
            });
        }
        public void Update(Transaction t)
        {
            MailMessage message = new MailMessage();
            string mailId = ConfigurationManager.AppSettings["MailId"];
            string pw = ConfigurationManager.AppSettings["Password"];   
            message.From = new MailAddress(mailId);
            message.To.Add(new MailAddress(t.Acc.Email));
            message.Subject = "Sucessfull Transaction";
            message.Body = t.ToString();
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


            Task.Run(() =>
            {
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(mailId, pw);
                    smtpClient.Send(message);
                }
            });
        }
    }
}
