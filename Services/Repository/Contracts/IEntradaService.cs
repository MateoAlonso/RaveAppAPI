using ErrorOr;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.RequestModel.Entrada;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface IEntradaService
    {
        ErrorOr<Created> CreateEntrada(Entrada entrada);
        ErrorOr<Updated> UpdateEntrada(Entrada entrada);
        ErrorOr<Entrada> GetEntradasFecha(GetEntradasFechaRequest request);

    }
}
