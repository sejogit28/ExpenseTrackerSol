using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailSender(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public void SendEmail(EmailMessage emailMessage) 
        {
            var Message = CreateEmailMessage(emailMessage);
            Send(Message);
        }

        private MimeMessage CreateEmailMessage(EmailMessage emailMessage) 
        {
            var eMessage = new MimeMessage();
            eMessage.From.Add(new MailboxAddress(_emailConfiguration.From));
            eMessage.To.AddRange(emailMessage.To);
            eMessage.Subject = emailMessage.Subject;
            eMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = emailMessage.Content };

            return eMessage;
        }

        private void Send(MimeMessage mimeMailMessage) 
        {
            using (var client = new SmtpClient()) 
            {
                try
                {
                    client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfiguration.Username, _emailConfiguration.Password);

                    client.Send(mimeMailMessage);
                }
                catch 
                {

                    throw;
                }
                finally 
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
