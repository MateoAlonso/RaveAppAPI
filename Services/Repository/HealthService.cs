using ErrorOr;
using MySql.Data.MySqlClient;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Repository.Contracts;

namespace RaveAppAPI.Services.Repository
{
    public class HealthService : IHealthService
    {
        private readonly string connectionString = EnvHelper.GetConnectionString();
        public ErrorOr<string> GetDBHealth()
        {
            try
            {
                Logger.LogInfo($"Entro check health con ConnectionString: {connectionString}");
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    return dbcon.Ping() ? "OK" : Error.Failure();
                }

            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return Error.Unexpected();
            }
        }
    }
}
