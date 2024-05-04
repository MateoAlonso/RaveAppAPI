using RaveAppAPI.Models;
using RaveAppAPI.Services.Models;

namespace RaveAppAPI.Services.RequestModel.Evento
{
    public record CreateEventoRequest(Usuario usuario, Domicilio domicilio, string nombre, string descripcion, string genero, bool isAfter, bool isLgbt, DateTime inicioVenta, DateTime finVenta, DateTime inicioEvento, DateTime finEvento);
}
