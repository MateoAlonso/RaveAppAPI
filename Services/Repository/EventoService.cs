using ErrorOr;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Evento;
using Serilog.Events;
using Error = ErrorOr.Error;

namespace RaveAppAPI.Services.Repository
{
    public class EventoService : IEventoService
    {
        private readonly string connectionString = DbHelper.GetConnectionString();
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
                    cmd.Parameters.AddRange(ProcedureHelper.GetEventoParameters(request.IdEvento, request.Estado, request.CodigoProvincia, request.Genero, request.IsAfter, request.IsLgbt));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<Evento> eventos = ReaderMaper.ReaderToObject<Evento>(reader).ToList();
                            eventos.ForEach(e => e.Genero = GetGeneros(e.IdEvento));
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
        private List<int> GetGeneros(string idEvento) 
        {
            List<int> generos = new();
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetGeneros, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(ProcedureHelper.GetGenerosParameters(idEvento));
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
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return Error.Unexpected();
            }
        }
    }
}
