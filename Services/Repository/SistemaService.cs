using ErrorOr;
using MySql.Data.MySqlClient;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Repository.Contracts;

namespace RaveAppAPI.Services.Repository
{
    public class SistemaService : ISistemaService
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

        public ErrorOr<string> GetParametro(string parametro)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetParametro, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.GetParametroParameters(parametro));
                    cmd.ExecuteNonQuery();
                    return cmd.Parameters["p_result"].Value.ToString() ?? string.Empty;
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
