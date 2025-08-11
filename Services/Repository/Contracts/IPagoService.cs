using ErrorOr;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface IPagoService
    {
        ErrorOr<Updated> FinalizarCompra(string idCompra, int cdMedioPago);
    }
}
