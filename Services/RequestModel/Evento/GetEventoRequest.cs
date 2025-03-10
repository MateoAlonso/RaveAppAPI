namespace RaveAppAPI.Services.RequestModel.Evento
{
    public record GetEventoRequest(string? IdEvento, int? Estado, int? CodigoProvincia, List<int>? Genero, bool? IsAfter, bool? IsLgbt);
}