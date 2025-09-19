using Newtonsoft.Json;

namespace RaveAppAPI.Services.RequestModel.Pago
{
    public class RefundResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("payment_id")]
        public long PaymentId { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("metadata")]
        public List<Metadata> Metadata { get; set; }

        [JsonProperty("source")]
        public List<Source> Source { get; set; }

        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("unique_sequence_number")]
        public object UniqueSequenceNumber { get; set; }

        [JsonProperty("refund_mode")]
        public string RefundMode { get; set; }

        [JsonProperty("adjustment_amount")]
        public int AdjustmentAmount { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("reason")]
        public object Reason { get; set; }

        [JsonProperty("partition_details")]
        public List<PartitionDetail> PartitionDetails { get; set; }
    }

    public class Name
    {
        [JsonProperty("en")]
        public string En { get; set; }

        [JsonProperty("pt")]
        public string Pt { get; set; }

        [JsonProperty("es")]
        public string Es { get; set; }
    }

    public class PartitionDetail
    {
    }

    public class Source
    {
        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }


}
