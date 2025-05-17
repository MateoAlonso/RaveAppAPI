using RaveAppAPI.Services.Models;

namespace RaveAppAPI.Services.RequestModel.Artista
{
    public record UpdateArtistaRequest(string idArtista, string Nombre, string Bio, bool IsActivo, Socials? Socials);
}
