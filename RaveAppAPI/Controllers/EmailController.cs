using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.RequestModel.Mail;
using System.Net.Http.Headers;
using System.Text;

namespace RaveAppAPI.Controllers
{
    [Authorize]
    public class EmailController : ApiController
    {
        private readonly string _jwtKey = EnvHelper.GetJWTKey();
        private readonly string _jwtIssuer = EnvHelper.GetJWTIssuer();
        private readonly string _MailgunToken = EnvHelper.GetMailgunToken();

        [HttpPost("EnviarConfirmarEmail")]
        public async Task<IActionResult> EnviarConfirmarEmail(ConfirmarEmailRequest request)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    request.TemplateData.ConfirmationUrl = $"{request.TemplateData.ConfirmationUrl}?token={JwtHelper.GenerateToken(_jwtKey, _jwtIssuer, 15)}";

                    string templateData = JsonConvert.SerializeObject(request.TemplateData);

                    Dictionary<string, object> formData = new Dictionary<string, object>();
                    formData.Add("from", FromEmail);
                    formData.Add("to", new List<string> { request.To });
                    formData.Add("template", ConfirmarMailTemplate);
                    formData.Add("t:variables", templateData);

                    var sendRequest = ApiMailHelper.BuildRequest(
                        HttpMethod.Post,
                        BaseUrl,
                        String.Format(MessagesEndpoint, DomainName),
                        formData,
                         new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{_MailgunToken}"))));

                    var response = client.SendAsync(sendRequest).GetAwaiter().GetResult();
                    if (!response.IsSuccessStatusCode)
                    {
                        return Problem();
                    }
                }

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
                using (HttpClient client = new HttpClient())
                {
                    request.TemplateData.RecoveryUrl = $"{request.TemplateData.RecoveryUrl}?token={JwtHelper.GenerateToken(_jwtKey, _jwtIssuer, 15)}";

                    string templateData = JsonConvert.SerializeObject(request.TemplateData);

                    Dictionary<string, object> formData = new Dictionary<string, object>();
                    formData.Add("from", FromEmail);
                    formData.Add("to", new List<string> { request.To });
                    formData.Add("template", PassRecoveryTemplate);
                    formData.Add("t:variables", templateData);

                    var sendRequest = ApiMailHelper.BuildRequest(
                        HttpMethod.Post,
                        BaseUrl,
                        String.Format(MessagesEndpoint, DomainName),
                        formData,
                         new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{_MailgunToken}"))));

                    var response = client.SendAsync(sendRequest).GetAwaiter().GetResult();
                    if (!response.IsSuccessStatusCode)
                    {
                        return Problem();
                    }
                }

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
