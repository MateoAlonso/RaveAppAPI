namespace RaveAppAPI.Services.Helpers
{
    public class ApiMailHelper : ApiHelper
    {
        public const string FromEmail = "comunicaciones@raveapp.com.ar";
        public const string DomainName = "raveapp.com.ar";
        public const string ConfirmarMailTemplate = "EmailConfirmationTemplate";
        public const string PassRecoveryTemplate = "PassRecoveryTemplate";
        public const string BaseUrl = "https://api.mailgun.net";
        public const string MessagesEndpoint = "/v3/{0}/messages";
    }
}
