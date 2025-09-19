using ErrorOr;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface ISistemaService
    {
        ErrorOr<string> GetDBHealth();
        ErrorOr<string> GetParametro(string parametro);
    }
}
