using RaveAppAPI.Services.Models;
namespace RaveAppAPI.Services.RequestModel.Evento
{
    public record UpdateEventoRequest(string idEvento,List<string> idArtistas, Domicilio domicilio, string nombre, string descripcion, List<int> genero, bool isAfter, bool isLgbt, DateTime inicioEvento, DateTime finEvento, int estado, List<Fecha> fechas, string? idFiesta);
}
