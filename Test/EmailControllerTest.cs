using EmailApplication.Controllers;
using EmailApplication.Domain;
using EmailApplication.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Test
{
    public class EmailControllerTest
    {
        private readonly Mock<ILogger<EmailController>> _mockLogger;

        public EmailControllerTest()
        {
            _mockLogger = new Mock<ILogger<EmailController>>();
        }


        [Fact]
        public void SendEmail_To_RecepientSuccessfully()
        {
            //Arrage
            var emailRequest = new EmailRequestModel() { RecipientMailId = "" };
            var emailResponse = new EmailResponseModel() { Status = true, Message = "Scuccessfully sent" };
            var mockSMTPConfig = new Mock<IOptions<SMTPConfig>>();
            mockSMTPConfig.Setup(x => x.Value).Returns(new SMTPConfig() { FromMailId = "mockemail@mock.com", Host = "mockhost.com", Password = "12345", Port = 80, TimeOut = 2000, UserName = "testuser" });

            var _mockEmailService = new Mock<IEmailService>();
            _mockEmailService.Setup(x => x.SendEmailAsync(It.Is<EmailRequestModel>(r => r == emailRequest))).ReturnsAsync(emailResponse);

            var _mockEmailController = new EmailController(_mockEmailService.Object, _mockLogger.Object);

            //Act
            var response = _mockEmailController.SendEmail(emailRequest);

            //Assert
            Assert.Equal(emailResponse.Status, response.Result.Status);
            Assert.Equal(emailResponse.Message, response.Result.Message);
        }

        [Fact]
        public void SendEmail_WithBadEmailId_ReturnsException()
        {
            //Arrage
            var emailRequest = new EmailRequestModel() { RecipientMailId = "bad emailid" };
            var emailResponse = new EmailResponseModel() { Status = false, Message = "Email not sent, Exception" };
            var mockSMTPConfig = new Mock<IOptions<SMTPConfig>>();
            mockSMTPConfig.Setup(x => x.Value).Returns(new SMTPConfig() { FromMailId = "mockemail@mock.com", Host = "mockhost.com", Password = "12345", Port = 80, TimeOut = 2000, UserName = "testuser" });

            var _mockEmailService = new Mock<IEmailService>();
            _mockEmailService.Setup(x => x.SendEmailAsync(It.Is<EmailRequestModel>(r => r == emailRequest))).ReturnsAsync(emailResponse);

            var _mockEmailController = new EmailController(_mockEmailService.Object, _mockLogger.Object);

            //Act
            var response = _mockEmailController.SendEmail(emailRequest);

            //Assert
            Assert.Equal(emailResponse.Status, response.Result.Status);
        }
    }
}
