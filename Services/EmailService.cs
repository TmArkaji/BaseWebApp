using BaseWebApplication.Configurations;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace BaseWebApplication.Services
{
    public class EmailService : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private const string password = "lhcb oliv kpvw gqbh";

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public Task SendEmailAsync(string email, string subject, string htmlMessage)
        //{
        //    var message = new MailMessage
        //    {
        //        From = new MailAddress(fromEmailAddress),
        //        Subject = subject,
        //        Body = htmlMessage,
        //        IsBodyHtml = true,
        //    };

        //    message.To.Add(new MailAddress(email));
        //    message.Bcc.Add(new MailAddress(Constants.DEFAULT_USER_EMAIL));

        //    using (var client = new SmtpClient(smtpServer, smtpPort))
        //    {
        //        client.EnableSsl = _mailServerEnableSsl;
        //        client.UseDefaultCredentials = _mailServerUseDefaultCredentials;
        //        client.Credentials = new NetworkCredential(_email, password);
        //        client.Send(message);
        //    }
        //    return Task.CompletedTask;
        //}

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
            smtpSettings.Password = password;

            using (var client = new SmtpClient(smtpSettings.Host, smtpSettings.Port)
            {
                Credentials = new NetworkCredential(smtpSettings.UserName, smtpSettings.Password),
                EnableSsl = smtpSettings.EnableSsl,
            })
            using (var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpSettings.From),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                mailMessage.To.Add(smtpSettings.To);

                await client.SendMailAsync(mailMessage);
            }
        }
    }

    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}