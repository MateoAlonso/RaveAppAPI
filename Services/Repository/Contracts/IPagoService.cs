using ErrorOr;
using RaveAppAPI.Services.Models;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface IPagoService
    {
        ErrorOr<Updated> AnularCompra(string idCompra);
        ErrorOr<List<string>> FinalizarCompra(string idCompra, int cdMedioPago);
        ErrorOr<List<DatosReembolsoDTO>> GetDatosReembolso(string idEntrada);
        ErrorOr<Updated> PendienteCompra(string idCompra, decimal subTotal, decimal cargoServicio);
        void Reembolso(string idEntrada);
        void RefrescarTimerReserva(string idCompra);
    }
}
