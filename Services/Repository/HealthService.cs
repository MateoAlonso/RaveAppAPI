using ErrorOr;
using MySql.Data.MySqlClient;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Repository.Contracts;

namespace RaveAppAPI.Services.Repository
{
    public class HealthService : IHealthService
    {
        private readonly string connectionString = Environment.GetEnvironmentVariable("dbcon", EnvironmentVariableTarget.Machine);
        public ErrorOr<string> GetDBHealth()
        {
            try
            {
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
