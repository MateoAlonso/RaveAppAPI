using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmailV2;
using Amazon.SimpleEmailV2.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.RequestModel.Mail;

namespace RaveAppAPI.Controllers
{
    [Authorize]
    public class EmailController : ApiController
    {
        private readonly IAmazonSimpleEmailServiceV2 _sesClient;
        private readonly string _accessKey = EnvHelper.GetSESAccessKey();
        private readonly string _secretKey = EnvHelper.GetSESSecretKey();
        private readonly string _jwtKey = EnvHelper.GetJWTKey();
        private readonly string _jwtIssuer = EnvHelper.GetJWTIssuer();

        public EmailController()
        {
            _sesClient = new AmazonSimpleEmailServiceV2Client(
                new BasicAWSCredentials(_accessKey, _secretKey),
                RegionEndpoint.SAEast1
            );
        }
        [HttpPost("EnviarConfirmarEmail")]
        public async Task<IActionResult> EnviarConfirmarEmail(ConfirmarEmailRequest request)
        {
            try
            {
                request.TemplateData.ConfirmationUrl = $"{request.TemplateData.ConfirmationUrl}?token={JwtHelper.GenerateToken(_jwtKey, _jwtIssuer, 15)}";

                string templateData = JsonConvert.SerializeObject(request.TemplateData);

                var sendRequest = new SendEmailRequest
                {
                    FromEmailAddress = MailHelper.FromEmail,
                    Destination = new Destination
                    {
                        ToAddresses = new List<string> { request.To }
                    },
                    Content = new EmailContent
                    {
                        Template = new Template
                        {
                            TemplateName = MailHelper.ConfirmarMailTemplate,
                            TemplateData = templateData
                        }
                    }
                };

                var response = await _sesClient.SendEmailAsync(sendRequest);

                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return Problem(e.Message);
            }
        }
        [HttpPost("EnviarPassRecoveryEmail")]
        public async Task<IActionResult> EnviarPassRecoveryEmail(PassRecoveryEmailRequest request)
        {
            try
            {
                request.TemplateData.RecoveryUrl = $"{request.TemplateData.RecoveryUrl}?token={JwtHelper.GenerateToken(_jwtKey, _jwtIssuer, 15)}";
                string templateData = JsonConvert.SerializeObject(request.TemplateData);

                var sendRequest = new SendEmailRequest
                {
                    FromEmailAddress = MailHelper.FromEmail,
                    Destination = new Destination
                    {
                        ToAddresses = new List<string> { request.To }
                    },
                    Content = new EmailContent
                    {
                        Template = new Template
                        {
                            TemplateName = MailHelper.PassRecoveryTemplate,
                            TemplateData = templateData
                        }
                    }
                };

                var response = await _sesClient.SendEmailAsync(sendRequest);

                return Ok();
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return Problem(e.Message);
            }
        }
    }
}
