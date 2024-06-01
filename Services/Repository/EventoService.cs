using ErrorOr;
using MySql.Data.MySqlClient;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using Error = ErrorOr.Error;

namespace RaveAppAPI.Services.Repository
{
    public class EventoService : IEventoService
    {
        private readonly string connectionString = Environment.GetEnvironmentVariable("dbcon", EnvironmentVariableTarget.Machine);
        public ErrorOr<Created> CreateEvento(Evento evento)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    //TODO : Acceder SP
                    MySqlCommand cmd = new("", dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //TODO : Agregar parametros
                    cmd.ExecuteNonQuery();
                    int ok = Convert.ToInt32(cmd.Parameters["p_ok"].Value);
                    if (ok == 1)
                    {
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
                    //TODO : Acceder SP
                    MySqlCommand cmd = new("", dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //TODO : Agregar parametros
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
            catch (Exception)
            {
                return Error.Unexpected();
            }
        }
        public ErrorOr<Evento> GetEventoById(string id)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    //TODO : Acceder SP
                    MySqlCommand cmd = new("", dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //TODO : Agregar parametros
                    MySqlDataReader datareader = cmd.ExecuteReader();

                }
                return Error.NotFound();
            }
            catch (Exception)
            {
                return Error.Unexpected();
            }

        }
        public ErrorOr<List<Evento>> GetEventosByEstado(string estado)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    //TODO : Acceder SP
                    MySqlCommand cmd = new("", dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //TODO : Agregar parametros
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<Usuario> usuarios = ReaderMaper.ReaderToObject<Usuario>(reader).ToList();
                        }
                        else
                        {
                            return Error.NotFound();
                        }
                    }
                }
                return Error.NotFound();
            }
            catch (Exception e)
            {
                return Error.Unexpected();
            }

        }

        public ErrorOr<Updated> UpdateEvento(Evento evento)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    //TODO : Acceder SP
                    MySqlCommand cmd = new("", dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //TODO : Agregar parametros
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
            catch (Exception)
            {
                return Error.Unexpected();
            }
        }
    }
}
