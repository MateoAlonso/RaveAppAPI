namespace RaveAppAPI.Services.Helpers
{
    public static class PaymentStatus
    {
        //Confirmar compra
        public const string Approved = "approved";
        public const string Authorized = "authorized";
        //Esperar confirmacion
        public const string Pending = "pending";
        public const string InProcess = "in_process";
        public const string InMediation = "in_mediation";
        //Rollback compra
        public const string Rejected = "rejected";
        public const string Cancelled = "cancelled";
        public const string Refunded = "refunded";
        public const string ChargedBack = "charged_back";
    }
}
