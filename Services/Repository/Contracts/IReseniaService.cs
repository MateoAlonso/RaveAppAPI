using ErrorOr;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.RequestModel.Resenia;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface IReseniaService
    {
        ErrorOr<Created> CreateResenia(Resenia resenia);
        ErrorOr<List<Resenia>> GetResenias(GetReseniaRequest request);
        ErrorOr<List<AvgReseniaDTO>> GetAvgResenias(GetAvgReseniaRequest idFiesta);
        ErrorOr<Updated> UpdateResenia(Resenia resenia);
        ErrorOr<Deleted> DeleteResenia(string idResenia);
    }
}
