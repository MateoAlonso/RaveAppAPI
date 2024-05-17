using ErrorOr;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using RaveAppAPI.Models;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.Repository
{
    public class NoticiaService : INoticiaService
    {
        private readonly string connectionString = Environment.GetEnvironmentVariable("dbcon", EnvironmentVariableTarget.Machine);
        public ErrorOr<Created> CreateNoticia(Noticia noticia)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new("PCD_NOTICIAS_SetNoticia", dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(new MySqlParameter[]{
                            new ("p_titulo", noticia.Titulo),
                            new ("p_contenido", noticia.Contenido),
                            new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                            new ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output }
                    });
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
            catch (Exception)
            {
                return Error.Unexpected();
            }
        }

        public ErrorOr<Deleted> DeleteNoticia(string idNoticia)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new("PCD_NOTICIAS_DeleteNoticia", dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(new MySqlParameter[] {
                            new ("p_idnoticia", idNoticia),
                            new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                            new ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output } 
                    });
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

        public ErrorOr<List<Noticia>> GetNoticias()
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new("PCD_NOTICIAS_GetNoticias", dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<Noticia> noticias = ReaderMaper.ReaderToObject<Noticia>(reader).ToList();
                            return noticias;
                        }
                        else
                        {
                            return Error.NotFound();
                        }
                    }
                    
                }
            }
            catch (Exception)
            {
                return Error.Unexpected();
            }
        }

        public ErrorOr<Updated> UpdateNoticia(Noticia noticia)
        {
            throw new NotImplementedException();
        }
    }
}
