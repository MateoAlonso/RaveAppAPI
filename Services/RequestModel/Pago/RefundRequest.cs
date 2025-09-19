using Newtonsoft.Json;

namespace RaveAppAPI.Services.RequestModel.Pago
{
    public class RefundRequest
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }

}
