using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Mail;
using ProfessionalEvaluation.TO;
using System.Net;
using System.Configuration;

namespace ProfessionalEvaluation.Utilities
{
    public class Mail
    {
        private const string EmailAccountSetting = "FromEmailAccount";
        private const string EmailAccountNameSetting = "FromEmailAccountName";
        private const string EmailAccountPasswordSetting = "FromEmailAccountPassword";
        private const string MailHostSetting = "MailHost"; 

        private MailMessageTO message;

        public Mail(MailMessageTO message)
        {
            this.message = message;
        }

        public void Send()
        {
            var fromAddress = new MailAddress(ConfigurationManager.AppSettings[EmailAccountSetting], ConfigurationManager.AppSettings[EmailAccountNameSetting]);
            var toAddress = new MailAddress(message.ToEmail, message.ToName);

            var smtp = new SmtpClient
            {
                Host = ConfigurationManager.AppSettings[MailHostSetting],
                Port = 25,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true
            };

            var mm = new MailMessage(fromAddress, toAddress)
            {
                Subject = message.Title,
                Body = message.Body,
                IsBodyHtml = true
            };

            if (message.Attachements != null && message.Attachements.Count > 0)
	        {
                foreach (var item in message.Attachements)
                {
                    mm.Attachments.Add(new Attachment(item));
                }
	        }

            smtp.Send(mm);
        }
    }
}
