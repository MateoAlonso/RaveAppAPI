﻿using ErrorOr;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Artista;
using RaveAppAPI.Services.RequestModel.User;
using Error = ErrorOr.Error;

namespace RaveAppAPI.Services.Repository
{
    public class UsuarioService : IUsuarioService
    {
        private readonly string connectionString = EnvHelper.GetConnectionString();

        public ErrorOr<Created> CreateUsuario(Usuario usuario)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDCreateUsuario, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.CreateUsuarioParameters(usuario));
                    cmd.ExecuteNonQuery();
                    int ok = Convert.ToInt32(cmd.Parameters["p_ok"].Value);
                    if (ok == 1)
                    {
                        usuario.IdUsuario = cmd.Parameters["p_idUsuario"].Value.ToString();
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
        public ErrorOr<Deleted> DeleteUsuario(string id)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDDeleteUsuario, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.DeleteUsuarioParameters(id));
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
        public ErrorOr<List<Usuario>> GetUsuario(GetUsuarioRequest request)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetUsuario, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.GetUsuarioParameters(request.IdUsuario, request.Mail, request.IsActivo, request.Rol));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<Usuario> usuarios = ReaderMaper.ReaderToObjectRecursive<Usuario>(reader).ToList();
                            usuarios.ForEach(u => u.Roles = (GetRolesUsuario(u.IdUsuario).Value));
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
        public ErrorOr<Updated> UpdateUsuario(Usuario usuario)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDUpdateUsuario, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.UpdateUsuarioParameters(usuario));
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
        public ErrorOr<List<RolesUsuario>> GetRolesUsuario(string idusuario)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetRolesUsuario, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(ProcedureHelper.GetRolesUsuarioParameters(idusuario));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<RolesUsuario> roles = ReaderMaper.ReaderToObject<RolesUsuario>(reader).ToList();
                            return roles;
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

        public ErrorOr<bool> Login(LoginUsuarioRequest request)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDLoginUsuario, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.GetLoginUsuarioParameters(request));
                    cmd.ExecuteNonQuery();
                    int ok = Convert.ToInt32(cmd.Parameters["p_ok"].Value);
                    if (ok == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return Error.Unexpected();
            }
        }

        public ErrorOr<Updated> ResetPass(ResetPassUsuarioRequest request)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDResetPassUsuario, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.ResetPassUsuarioParameters(request));
                    cmd.ExecuteNonQuery();
                    int ok = Convert.ToInt32(cmd.Parameters["p_ok"].Value);
                    if (ok == 1)
                    {
                        return Result.Updated;
                    }
                    else
                    {
                        return Error.Failure("Usuario o contraseña incorrecta");
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return Error.Unexpected();
            }
        }

        public ErrorOr<Updated> RecoverPass(RecoverPassUsuarioRequest request)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDRecoverPassUsuario, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.RecoverPassUsuarioParameters(request));
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
        public ErrorOr<Updated> EventoFavorito(EventoFavoritoRequest request)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDToggleEventoFavorito, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.EventoFavoritoParameters(request));
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

        public ErrorOr<Updated> ArtistaFavorito(ArtistaFavoritoRequest request)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDToggleLikeArtista, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(ProcedureHelper.ArtistaFavoritoParameters(request));
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

        public ErrorOr<List<string>> GetEventosFavoritos(string idUsuario)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetEventosFavoritos, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(ProcedureHelper.GetEventosFavoritosParameters(idUsuario));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return ReaderMaper.ReaderToSimpleType<string>(reader).ToList();
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
