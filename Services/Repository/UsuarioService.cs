using ErrorOr;
using RaveAppAPI.Models;
using RaveAppAPI.Services.Repository.Contracts;
using MySql.Data.MySqlClient;
using Error = ErrorOr.Error;
using Microsoft.Extensions.Configuration;

namespace RaveAppAPI.Services.Repository
{
    public class UsuarioService : IUsuarioService
    {
        private static readonly ConfigurationBuilder configurationBuilder = new();
        private static readonly IConfiguration config = configurationBuilder.AddUserSecrets<UsuarioService>().Build();
        private readonly string connectionString = config.GetConnectionString("Default");
        public ErrorOr<Created> CreateUsuario(Usuario usuario)
        {
            using (MySqlConnection dbcon = new(connectionString))
            {
                try
                {
                    dbcon.Open();
                    MySqlCommand cmd = new("PCD_USUARIOS_SetUsuario", dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(new MySqlParameter[] {
                        new ("p_provincia", usuario.Domicilio.Localidad.Provincia.DsProvincia),
                        new ("p_localidad", usuario.Domicilio.Localidad.DsNombre),
                        new ("p_calle", usuario.Domicilio.DsCalle),
                        new ("p_altura", usuario.Domicilio.NmAltura),
                        new ("p_pisodepartamento", usuario.Domicilio.DsPisoDepartamento),
                        new ("p_nombreusuario", usuario.DsNombre),
                        new ("p_apellido", usuario.DsApellido),
                        new ("p_correo", usuario.DsCorreo),
                        new ("p_nmdni", usuario.NmDni),
                        new ("p_nmtelefono", usuario.NmTelefono),
                        new ("p_dscbu", usuario.DsCBU),
                        new ("p_isorganizador", usuario.IsOrganizador),
                        new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                        new ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output } }
                        );
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

        public ErrorOr<Deleted> DeleteUsuario(string id)
        {
            using (MySqlConnection dbcon = new(connectionString))
            {
                try
                {
                    dbcon.Open();
                    MySqlCommand cmd = new("PCD_USUARIOS_DeleteUsuario", dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(new MySqlParameter[] {
                        new ("p_idusuario", id),
                        new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                        new ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output } }
                        );
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
        public ErrorOr<Usuario> GetUsuarioById(string id)
        {
            using (MySqlConnection dbcon = new(connectionString))
            {
                try
                {
                    dbcon.Open();
                    MySqlCommand cmd = new("PCD_Usuarios_GetUsuarioById", dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("p_idusuario", id));
                    MySqlDataReader datareader = cmd.ExecuteReader();

                }
                catch (Exception)
                {
                    return Error.Unexpected();
                }
            }
            return Error.NotFound();
            
        }
        public ErrorOr<Usuario> GetUsuarioByMail(string mail)
        {
            using (MySqlConnection dbcon = new(connectionString))
            {
                try
                {
                    dbcon.Open();
                    MySqlCommand cmd = new("PCD_Usuarios_GetUsuarioByCorreo", dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("p_correo", mail));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.HasRows)
                            {
                                //TODO Data mapper
                                //return Usuario.Devolver(reader.GetString("dsprovincia"), reader.GetString("dslocalidad"), reader.GetString("dscalle"), reader.GetString("nmaltura"), reader.GetString("dspisodepartamento"), reader.GetString("dsnombre"), reader.GetString("dsapellido"), reader.GetString("dscorreo"), reader.GetString("dscbu"), reader.GetString("nmdni"), reader.GetString("nmtelefono"), reader.GetInt32("isorganizador"), reader.GetInt32("isactivo"), reader.GetDateTime("dtalta"), reader.GetDateTime("dtbaja"));

                            }
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

        public ErrorOr<Updated> UpdateUsuario(Usuario usuario)
        {
            using (MySqlConnection dbcon = new(connectionString))
            {
                try
                {
                    dbcon.Open();
                    MySqlCommand cmd = new("PCD_USUARIOS_UpdateUsuario", dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(new MySqlParameter[] {
                        new ("p_idusuario", usuario.IdUsuario),
                        new ("p_provincia", usuario.Domicilio.Localidad.Provincia.DsProvincia),
                        new ("p_localidad", usuario.Domicilio.Localidad.DsNombre),
                        new ("p_calle", usuario.Domicilio.DsCalle),
                        new ("p_altura", usuario.Domicilio.NmAltura),
                        new ("p_pisodepartamento", usuario.Domicilio.DsPisoDepartamento),
                        new ("p_nombreusuario", usuario.DsNombre),
                        new ("p_apellido", usuario.DsApellido),
                        new ("p_correo", usuario.DsCorreo),
                        new ("p_nmdni", usuario.NmDni),
                        new ("p_nmtelefono", usuario.NmTelefono),
                        new ("p_dscbu", usuario.DsCBU),
                        new ("p_isorganizador", usuario.IsOrganizador),
                        new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                        new ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output } }
                        );
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
