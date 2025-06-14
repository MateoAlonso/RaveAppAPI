namespace RaveAppAPI.Services.RequestModel.Evento
{
    public record GetEventoRequest(string? IdEvento, int? Estado, string? CodigoProvincia, string? CodigoMunicipio, string? CodigoLocalidad, List<int>? Genero, bool? IsAfter, bool? IsLgbt, string? idFiesta, string? IdUsuario);
}