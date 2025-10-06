using ErrorOr;
using MySql.Data.MySqlClient;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;

namespace RaveAppAPI.Services.Repository
{
    public class PagoService : IPagoService
    {
        private readonly string connectionString = EnvHelper.GetConnectionString();

        public ErrorOr<Updated> AnularCompra(string idCompra)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.AnularCompra, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(ProcedureHelper.AnularCompraParameters(idCompra));
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

        public ErrorOr<List<string>> FinalizarCompra(string idCompra, int cdMedioPago)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.FinalizarCompra, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.FinalizarCompraParameters(idCompra, cdMedioPago));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<string> entradas = ReaderMaper.ReaderToSimpleType<string>(reader).ToList();
                            return entradas;
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

        public ErrorOr<List<DatosReembolsoDTO>> GetDatosReembolso(string idCompra)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.GetDatosReembolso, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(ProcedureHelper.GetDatosReembolsoParameters(idCompra));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<DatosReembolsoDTO> datos = ReaderMaper.ReaderToObjectRecursive<DatosReembolsoDTO>(reader).ToList();
                            return datos;
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

        public ErrorOr<Updated> PendienteCompra(string idCompra, decimal subTotal, decimal cargoServicio)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PendienteCompra, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.PendienteCompraParameters(idCompra, subTotal, cargoServicio));
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

        public void Reembolso(string idCompra)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.Reembolso, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(ProcedureHelper.ReembolsoParameters(idCompra));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
            }
        }

        public void RefrescarTimerReserva(string idCompra)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.RefreshTimerReserva, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(ProcedureHelper.RefreshTimerReservaParameters(idCompra));
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
