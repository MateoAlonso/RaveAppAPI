using ErrorOr;
using RaveAppAPI.Services.Models;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface IEventoService
    {
        ErrorOr<Created> CreateEvento(Evento Evento);
        ErrorOr<List<Evento>> GetEventos();
        ErrorOr<Evento> GetEventoById(string id);
        ErrorOr<List<Evento>> GetEventosByEstado(string estado);
        ErrorOr<Updated> UpdateEvento(Evento Evento);
        ErrorOr<Deleted> DeleteEvento(string id);
    }
}
