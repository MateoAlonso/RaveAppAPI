using Newtonsoft.Json;

namespace RaveAppAPI.Services.RequestModel.Mail
{
    public class ConfirmarEmailRequest : EmailRequest
    {
        public ConfirmarEmailData TemplateData { get; set; }
    }
    public class ConfirmarEmailData
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("confirmationUrl")]
        public string ConfirmationUrl { get; set; }
    }
}
