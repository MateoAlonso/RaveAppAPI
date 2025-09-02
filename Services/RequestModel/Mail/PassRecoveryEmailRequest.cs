using Newtonsoft.Json;

namespace RaveAppAPI.Services.RequestModel.Mail
{
    public class PassRecoveryEmailRequest : EmailRequest
    {
        public PassRecoveryEmailData TemplateData { get; set; }
    }
    public class PassRecoveryEmailData
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("recoveryUrl")]
        public string RecoveryUrl { get; set; }
    }
}
