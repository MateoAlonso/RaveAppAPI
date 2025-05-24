using ErrorOr;
using RaveAppAPI.Services.Models;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface IMediaService
    {
        ErrorOr<List<Media>> GetMedia(string idMediaEntidad);
        ErrorOr<Created> CreateMedia(Media media);
        ErrorOr<Deleted> DeleteMedia(string idMedia);
    }
}
