using ErrorOr;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.RequestModel.Fiesta;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface IFiestaService
    {
        ErrorOr<Created> CreateFiesta(Fiesta fiesta);
        ErrorOr<Deleted> DeleteFiesta(string id);
        ErrorOr<List<Fiesta>> GetFiestas(GetFiestaRequest request);
        ErrorOr<Updated> UpdateFiesta(Fiesta fiesta);
    }
}
