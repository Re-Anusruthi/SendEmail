using EmailApplication.Domain;
using EmailApplication.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EmailApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {

        private readonly ILogger<EmailController> _logger;
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService, ILogger<EmailController> logger)
        {
            _logger = logger;
            _emailService = emailService;
        }

        [HttpPost(Name = "SendEmail")]
        public async Task<EmailResponseModel> SendEmail([FromBody] EmailRequestModel emailRequestModel)
        {
            try
            {
                _logger.LogInformation($"Sending email to {emailRequestModel.RecipientMailId}");
                var sendEmailResponse = await _emailService.SendEmailAsync(emailRequestModel);
                _logger.LogInformation($"Mail sent successfully to {emailRequestModel.RecipientMailId}");
                return sendEmailResponse;
;            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception occured: {ex.Message}");
                return new EmailResponseModel() { Status = false, Message = ex.Message };
            }
        }
    }
}