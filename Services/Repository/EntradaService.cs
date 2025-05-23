using ErrorOr;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Entrada;

namespace RaveAppAPI.Services.Repository
{
    public class EntradaService : IEntradaService
    {
        public ErrorOr<Created> CreateEntrada(Entrada entrada)
        {
            throw new NotImplementedException();
        }

        public ErrorOr<Entrada> GetEntradasFecha(GetEntradasFechaRequest request)
        {
            throw new NotImplementedException();
        }

        public ErrorOr<Updated> UpdateEntrada(Entrada entrada)
        {
            throw new NotImplementedException();
        }
    }
}
