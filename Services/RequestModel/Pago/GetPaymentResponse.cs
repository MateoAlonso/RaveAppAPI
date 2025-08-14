using Newtonsoft.Json;

namespace RaveAppAPI.Services.RequestModel.Pago
{
    public class GetPaymentResponse
    {
        [JsonProperty("accounts_info")]
        public object AccountsInfo { get; set; }

        [JsonProperty("acquirer_reconciliation")]
        public List<object> AcquirerReconciliation { get; set; }

        [JsonProperty("additional_info")]
        public AdditionalInfo AdditionalInfo { get; set; }

        [JsonProperty("authorization_code")]
        public object AuthorizationCode { get; set; }

        [JsonProperty("binary_mode")]
        public bool BinaryMode { get; set; }

        [JsonProperty("brand_id")]
        public object BrandId { get; set; }

        [JsonProperty("build_version")]
        public string BuildVersion { get; set; }

        [JsonProperty("call_for_authorize_id")]
        public object CallForAuthorizeId { get; set; }

        [JsonProperty("captured")]
        public bool Captured { get; set; }

        [JsonProperty("card")]
        public Card Card { get; set; }

        [JsonProperty("charges_details")]
        public List<ChargesDetail> ChargesDetails { get; set; }

        [JsonProperty("charges_execution_info")]
        public ChargesExecutionInfo ChargesExecutionInfo { get; set; }

        [JsonProperty("collector_id")]
        public long CollectorId { get; set; }

        [JsonProperty("corporation_id")]
        public object CorporationId { get; set; }

        [JsonProperty("counter_currency")]
        public object CounterCurrency { get; set; }

        [JsonProperty("coupon_amount")]
        public int CouponAmount { get; set; }

        [JsonProperty("currency_id")]
        public string CurrencyId { get; set; }

        [JsonProperty("date_approved")]
        public DateTime DateApproved { get; set; }

        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("date_last_updated")]
        public DateTime DateLastUpdated { get; set; }

        [JsonProperty("date_of_expiration")]
        public object DateOfExpiration { get; set; }

        [JsonProperty("deduction_schema")]
        public object DeductionSchema { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("differential_pricing_id")]
        public object DifferentialPricingId { get; set; }

        [JsonProperty("external_reference")]
        public object ExternalReference { get; set; }

        [JsonProperty("fee_details")]
        public List<FeeDetail> FeeDetails { get; set; }

        [JsonProperty("financing_group")]
        public object FinancingGroup { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("installments")]
        public int Installments { get; set; }

        [JsonProperty("integrator_id")]
        public object IntegratorId { get; set; }

        [JsonProperty("issuer_id")]
        public string IssuerId { get; set; }

        [JsonProperty("live_mode")]
        public bool LiveMode { get; set; }

        [JsonProperty("marketplace_owner")]
        public object MarketplaceOwner { get; set; }

        [JsonProperty("merchant_account_id")]
        public object MerchantAccountId { get; set; }

        [JsonProperty("merchant_number")]
        public object MerchantNumber { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("money_release_date")]
        public DateTime MoneyReleaseDate { get; set; }

        [JsonProperty("money_release_schema")]
        public object MoneyReleaseSchema { get; set; }

        [JsonProperty("money_release_status")]
        public string MoneyReleaseStatus { get; set; }

        [JsonProperty("notification_url")]
        public object NotificationUrl { get; set; }

        [JsonProperty("operation_type")]
        public string OperationType { get; set; }

        [JsonProperty("order")]
        public Order Order { get; set; }

        [JsonProperty("payer")]
        public Payer Payer { get; set; }

        [JsonProperty("payment_method")]
        public PaymentMethod PaymentMethod { get; set; }

        [JsonProperty("payment_method_id")]
        public string PaymentMethodId { get; set; }

        [JsonProperty("payment_type_id")]
        public string PaymentTypeId { get; set; }

        [JsonProperty("platform_id")]
        public object PlatformId { get; set; }

        [JsonProperty("point_of_interaction")]
        public PointOfInteraction PointOfInteraction { get; set; }

        [JsonProperty("pos_id")]
        public object PosId { get; set; }

