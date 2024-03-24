
using MailKit.Security;
using MimeKit;
using BusinessObject.Model;

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
