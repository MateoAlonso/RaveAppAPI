using ErrorOr;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RaveAppAPI.Models;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using Error = ErrorOr.Error;

namespace RaveAppAPI.Services.Repository
{
    public class EventoService : IEventoService
    {
        private static readonly ConfigurationBuilder configurationBuilder = new();
        private static readonly IConfiguration config = configurationBuilder.AddUserSecrets<UsuarioService>().Build();
        private readonly string connectionString = config.GetConnectionString("Default");
        public ErrorOr<Created> CreateEvento(Evento evento)
        {
            using (MySqlConnection dbcon = new(connectionString))
            {
                try
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
                catch (Exception e)
                {
                    return Error.Unexpected();
                }
            }
        }

        public ErrorOr<Deleted> DeleteEvento(string id)
        {
            using (MySqlConnection dbcon = new(connectionString))
            {
                try
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
                catch (Exception)
                {
                    return Error.Unexpected();
                }
            }
        }
        public ErrorOr<Evento> GetEventoById(string id)
        {
            using (MySqlConnection dbcon = new(connectionString))
            {
                try
                {
                    dbcon.Open();
                    //TODO : Acceder SP
                    MySqlCommand cmd = new("", dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //TODO : Agregar parametros
                    MySqlDataReader datareader = cmd.ExecuteReader();

                }
                catch (Exception)
                {
                    return Error.Unexpected();
                }
            }
            return Error.NotFound();

        }
        public ErrorOr<Evento> GetEventoByNombre(string mail)
        {
            using (MySqlConnection dbcon = new(connectionString))
            {
                try
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
                catch (Exception e)
                {
                    return Error.Unexpected();
                }
            }
            return Error.NotFound();

        }

        public ErrorOr<Updated> UpdateEvento(Evento evento)
        {
            using (MySqlConnection dbcon = new(connectionString))
            {
                try
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
                catch (Exception)
                {
                    return Error.Unexpected();
                }
            }
        }
    }
}
