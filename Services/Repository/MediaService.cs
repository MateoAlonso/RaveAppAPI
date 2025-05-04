using ErrorOr;
using MySql.Data.MySqlClient;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;

namespace RaveAppAPI.Services.Repository
{
    public class MediaService : IMediaService
    {
        private readonly string connectionString = DbHelper.GetConnectionString();
        public ErrorOr<Created> CreateMedia(Media media)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDCreateMedia, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.CreateMediaParameters(media));
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

        public ErrorOr<Deleted> DeleteMedia(string idMedia)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDDeleteMedia, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(ProcedureHelper.DeleteMediaParameters(idMedia));
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

        public ErrorOr<List<Media>> GetMedia(string idMediaEntidad)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetMedia, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(ProcedureHelper.GetMediaParameters(idMediaEntidad));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<Media> medias = ReaderMaper.ReaderToObject<Media>(reader).ToList();
                            return medias;
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
