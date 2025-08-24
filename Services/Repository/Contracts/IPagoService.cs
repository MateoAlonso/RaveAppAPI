using ErrorOr;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface IPagoService
    {
        ErrorOr<Updated> AnularCompra(string idCompra);
        ErrorOr<Updated> FinalizarCompra(string idCompra, int cdMedioPago);
        ErrorOr<Updated> PendienteCompra(string idCompra, decimal subTotal, decimal cargoServicio);
        void RefrescarTimerReserva(string idCompra);
    }
}
