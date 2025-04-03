using ErrorOr;
using RaveAppAPI.Services.Models;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface INoticiaService
    {
        ErrorOr<Created> CreateNoticia(Noticia noticia);
        ErrorOr<List<Noticia>> GetNoticias(string idNoticia);
        ErrorOr<Updated> UpdateNoticia(Noticia noticia);
        ErrorOr<Deleted> DeleteNoticia(string id);
    }
}
