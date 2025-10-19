using ErrorOr;
using MySql.Data.MySqlClient;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Reporte;

namespace RaveAppAPI.Services.Repository
{
    public class ReporteService : IReporteService
    {
        private readonly string connectionString = EnvHelper.GetConnectionString();
        public ErrorOr<List<VentasEventoDTO>> GetReporteVentasEvento(ReporteVentasEventoRequest request)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.GetVentasEvento, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.GetVentasEventoParameters(request));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<VentasEventoDTO> ventas = ReaderMaper.ReaderToObjectRecursive<VentasEventoDTO>(reader).ToList();
                            return ventas;
                        }
                        else
                        {
                            return Error.NotFound();
                        }
                    }

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
