namespace RaveAppAPI.Services.RequestModel.Evento
{
    public record GetEventoRequest(string? IdEvento, int? Estado, int? CodigoProvincia, int? CodigoMunicipio, int? CodigoLocalidad, List<int>? Genero, bool? IsAfter, bool? IsLgbt, string? idFiesta);
}