using ErrorOr;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.RequestModel.Reporte;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface IReporteService
    {
        ErrorOr<List<VentasEventoDTO>> GetReporteVentasEvento(ReporteVentasEventoRequest request);
    }
}