        [JsonProperty("processing_mode")]
        public string ProcessingMode { get; set; }

        [JsonProperty("refunds")]
        public List<object> Refunds { get; set; }

        [JsonProperty("release_info")]
        public object ReleaseInfo { get; set; }

        [JsonProperty("shipping_amount")]
        public int ShippingAmount { get; set; }

        [JsonProperty("sponsor_id")]
        public object SponsorId { get; set; }

        [JsonProperty("statement_descriptor")]
        public object StatementDescriptor { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("status_detail")]
        public string StatusDetail { get; set; }

        [JsonProperty("store_id")]
        public object StoreId { get; set; }

        [JsonProperty("tags")]
        public object Tags { get; set; }

        [JsonProperty("taxes_amount")]
        public int TaxesAmount { get; set; }

        [JsonProperty("transaction_amount")]
        public int TransactionAmount { get; set; }

        [JsonProperty("transaction_amount_refunded")]
        public int TransactionAmountRefunded { get; set; }

        [JsonProperty("transaction_details")]
        public TransactionDetails TransactionDetails { get; set; }
    }
    public class Accounts
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
    }

    public class AdditionalInfo
    {
        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("tracking_id")]
        public string TrackingId { get; set; }
    }

    public class Amounts
    {
        [JsonProperty("original")]
        public double Original { get; set; }

        [JsonProperty("refunded")]
        public int Refunded { get; set; }
    }

    public class ApplicationData
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("operating_system")]
        public object OperatingSystem { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

    public class BusinessInfo
    {
        [JsonProperty("branch")]
        public string Branch { get; set; }

        [JsonProperty("sub_unit")]
        public string SubUnit { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }
    }

    public class Card
    {
    }

    public class ChargesDetail
    {
        [JsonProperty("accounts")]
        public Accounts Accounts { get; set; }

        [JsonProperty("amounts")]
        public Amounts Amounts { get; set; }

        [JsonProperty("base_amount")]
        public int BaseAmount { get; set; }

        [JsonProperty("client_id")]
        public int ClientId { get; set; }

        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rate")]
        public double Rate { get; set; }

        [JsonProperty("refund_charges")]
        public List<object> RefundCharges { get; set; }

        [JsonProperty("reserve_id")]
        public object ReserveId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class ChargesExecutionInfo
    {
        [JsonProperty("internal_execution")]
        public InternalExecution InternalExecution { get; set; }
    }

    public class FeeDetail
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("fee_payer")]
        public string FeePayer { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class InternalExecution
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("execution_id")]
        public string ExecutionId { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("source_detail")]
        public string SourceDetail { get; set; }

        [JsonProperty("id_compra")]
        public string IdCompra { get; set; }
    }

    public class Order
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class PaymentMethod
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("issuer_id")]
        public string IssuerId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class PointOfInteraction
    {
        [JsonProperty("application_data")]
        public ApplicationData ApplicationData { get; set; }

        [JsonProperty("business_info")]
        public BusinessInfo BusinessInfo { get; set; }

        [JsonProperty("transaction_data")]
        public TransactionData TransactionData { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class TransactionData
    {
        [JsonProperty("e2e_id")]
        public object E2eId { get; set; }
    }

    public class TransactionDetails
    {
        [JsonProperty("acquirer_reference")]
        public object AcquirerReference { get; set; }

        [JsonProperty("external_resource_url")]
        public object ExternalResourceUrl { get; set; }

        [JsonProperty("financial_institution")]
        public object FinancialInstitution { get; set; }

        [JsonProperty("installment_amount")]
        public int InstallmentAmount { get; set; }

        [JsonProperty("net_received_amount")]
        public double NetReceivedAmount { get; set; }

        [JsonProperty("overpaid_amount")]
        public int OverpaidAmount { get; set; }

        [JsonProperty("payable_deferral_period")]
        public object PayableDeferralPeriod { get; set; }

        [JsonProperty("payment_method_reference_id")]
        public object PaymentMethodReferenceId { get; set; }

        [JsonProperty("total_paid_amount")]
        public int TotalPaidAmount { get; set; }
    }
}
