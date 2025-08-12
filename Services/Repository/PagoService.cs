using ErrorOr;
using MySql.Data.MySqlClient;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Repository.Contracts;

namespace RaveAppAPI.Services.Repository
{
    public class PagoService : IPagoService
    {
        private readonly string connectionString = EnvHelper.GetConnectionString();

        public ErrorOr<Updated> FinalizarCompra(string idCompra, int cdMedioPago)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.FinalizarCompra, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.FinalizarCompraParameters(idCompra, cdMedioPago));
                    cmd.ExecuteNonQuery();
                    return Result.Updated;
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
