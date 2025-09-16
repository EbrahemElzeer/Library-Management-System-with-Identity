using ApplicationCore.Interfaces;
using Castle.Core.Smtp;
using Library_Managment_System.config;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Library_Managment_System.Services
{
    public class EmailService:IEmailService
    {
        private readonly EmailSettings options;

        public EmailService(IOptions<EmailSettings> options) {
            this.options = options.Value;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
        {

            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(options.Email));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = htmlBody };

            using var smpt = new SmtpClient();
            await smpt.ConnectAsync(options.Host,options.Port, SecureSocketOptions.StartTls);
            await smpt.AuthenticateAsync(options.Email,options.Password);
            await smpt.SendAsync(message);
            await smpt.DisconnectAsync(true);
        }




        }
}
