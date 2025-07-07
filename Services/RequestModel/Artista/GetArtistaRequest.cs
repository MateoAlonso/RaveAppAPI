namespace RaveAppAPI.Services.RequestModel.Artista
{
    public record GetArtistaRequest(string? idArtista, string? idUsuario, bool? isActivo);
}
