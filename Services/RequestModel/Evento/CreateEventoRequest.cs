using RaveAppAPI.Services.Models;

namespace RaveAppAPI.Services.RequestModel.Evento
{
    public record CreateEventoRequest(string idUsuario, List<string> idArtistas, Domicilio domicilio, string nombre, string descripcion, List<int> genero, bool isAfter, bool isLgbt, DateTime inicioVenta, DateTime finVenta, DateTime inicioEvento, DateTime finEvento, int estado, List<CreateFechaRequest> fechas, string? idFiesta, string? SoundCloud);
}