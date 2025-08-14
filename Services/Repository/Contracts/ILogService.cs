namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface ILogService
    {
        void LogWebhookMP(string idCompra, string estadoPago, string detalleEstadoPago, decimal monto, long idPagoMP);
    }
}
