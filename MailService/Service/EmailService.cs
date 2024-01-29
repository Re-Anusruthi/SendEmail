using EmailApplication.Domain;
using EmailApplication.Interface;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;

namespace EmailApplication.Service
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly SMTPConfig _smtpConfig;

        public EmailService(ILogger<EmailService> logger, IOptions<SMTPConfig> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
            _logger = logger;
        }

        public async Task<EmailResponseModel> SendEmailAsync(EmailRequestModel emailRequestModel)
        {
            try
            {
                using (var smtpClient = new SmtpClient(_smtpConfig.Host, _smtpConfig.Port))
                {
                    smtpClient.Credentials = new System.Net.NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.EnableSsl = true;
                    smtpClient.Timeout = _smtpConfig.TimeOut;

                    var mailMessage = CreateMailMessage(emailRequestModel);
                    await smtpClient.SendMailAsync(mailMessage);
                    return new EmailResponseModel { Status = true, Message = "Email sent successfully" };
                }
            }
            catch (Exception ex)
            {
                return new EmailResponseModel { Status = false, Message = ex.Message };
            }
        }

        private MailMessage CreateMailMessage(EmailRequestModel emailRequestModel)
        {
            var mail = new MailMessage()
            {
                Subject = "Test Mail",
                Body = "This mail is to test the mail sending functionality",
                From = new MailAddress(_smtpConfig.FromMailId)
            };
            mail.To.Add(new MailAddress(_smtpConfig.FromMailId));

            return mail;
        }
    }
}

