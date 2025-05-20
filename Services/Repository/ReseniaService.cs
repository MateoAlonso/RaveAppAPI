using ErrorOr;
using MySql.Data.MySqlClient;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Resenia;

namespace RaveAppAPI.Services.Repository
{
    public class ReseniaService : IReseniaService
    {
        private readonly string connectionString = DbHelper.GetConnectionString();
        public ErrorOr<Created> CreateResenia(Resenia resenia)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDCreateResenia, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.CreateReseniaParameters(resenia));
                    cmd.ExecuteNonQuery();
                    resenia.IdResenia = cmd.Parameters["p_idResenia"].Value.ToString();
                    return Result.Created;
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return Error.Unexpected();
            }
        }

        public ErrorOr<Deleted> DeleteResenia(string idResenia)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDDeleteResenia, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(ProcedureHelper.DeleteReseniaParameters(idResenia));
                    cmd.ExecuteNonQuery();
                    return Result.Deleted;
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return Error.Unexpected();
            }
        }

        public ErrorOr<List<AvgReseniaDTO>> GetAvgResenias(GetAvgReseniaRequest request)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetAvgResenias, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(ProcedureHelper.GetAvgReseniaParameters(request));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<AvgReseniaDTO> resenias = ReaderMaper.ReaderToObject<AvgReseniaDTO>(reader).ToList();
                            return resenias;
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

        public ErrorOr<List<Resenia>> GetResenias(GetReseniaRequest request)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetResenias, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.GetReseniasParameters(request));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<Resenia> resenias = ReaderMaper.ReaderToObject<Resenia>(reader).ToList();
                            return resenias;
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

        public ErrorOr<Updated> UpdateResenia(Resenia resenia)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDUpdateResenia, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.UpdateReseniaParameters(resenia));
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
