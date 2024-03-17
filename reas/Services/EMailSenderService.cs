using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using reas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace reas.Services
{
    public class EMailSenderService : IEMailSenderService
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EMailSenderService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public void SendEmail(string toEmail, string subject, string messageBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("REAS", _emailConfiguration.From));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = messageBody };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, SecureSocketOptions.StartTls);
                client.Authenticate(_emailConfiguration.Username, _emailConfiguration.Password);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
