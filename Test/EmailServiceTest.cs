using EmailApplication.Controllers;
using EmailApplication.Domain;
using EmailApplication.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Test
{
   public class EmailServiceTest
   {
      private readonly Mock<ILogger<EmailService>> _mockLogger;
      private readonly Mock<IOptions<SMTPConfig>> _mockSmtpConfig;
     
      public EmailServiceTest()
      {
          _mockLogger = new Mock<ILogger<EmailService>>();
          _mockSmtpConfig = new Mock<IOptions<SMTPConfig>>();
      }

     [Fact]
      public async void SendMailAsyns_Should_sendmail()
      {
        var smtpClient = new SMTPClient { Host = "test", Port = 25};
        var emailRequest = new EmailRequestModel() { RecipientMailId = "" };
        var emailResponse = new EmailResponseModel() { Status = true, Message = "Scuccessfully sent" };
        var emailService = new EmailService(emailRequest);

        var response = await emailService.SendEmailAsync(_mockLogger,_mockSmtpConfig);

        Assert.Equal(emailResponse.Status, response.Result.Status);
        Assert.Equal(emailResponse.Message, response.Result.Message);
      }
   }
}
