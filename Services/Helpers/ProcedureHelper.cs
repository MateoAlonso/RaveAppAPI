using MySql.Data.MySqlClient;
using RaveAppAPI.Services.Models;

namespace RaveAppAPI.Services.Helpers
{
    public static class ProcedureHelper
    {
        #region Usuario PCDS
        public const string PCDCreateUsuario = "PCD_USUARIOS_SetUsuario";
        public const string PCDDeleteUsuario = "PCD_USUARIOS_DeleteUsuario";
        public const string PCDGetUsuarioById = "PCD_USUARIOS_GetUsuarioById";
        public const string PCDGetUsuarioByMail = "PCD_USUARIOS_GetUsuarioByMail";
        public const string PCDUpdateUsuario = "PCD_USUARIOS_UpdateUsuario";
        public const string PCDGetRolesUsuarioByMail = "PCD_USUARIOS_GetRolesUsuarioByMail";
        #endregion

        #region Usuario Parameters
        public static MySqlParameter[] CreateUsuarioParameters(Usuario usuario)
        {
            return new MySqlParameter[]
            {
                new ("p_cdprovincia", usuario.Domicilio.Localidad.Provincia.Codigo),
                new ("p_localidad", usuario.Domicilio.Localidad.Nombre),
                new ("p_calle", usuario.Domicilio.Calle),
                new ("p_altura", usuario.Domicilio.Altura),
                new ("p_pisodepartamento", usuario.Domicilio.PisoDepartamento),
                new ("p_nombreusuario", usuario.Nombre),
                new ("p_apellido", usuario.Apellido),
                new ("p_correo", usuario.Correo),
                new ("p_nmdni", usuario.Dni),
                new ("p_nmtelefono", usuario.Telefono),
                new ("p_dscbu", usuario.CBU),
                new ("p_dsnombrefantasia", usuario.NombreFantasia),
                new ("p_dsbio", usuario.Bio),
                new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                new ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        public static MySqlParameter[] DeleteUsuarioParameters(string id)
        {
            return new MySqlParameter[]
            {
                new ("p_idusuario", id),
                new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                new ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        public static MySqlParameter GetUsuarioByIdParameters(string id)
        {
            return new MySqlParameter("p_idusuario", id);
        }
        public static MySqlParameter GetUsuarioByMailParameters(string mail)
        {
            return new MySqlParameter("p_correo", mail);
        }
        public static MySqlParameter GetRolesUsuarioByMailParameters(string mail)
        {
            return new MySqlParameter("p_correo", mail);
        }
        public static MySqlParameter[] UpdateUsuarioParameters(Usuario usuario)
        {
            return new MySqlParameter[]
            {
                new ("p_idusuario", usuario.IdUsuario),
                new ("p_provincia", usuario.Domicilio.Localidad.Provincia.Nombre),
                new ("p_localidad", usuario.Domicilio.Localidad.Nombre),
                new ("p_calle", usuario.Domicilio.Calle),
                new ("p_altura", usuario.Domicilio.Altura),
                new ("p_pisodepartamento", usuario.Domicilio.PisoDepartamento),
                new ("p_nombreusuario", usuario.Nombre),
                new ("p_apellido", usuario.Apellido),
                new ("p_correo", usuario.Correo),
                new ("p_nmdni", usuario.Dni),
                new ("p_nmtelefono", usuario.Telefono),
                new ("p_dscbu", usuario.CBU),
                new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                new ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        #endregion

        #region Noticia PCDS
        public const string PCDCreateNoticia = "PCD_NOTICIAS_SetNoticia";
        public const string PCDDeleteNoticia = "PCD_NOTICIAS_DeleteNoticia";
        public const string PCDGetNoticias = "PCD_NOTICIAS_GetNoticias";
        public const string PCDGetNoticiaById = "PCD_NOTICIAS_UpdateNoticia";
        #endregion

        #region Noticia Parameters
        public static MySqlParameter[] CreateNoticiaParameters(Noticia noticia)
        {
            return new MySqlParameter[]
            {
                new ("p_titulo", noticia.Titulo),
                new ("p_contenido", noticia.Contenido),
                new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                new ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        public static MySqlParameter[] DeleteNoticiaParameters(string id)
        {
            return new MySqlParameter[]
            {
                new ("p_idnoticia", id),
                new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                new ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        #endregion

        #region Noticia PCDS
        public const string PCDGetEventos = "PCD_EVENTOS_GetEventos";
        public const string PCDGetEventosById = "PCD_EVENTOS_GetEventosById";
        public const string PCDGetEventosByEstado = "PCD_EVENTOS_GetEventosByEstado";
        #endregion
        #region Evento Parameters
        public static MySqlParameter GetEventosByIdParametes(string idEvento)
        {
            return new MySqlParameter("p_idevento", idEvento);
        }
        public static MySqlParameter GetEventosByEstadoParametes(int cdEstado)
        {
            return new MySqlParameter("p_cdestado", cdEstado);
        }
        public static MySqlParameter[] DeleteEventoParameters(string idEvento)
        {
            return new MySqlParameter[]
            {
                new ("p_idevento", idEvento),
                new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                new ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        public static MySqlParameter[] UpdateEventoParameters(Evento evento)
        {
            return new MySqlParameter[]
            {
                new ("p_idevento", evento.IdEvento),
                new ("p_dsnombre", evento.Nombre),
                new ("p_dsevento", evento.Descripcion),
                new ("p_cdgenero", evento.Genero),
                new ("p_cdestado", evento.Estado),
                new ("p_isafter", evento.IsAfter),
                new ("p_islgbt", evento.IsLgbt),
                new ("p_dtinicioventa", evento.InicioVenta),
                new ("p_dtfinventa", evento.FinVenta),
                new ("p_dtinicioevento", evento.InicioEvento),
                new ("p_dtfinevento", evento.FinEvento),
                new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                new ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        public static MySqlParameter[] CreateEventoParameters(Evento evento)
        {
            return new MySqlParameter[]
            {
                new ("p_dsnombre", evento.Nombre),
                new ("p_dsevento", evento.Descripcion),
                new ("p_cdgenero", evento.Genero),
                new ("p_cdestado", evento.Estado),
                new ("p_isafter", evento.IsAfter),
                new ("p_islgbt", evento.IsLgbt),
                new ("p_dtinicioventa", evento.InicioVenta),
                new ("p_dtfinventa", evento.FinVenta),
                new ("p_dtinicioevento", evento.InicioEvento),
                new ("p_dtfinevento", evento.FinEvento),
                new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                new ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        #endregion
    }
}
