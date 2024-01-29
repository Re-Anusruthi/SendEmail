using EmailApplication.Domain;

namespace MailService.Service
{
    public interface IEmailService
    {
        Task<EmailResponseModel> SendEmailAsync(EmailRequestModel emailRequestModel);
    }
}
