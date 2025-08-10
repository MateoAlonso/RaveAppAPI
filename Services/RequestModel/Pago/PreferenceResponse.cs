using Newtonsoft.Json;

namespace RaveAppAPI.Services.RequestModel.Pago
{
    public class PreferenceResponse
    {
        [JsonProperty("additional_info")]
        public string AdditionalInfo { get; set; }

        [JsonProperty("auto_return")]
        public string AutoReturn { get; set; }

        [JsonProperty("back_urls")]
        public BackUrls BackUrls { get; set; }

        [JsonProperty("binary_mode")]
        public bool BinaryMode { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("collector_id")]
        public long CollectorId { get; set; }

        [JsonProperty("coupon_code")]
        public object CouponCode { get; set; }

        [JsonProperty("coupon_labels")]
        public object CouponLabels { get; set; }

        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("date_of_expiration")]
        public object DateOfExpiration { get; set; }

        [JsonProperty("expiration_date_from")]
        public DateTime ExpirationDateFrom { get; set; }

        [JsonProperty("expiration_date_to")]
        public DateTime ExpirationDateTo { get; set; }

        [JsonProperty("expires")]
        public bool Expires { get; set; }

        [JsonProperty("external_reference")]
        public string ExternalReference { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("init_point")]
        public string InitPoint { get; set; }

        [JsonProperty("internal_metadata")]
        public object InternalMetadata { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("marketplace")]
        public string Marketplace { get; set; }

        [JsonProperty("marketplace_fee")]
        public int MarketplaceFee { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("notification_url")]
        public string NotificationUrl { get; set; }

        [JsonProperty("operation_type")]
        public string OperationType { get; set; }

        [JsonProperty("payer")]
        public Payer Payer { get; set; }

        [JsonProperty("payment_methods")]
        public PaymentMethods PaymentMethods { get; set; }

        [JsonProperty("processing_modes")]
        public object ProcessingModes { get; set; }

        [JsonProperty("product_id")]
        public object ProductId { get; set; }

        [JsonProperty("redirect_urls")]
        public RedirectUrls RedirectUrls { get; set; }

        [JsonProperty("sandbox_init_point")]
        public string SandboxInitPoint { get; set; }

        [JsonProperty("site_id")]
        public string SiteId { get; set; }

        [JsonProperty("shipments")]
        public Shipments Shipments { get; set; }

        [JsonProperty("statement_descriptor")]
        public string StatementDescriptor { get; set; }

        [JsonProperty("total_amount")]
        public object TotalAmount { get; set; }

        [JsonProperty("last_updated")]
        public object LastUpdated { get; set; }
    }

    public class ExcludedPaymentMethod
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class ExcludedPaymentType
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class ReceiverAddress
    {
        [JsonProperty("zip_code")]
        public string ZipCode { get; set; }

        [JsonProperty("street_name")]
        public string StreetName { get; set; }

        [JsonProperty("street_number")]
        public object StreetNumber { get; set; }

        [JsonProperty("floor")]
        public string Floor { get; set; }

        [JsonProperty("apartment")]
        public string Apartment { get; set; }

        [JsonProperty("city_name")]
        public object CityName { get; set; }

        [JsonProperty("state_name")]
        public object StateName { get; set; }

        [JsonProperty("country_name")]
        public object CountryName { get; set; }
    }

    public class RedirectUrls
    {
        [JsonProperty("failure")]
        public string Failure { get; set; }

        [JsonProperty("pending")]
        public string Pending { get; set; }

        [JsonProperty("success")]
        public string Success { get; set; }
    }

    public class Shipments
    {
        [JsonProperty("default_shipping_method")]
        public object DefaultShippingMethod { get; set; }

        [JsonProperty("receiver_address")]
        public ReceiverAddress ReceiverAddress { get; set; }
    }


}
