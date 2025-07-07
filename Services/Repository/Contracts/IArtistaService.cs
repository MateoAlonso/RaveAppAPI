using ErrorOr;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.RequestModel.Artista;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface IArtistaService
    {
        ErrorOr<Created> CreateArtista(Artista artista);
        ErrorOr<Updated> UpdateArtista(Artista artista);
        ErrorOr<Deleted> DeleteArtista(string idArtista);
        ErrorOr<List<Artista>> GetArtistas(GetArtistaRequest request);
        int GetCantLikesArtista(string id);
    }
}
