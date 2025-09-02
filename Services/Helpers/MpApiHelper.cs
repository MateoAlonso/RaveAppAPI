namespace RaveAppAPI.Services.Helpers
{
    public class MpApiHelper : ApiHelper
    {
        public const string CreatePreference = "/checkout/preferences";
        public const string GetPayment = "/v1/payments/{0}";
        public const string RefundPayment = "/v1/payments/{0}/refunds";
    }
}
