using ErrorOr;
using MySql.Data.MySqlClient;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Entrada;

namespace RaveAppAPI.Services.Repository
{
    public class EntradaService : IEntradaService
    {
        private readonly string connectionString = EnvHelper.GetConnectionString();

        public ErrorOr<Updated> CancelarReserva(string idCompra)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDCancelarReserva, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(ProcedureHelper.CancelarReservaParameters(idCompra));
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

        public ErrorOr<Created> CreateEntrada(Entrada entrada)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDCreateEntrada, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.CreateEntradaParameters(entrada));
                    cmd.ExecuteNonQuery();
                    return Result.Created;
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return Error.Unexpected();
            }
        }

        public ErrorOr<List<Entrada>> GetEntradasFecha(GetEntradasFechaRequest request)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetEntradasFecha, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.GetEntradasFechaParameters(request));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<Entrada> entradas = ReaderMaper.ReaderToObjectRecursive<Entrada>(reader).ToList();
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

        public ErrorOr<List<Estado>> GetEstadosEntrada()
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetEstadosEntrada, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<Estado> estados = ReaderMaper.ReaderToObject<Estado>(reader).ToList();
                            return estados;
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

        public ErrorOr<List<GetReservaActivaDTO>> GetReservaActiva(string idUsuario)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetEntradas, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(ProcedureHelper.GetEntradasParameters(idUsuario));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<GetReservaActivaDTO> reserva = ReaderMaper.ReaderToObject<GetReservaActivaDTO>(reader).ToList();
                            return reserva;
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

        public ErrorOr<List<Tipo>> GetTiposEntrada()
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetTipoEntradas, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<Tipo> tipos = ReaderMaper.ReaderToObject<Tipo>(reader).ToList();
                            return tipos;
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

        public ErrorOr<string> ReservarEntradas(ReservarEntradasRequest request)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDReservarEntradas, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.ReservarEntradasParameters(request));
                    cmd.ExecuteNonQuery();
                    return cmd.Parameters["p_idCompra"].Value.ToString();
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return Error.Unexpected();
            }
        }

        public ErrorOr<Updated> UpdateEntrada(Entrada entrada)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDUpdateEntradas, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //TODO Cambios DB Reservar entradas
                    cmd.Parameters.AddRange(ProcedureHelper.UpdateEntradasParameters(entrada));
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
