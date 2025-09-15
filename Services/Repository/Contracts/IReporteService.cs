using ErrorOr;
using RaveAppAPI.Services.Models;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface IReporteService
    {
        ErrorOr<List<VentasEventoDTO>> GetReporteVentasEvento(string idEvento);
    }
}
