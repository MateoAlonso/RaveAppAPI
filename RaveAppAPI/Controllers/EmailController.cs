using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Mail;
using RaveAppAPI.Services.RequestModel.Pago;
using System.Net.Http.Headers;
using System.Text;
using static RaveAppAPI.Services.Helpers.ApiMailHelper;

namespace RaveAppAPI.Controllers
{
    [Authorize]
    public class EmailController : ApiController
    {
        private readonly string _jwtKey = EnvHelper.GetJWTKey();
        private readonly string _jwtIssuer = EnvHelper.GetJWTIssuer();
        private readonly string _MailgunToken = EnvHelper.GetMailgunToken();
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("EnviarConfirmarEmail")]
        public async Task<IActionResult> EnviarConfirmarEmail(ConfirmarEmailRequest request)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    request.TemplateData.ConfirmationUrl = $"{request.TemplateData.ConfirmationUrl}?token={JwtHelper.GenerateToken(_jwtKey, _jwtIssuer, 15)}&correo={request.To}";

                    string templateData = JsonConvert.SerializeObject(request.TemplateData);

                    MultipartFormDataContent formData = new MultipartFormDataContent
                    {
                        { new StringContent(FromEmail), "from" },
                        { new StringContent(request.To), "to" },
                        { new StringContent(ConfirmarMailTemplate), "template" },
                        { new StringContent(templateData), "t:variables" }
                    };

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
                    request.TemplateData.RecoveryUrl = $"{request.TemplateData.RecoveryUrl}?token={JwtHelper.GenerateToken(_jwtKey, _jwtIssuer, 15)}&correo={request.To}";

                    string templateData = JsonConvert.SerializeObject(request.TemplateData);

                    MultipartFormDataContent formData = new MultipartFormDataContent
                    {
                        { new StringContent(FromEmail), "from" },
                        { new StringContent(request.To), "to" },
                        { new StringContent(PassRecoveryTemplate), "template" },
                        { new StringContent(templateData), "t:variables" }
                    };

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
        [HttpPost("EnvioMailGenerico")]
        public async Task<IActionResult> EnvioMailGenerico(EnvioEmailGenericoRequest request)
        {
            try
            {
                string html = ApiMailHelper.BuildEmailGenerico(request.Titulo, request.Cuerpo, request.BotonUrl, request.BotonTexto);
                MultipartFormDataContent formData = new MultipartFormDataContent
                {
                    { new StringContent(FromEmail), "from" },
                    { new StringContent(request.To), "to" },
                    { new StringContent($"{request.Titulo}"), "subject" },
                    { new StringContent(html), "html" }
                };
                using (HttpClient client = new HttpClient())
                {
                    var sendRequest = ApiMailHelper.BuildRequest(
                        HttpMethod.Post,
                        BaseUrl,
                        String.Format(MessagesEndpoint, DomainName),
                        formData,
                            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{_MailgunToken}"))));

                    var response = await client.SendAsync(sendRequest);
                    if (!response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        Logger.LogError($"Error enviando mail generico: {(int)response.StatusCode} - {content}");
                        return Problem();
                    }
                    return Ok();
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return Problem();
            }
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public async void EnviarMailsQR(string idCompra)
        {
            try
            {
                var emailData = _emailService.GetEmailQrData(idCompra);
                if (emailData.IsError)
                {
                    return;
                }
                var emailDataResult = emailData.Value;

                string nombreEvento = emailDataResult.First().NombreEvento;
                string fechaEvento = emailDataResult.First().DtInicioFecha.ToString("f");
                string To = emailDataResult.First().To;
                string qrHtmlSection = string.Empty;
                MultipartFormDataContent formData = new MultipartFormDataContent
                {
                    { new StringContent(FromEmail), "from" },
                    { new StringContent(To), "to" },
                    { new StringContent($"Entradas - {nombreEvento}"), "subject" }
                };
                foreach (var qrEmail in emailDataResult)
                {
                    qrHtmlSection += ApiMailHelper.BuildQrSection($"QR_{qrEmail.IdEntrada}", qrEmail.TipoEntrada);
                    string qrContent = $"{qrEmail.IdEntrada},{qrEmail.MdQr}";
                    var qrCode = QRHelper.GenerateQRCode(qrContent);
                    formData.Add(new ByteArrayContent(qrCode), "inline", $"QR_{qrEmail.IdEntrada}");
                }
                string html = ApiMailHelper.BuildQrEmail(nombreEvento, fechaEvento, qrHtmlSection);
                formData.Add(new StringContent(html), "html");

                using (HttpClient client = new HttpClient())
                {
                    var sendRequest = ApiMailHelper.BuildRequest(
                        HttpMethod.Post,
                        BaseUrl,
                        String.Format(MessagesEndpoint, DomainName),
                        formData,
                            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{_MailgunToken}"))));

                    var response = await client.SendAsync(sendRequest);
                    if (!response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        Logger.LogError($"Error enviando mails QR: {(int)response.StatusCode} - {content}");
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
            }
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public async void EnviarMailReembolsoMasivo(string nombreEvento, decimal monto, string correo)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string templateData = JsonConvert.SerializeObject(
                        new { amount = monto, event_name = nombreEvento}
                        );

                    MultipartFormDataContent formData = new MultipartFormDataContent
                    {
                        { new StringContent(FromEmail), "from" },
                        { new StringContent(correo), "to" },
                        { new StringContent(ReembolsoMasivoTemplate), "template" },
                        { new StringContent(templateData), "t:variables" }
                    };

                    var sendRequest = ApiMailHelper.BuildRequest(
                        HttpMethod.Post,
                        BaseUrl,
                        String.Format(MessagesEndpoint, DomainName),
                        formData,
                         new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{_MailgunToken}"))));

                    var response = await client.SendAsync(sendRequest);
                    if (!response.IsSuccessStatusCode)
                    {
                        Logger.LogError($"Error enviando correo devolucion masiva: {correo}");
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
            }
        }
    }
}
