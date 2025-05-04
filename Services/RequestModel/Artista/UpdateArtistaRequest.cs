namespace RaveAppAPI.Services.RequestModel.Artista
{
    public record UpdateArtistaRequest(string idArtista, string Nombre, string Bio, sbyte IsActivo);
}
