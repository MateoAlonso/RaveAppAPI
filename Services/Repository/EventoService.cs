using ErrorOr;
using MySql.Data.MySqlClient;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Evento;
using Error = ErrorOr.Error;

namespace RaveAppAPI.Services.Repository
{
    public class EventoService : IEventoService
    {
        private readonly string connectionString = EnvHelper.GetConnectionString();
        public ErrorOr<Created> CreateEvento(Evento evento)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDSetEvento, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.SetEventoParameters(evento));
                    cmd.ExecuteNonQuery();
                    int ok = Convert.ToInt32(cmd.Parameters["p_ok"].Value);
                    if (ok == 1)
                    {
                        evento.IdEvento = cmd.Parameters["p_idEvento"].Value.ToString();
                        return Result.Created;
                    }
                    else
                    {
                        return Error.Failure();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return Error.Unexpected();
            }
        }

        public ErrorOr<Created> SetFechas(Evento evento)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    ErrorOr<Created> result = Result.Created;
                    dbcon.Open();
                    MySqlCommand cmd;
                    foreach (Fecha fecha in evento.Fechas)
                    {
                        cmd = new(ProcedureHelper.PCDSetFechas, dbcon);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(ProcedureHelper.SetFechaParameters(fecha, evento.IdEvento));
                        cmd.ExecuteNonQuery();
                        int ok = Convert.ToInt32(cmd.Parameters["p_ok"].Value);
                        if (ok != 1)
                        {
                            result = Error.Failure("Error al insertar fechas");
                        }
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return Error.Unexpected();
            }
        }

        public ErrorOr<Deleted> DeleteEvento(string id)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();

                    MySqlCommand cmd = new(ProcedureHelper.PCDDeleteEvento, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.DeleteEventoParameters(id));
                    cmd.ExecuteNonQuery();
                    int ok = Convert.ToInt32(cmd.Parameters["p_ok"].Value);
                    if (ok == 1)
                    {
                        return Result.Deleted;
                    }
                    else
                    {
                        return Error.Failure();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return Error.Unexpected();
            }
        }
        public ErrorOr<List<Evento>> GetEventos(GetEventoRequest request)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetEventos, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.GetEventoParameters(request));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<Evento> eventos = ReaderMaper.ReaderToObjectRecursive<Evento>(reader).ToList();
                            eventos.ForEach(e =>
                            {
                                e.Genero = GetGeneros(e.IdEvento);
                                e.Fechas = GetFechas(e.IdEvento);
                            });
                            return eventos;
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

        private List<Fecha> GetFechas(string idEvento)
        {
            List<Fecha> fechas = new();
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetFechas, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(ProcedureHelper.GetFechasParameters(idEvento));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            fechas = ReaderMaper.ReaderToObject<Fecha>(reader).ToList();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
            }
            return fechas;
        }

        private List<int> GetGeneros(string idEvento)
        {
            List<int> generos = new();
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetGenerosByEvento, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(ProcedureHelper.GetGenerosByEventoParameters(idEvento));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            generos = ReaderMaper.ReaderToSimpleType<int>(reader).ToList();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
            }
            return generos;
        }
        public ErrorOr<Updated> UpdateEvento(Evento evento)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDUpdateEvento, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.UpdateEventoParameters(evento));
                    cmd.ExecuteNonQuery();
                    int ok = Convert.ToInt32(cmd.Parameters["p_ok"].Value);
                    if (ok == 1)
                    {
                        return Result.Updated;
                    }
                    else
                    {
                        return Error.Failure();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return Error.Unexpected();
            }
        }

        public ErrorOr<Updated> UpdateFechas(Evento evento)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    ErrorOr<Updated> result = Result.Updated;
                    dbcon.Open();
                    MySqlCommand cmd;
                    foreach (Fecha fecha in evento.Fechas)
                    {
                        cmd = new(ProcedureHelper.PCDUpdateFechas, dbcon);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(ProcedureHelper.UpdateFechaParameters(fecha));
                        cmd.ExecuteNonQuery();
                        int ok = Convert.ToInt32(cmd.Parameters["p_ok"].Value);
                        if (ok != 1)
                        {
                            result = Error.Failure();
                        }
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return Error.Unexpected();
            }
        }
        public ErrorOr<List<GeneroEvento>> GetGeneros()
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetGeneros, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<GeneroEvento> generos = ReaderMaper.ReaderToObject<GeneroEvento>(reader).ToList();
                            return generos;
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
        public ErrorOr<List<Estado>> GetEstadosEvento()
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetEstadosEvento, dbcon);
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
        public ErrorOr<List<Estado>> GetEstadosFecha()
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetEstadosFecha, dbcon);
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
    }
}
