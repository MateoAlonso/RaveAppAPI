using Newtonsoft.Json;

namespace RaveAppAPI.Services.RequestModel.Pago
{
    public class PreferenceRequest
    {
        [JsonProperty("auto_return")]
        public string AutoReturn { get; set; }

        [JsonProperty("back_urls")]
        public BackUrls BackUrls { get; set; }

        [JsonProperty("statement_descriptor")]
        public string StatementDescriptor { get; set; }

        [JsonProperty("binary_mode")]
        public bool BinaryMode { get; set; }

        [JsonProperty("external_reference")]
        public string ExternalReference { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("payer")]
        public Payer Payer { get; set; }

        [JsonProperty("payment_methods")]
        public PaymentMethods PaymentMethods { get; set; }

        [JsonProperty("notification_url")]
        public string NotificationUrl { get; set; }

        [JsonProperty("expires")]
        public bool Expires { get; set; }

        [JsonProperty("expiration_date_from")]
        public DateTime ExpirationDateFrom { get; set; }

        [JsonProperty("expiration_date_to")]
        public DateTime ExpirationDateTo { get; set; }
        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }

    public class Address
    {
        [JsonProperty("street_name")]
        public string StreetName { get; set; }

        [JsonProperty("street_number")]
        public int StreetNumber { get; set; }

        [JsonProperty("zip_code")]
        public string ZipCode { get; set; }
    }

    public class BackUrls
    {
        [JsonProperty("success")]
        public string Success { get; set; }

        [JsonProperty("failure")]
        public string Failure { get; set; }

        [JsonProperty("pending")]
        public string Pending { get; set; }
    }

    public class Identification
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }
    }

    public class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("unit_price")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("category_id")]
        public string CategoryId { get; set; }
    }

    public class Payer
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("phone")]
        public Phone Phone { get; set; }

        [JsonProperty("identification")]
        public Identification Identification { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }
    }

    public class PaymentMethods
    {
        [JsonProperty("excluded_payment_types")]
        public List<object> ExcludedPaymentTypes { get; set; }

        [JsonProperty("excluded_payment_methods")]
        public List<object> ExcludedPaymentMethods { get; set; }

        [JsonProperty("installments")]
        public int Installments { get; set; }

        [JsonProperty("default_payment_method_id")]
        public string DefaultPaymentMethodId { get; set; }
    }

    public class Phone
    {
        [JsonProperty("area_code")]
        public string AreaCode { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }
    }

}
