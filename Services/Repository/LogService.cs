using MySql.Data.MySqlClient;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Repository.Contracts;

namespace RaveAppAPI.Services.Repository
{
    public class LogService : ILogService
    {
        private readonly string connectionString = EnvHelper.GetConnectionString();
        public void LogWebhookMP(string idCompra, string estadoPago, string detalleEstadoPago, decimal monto, long idPagoMP)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.SetLogWebhookMP, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.SetLogWebhookMPParameters(idCompra, estadoPago, detalleEstadoPago, monto, idPagoMP));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
            }
        }
    }
}
