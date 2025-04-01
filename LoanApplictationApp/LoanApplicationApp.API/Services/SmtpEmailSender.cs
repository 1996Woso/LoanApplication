using System.Net;
using System.Net.Mail;
using LoanApplictationApp.API.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace LoanApplicationApp.API.Services
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
                From = new MailAddress(smtpSetting.Value.User),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            message.To.Add(email);
            using(var emailClient = new SmtpClient(smtpSetting.Value.Host, smtpSetting.Value.Port))
            {
                emailClient.Credentials = new NetworkCredential(
                   smtpSetting.Value.User,
                   smtpSetting.Value.Password
                );
                emailClient.EnableSsl = smtpSetting.Value.EnableSSL;
                await emailClient.SendMailAsync(message);
            }
        }
    }
}
