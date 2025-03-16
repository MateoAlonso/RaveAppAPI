using ErrorOr;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.RequestModel.Evento;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface IEventoService
    {
        ErrorOr<Created> CreateEvento(Evento Evento);
        ErrorOr<List<Evento>> GetEventos(GetEventoRequest request);
        ErrorOr<Updated> UpdateEvento(Evento Evento);
        ErrorOr<Deleted> DeleteEvento(string id);
        ErrorOr<Created> SetFechas(Evento evento);
    }
}
