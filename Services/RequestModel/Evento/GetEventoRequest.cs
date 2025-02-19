namespace RaveAppAPI.Services.RequestModel.Evento
{
    public record GetEventoRequest(string? IdEvento, int? Estado, int? CodigoProvincia, int? Genero, bool? IsAfter, bool? IsLgbt);
}