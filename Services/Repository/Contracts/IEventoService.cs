using ErrorOr;
using RaveAppAPI.Services.Models;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface IEventoService
    {
        ErrorOr<Created> CreateEvento(Evento Evento);
        ErrorOr<Evento> GetEventoById(string id);
        ErrorOr<Evento> GetEventoByNombre(string mail);
        ErrorOr<Updated> UpdateEvento(Evento Evento);
        ErrorOr<Deleted> DeleteEvento(string id);
    }
}
