using ErrorOr;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.RequestModel.Entrada;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface IEntradaService
    {
        ErrorOr<Created> CreateEntrada(Entrada entrada);
        ErrorOr<List<Entrada>> GetEntradasFecha(GetEntradasFechaRequest request);
        ErrorOr<string> ReservarEntradas(ReservarEntradasRequest request);
        ErrorOr<Updated> CancelarReserva(string idCompra);
        ErrorOr<List<Estado>> GetEstadosEntrada();
        ErrorOr<List<Tipo>> GetTiposEntrada();
    }
}
