using LoanApplictationApp.Models;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace LoanApplictationApp.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IOptions<SmtpSettings> smtpSetting;
        public SmtpEmailSender(IOptions<SmtpSettings> smtpSetting)
        {
            this.smtpSetting = smtpSetting;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MailMessage
            {
                From = new MailAddress(smtpSetting.Value.User), // Sender email address
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true // Set to true if your message is HTML formatted
            };
            message.To.Add(email); // Recipient email address

            using (var emailClient = new SmtpClient(smtpSetting.Value.Host, smtpSetting.Value.Port))
            {
                emailClient.Credentials = new NetworkCredential(
                    smtpSetting.Value.User,
                    smtpSetting.Value.Password
                );
                emailClient.EnableSsl = smtpSetting.Value.EnableSSL; // Enable SSL if required
                await emailClient.SendMailAsync(message);
            }
        }

    }
}
