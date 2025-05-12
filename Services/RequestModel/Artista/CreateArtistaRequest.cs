using RaveAppAPI.Services.Models;

namespace RaveAppAPI.Services.RequestModel.Artista
{
    public record CreateArtistaRequest(string Nombre, string Bio, Socials? Socials);
}
