using EmailApplication.Domain;

namespace EmailApplication.Interface
{
    public interface IEmailService
    {
        Task<EmailResponseModel> SendEmailAsync(EmailRequestModel emailRequestModel);
    }
}
