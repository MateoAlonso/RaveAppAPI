using ErrorOr;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface IHealthService
    {
        ErrorOr<string> GetDBHealth();
    }
}
