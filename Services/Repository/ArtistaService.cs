using ErrorOr;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Artista;

namespace RaveAppAPI.Services.Repository
{
    public class ArtistaService : IArtistaService
    {
        private readonly string connectionString = EnvHelper.GetConnectionString();

        public ErrorOr<Created> CreateArtista(Artista artista)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDCreateArtista, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.CreateArtistaParameters(artista));
                    cmd.ExecuteNonQuery();
                    artista.IdArtista = cmd.Parameters["p_idArtista"].Value.ToString();
                    return Result.Created;
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return Error.Unexpected();
            }
        }

        public ErrorOr<Deleted> DeleteArtista(string idArtista)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDCDeleteArtista, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(ProcedureHelper.DeleteArtistasParameters(idArtista));
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

        public ErrorOr<List<Artista>> GetArtistas(GetArtistaRequest request)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetArtistas, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.GetArtistasParameters(request));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<Artista> artistas = ReaderMaper.ReaderToObjectRecursive<Artista>(reader).ToList();
                            return artistas;
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

        public int GetCantLikesArtista(string id)
        {
            int res = 0;
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDCGetCantLikesArtista, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.GetCantLikesArtistaParameters(id));
                    cmd.ExecuteNonQuery();
                    res = Convert.ToInt32(cmd.Parameters["p_Cant"].Value);
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
            }
            return res;
        }

        public ErrorOr<List<string>> GetImgLikesArtistas(string id)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetImgLikesArtistas, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(ProcedureHelper.GetImgLikesArtistaParameters(id));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<string> usuarios = ReaderMaper.ReaderToSimpleType<string>(reader).ToList();
                            return usuarios;
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

        public ErrorOr<Updated> UpdateArtista(Artista artista)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDUpdateArtista, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.UpdateArtistaParameters(artista));
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
