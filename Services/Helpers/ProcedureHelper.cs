using MySql.Data.MySqlClient;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.RequestModel.Evento;
using RaveAppAPI.Services.RequestModel.Fiesta;

namespace RaveAppAPI.Services.Helpers
{
    public static class ProcedureHelper
    {
        #region Usuario PCDS
        public const string PCDCreateUsuario = "PCD_USUARIOS_SetUsuario";
        public const string PCDDeleteUsuario = "PCD_USUARIOS_DeleteUsuario";
        public const string PCDGetUsuarioById = "PCD_USUARIOS_GetUsuarioById";
        public const string PCDGetUsuario = "PCD_USUARIOS_GetUsuario";
        public const string PCDUpdateUsuario = "PCD_USUARIOS_UpdateUsuario";
        public const string PCDGetRolesUsuario = "PCD_USUARIOS_GetRolesUsuario";
        #endregion

        #region Usuario Parameters
        public static MySqlParameter[] CreateUsuarioParameters(Usuario usuario)
        {
            return new MySqlParameter[]
            {
                new ("p_dsDireccion", usuario.Domicilio.Direccion),
                new ("p_nmLatitud", usuario.Domicilio.Latitud),
                new ("p_nmLongitud", usuario.Domicilio.Longitud),
                new ("p_dsProvincia", usuario.Domicilio.Provincia.Nombre),
                new ("p_cdProvincia", usuario.Domicilio.Provincia.Codigo),
                new ("p_dsMunicipio", usuario.Domicilio.Municipio.Nombre),
                new ("p_cdMunicipio", usuario.Domicilio.Municipio.Codigo),
                new ("p_dsLocalidad", usuario.Domicilio.Localidad.Nombre),
                new ("p_cdLocalidad", usuario.Domicilio.Localidad.Codigo),
                new ("p_dsNombre", usuario.Nombre),
                new ("p_dsApellido", usuario.Apellido),
                new ("p_dsCorreo", usuario.Correo),
                new ("p_nmDni", usuario.Dni),
                new ("p_nmTelefono", usuario.Telefono),
                new ("p_dsCbu", usuario.CBU),
                new ("p_dsNombreFantasia", usuario.NombreFantasia),
                new ("p_dsBio", usuario.Bio),
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
        public static MySqlParameter[] GetUsuarioParameters(string idUsuario, string mail, bool?     isActivo, int? rol)
        {
            return new MySqlParameter[]
            {
                new MySqlParameter ("p_idusuario", idUsuario),
                new MySqlParameter ("p_mail", mail),
                new MySqlParameter ("p_isactivo", (isActivo ?? true) ? 1 : 0),
                new MySqlParameter ("p_cdrol", rol)
            };
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
                new ("p_idUsuario", usuario.IdUsuario),
                new ("p_dsDireccion", usuario.Domicilio.Direccion),
                new ("p_nmLatitud", usuario.Domicilio.Latitud),
                new ("p_nmLongitud", usuario.Domicilio.Longitud),
                new ("p_dsProvincia", usuario.Domicilio.Provincia.Nombre),
                new ("p_cdProvincia", usuario.Domicilio.Provincia.Codigo),
                new ("p_dsMunicipio", usuario.Domicilio.Municipio.Nombre),
                new ("p_cdMunicipio", usuario.Domicilio.Municipio.Codigo),
                new ("p_dsLocalidad", usuario.Domicilio.Localidad.Nombre),
                new ("p_cdLocalidad", usuario.Domicilio.Localidad.Codigo),
                new ("p_dsNombre", usuario.Nombre),
                new ("p_dsApellido", usuario.Apellido),
                new ("p_dsCorreo", usuario.Correo),
                new ("p_nmDni", usuario.Dni),
                new ("p_nmTelefono", usuario.Telefono),
                new ("p_dsCbu", usuario.CBU),
                new ("p_dsNombreFantasia", usuario.NombreFantasia),
                new ("p_dsBio", usuario.Bio),
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

        #region Evento PCDS
        public const string PCDGetEventos = "PCD_EVENTOS_GetEventos";
        public const string PCDGetGeneros = "PCD_EVENTOS_GetGeneros";
        public const string PCDGetEstadosEvento = "PCD_EVENTOS_GetEstadosEvento";
        public const string PCDGetEstadosFecha = "PCD_EVENTOS_GetEstadosFecha";
        public const string PCDGetFechas = "PCD_EVENTOS_GetFechasEvento";
        public const string PCDSetFechas = "PCD_EVENTOS_SetFechaEvento";
        public const string PCDUpdateFechas = "PCD_EVENTOS_UpdateFechaEvento";
        public const string PCDGetEventosById = "PCD_EVENTOS_GetEventosById";
        public const string PCDGetEventosByEstado = "PCD_EVENTOS_GetEventosByEstado";
        public const string PCDSetEvento = "PCD_EVENTOS_SetEvento";
        public const string PCDDeleteEvento = "PCD_EVENTOS_DeleteEvento";
        public const string PCDUpdateEvento = "PCD_EVENTOS_UpdateEvento";
        #endregion

        #region Evento Parameters
        public static MySqlParameter[] GetEventoParameters(GetEventoRequest eventoRequest)
        {
            string cdGeneros = null;
            if (eventoRequest.Genero != null)
            {
               cdGeneros = string.Join(",", eventoRequest.Genero.Select(e => e));
            }
            return new MySqlParameter[]
            {
                new MySqlParameter ("p_idEvento", eventoRequest.IdEvento),
                new MySqlParameter ("p_cdEstadoEvento", eventoRequest.Estado),
                new MySqlParameter ("p_cdProvincia", eventoRequest.CodigoProvincia),
                new MySqlParameter ("p_cdLocalidad", eventoRequest.CodigoLocalidad),
                new MySqlParameter ("p_cdMunicipio", eventoRequest.CodigoMunicipio),
                new MySqlParameter ("p_cdGeneroList", cdGeneros),
                new MySqlParameter ("p_isAfter", eventoRequest.IsAfter),
                new MySqlParameter ("p_isLgbt", eventoRequest.IsLgbt),
                new MySqlParameter ("p_idFiesta", eventoRequest.idFiesta),
            };
        }
        public static MySqlParameter GetGenerosParameters(string idEvento) 
        {
            return new MySqlParameter("p_idEvento", idEvento);
        }
        public static MySqlParameter[] SetEventoParameters(Evento evento)
        {
            string idArtistas = string.Join(",", evento.Artistas.Select(a => a.IdUsuario ?? string.Empty));
            string cdGeneros = string.Join(",", evento.Genero.Select(e => e));
            return new MySqlParameter[]
            {
                new MySqlParameter ("p_idUsuario", evento.Usuario.IdUsuario),
                new MySqlParameter ("p_idArtistaList", idArtistas),
                new MySqlParameter ("p_dsDireccion", evento.Domicilio.Direccion),
                new MySqlParameter ("p_nmLatitud", evento.Domicilio.Latitud),
                new MySqlParameter ("p_nmLongitud", evento.Domicilio.Longitud),
                new MySqlParameter ("p_dsProvincia", evento.Domicilio.Provincia.Nombre),
                new MySqlParameter ("p_cdProvincia", evento.Domicilio.Provincia.Codigo),
                new MySqlParameter ("p_dsMunicipio", evento.Domicilio.Municipio.Nombre),
                new MySqlParameter ("p_cdMunicipio", evento.Domicilio.Municipio.Codigo),
                new MySqlParameter ("p_dsLocalidad", evento.Domicilio.Localidad.Nombre),
                new MySqlParameter ("p_cdLocalidad", evento.Domicilio.Localidad.Codigo),
                new MySqlParameter ("p_dsNombre", evento.Nombre),
                new MySqlParameter ("p_dsEvento", evento.Descripcion),
                new MySqlParameter ("p_cdGeneroList", cdGeneros),
                new MySqlParameter ("p_isAfter", evento.IsAfter),
                new MySqlParameter ("p_isLgbt", evento.IsLgbt),
                new MySqlParameter ("p_dtInicioEvento", evento.InicioEvento),
                new MySqlParameter ("p_dtFinEvento", evento.FinEvento),
                new MySqlParameter ("p_cdestado", evento.CdEstado),
                new MySqlParameter ("p_idFiesta", evento.IdFiesta),
                new MySqlParameter ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                new MySqlParameter ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output },
                new MySqlParameter ("p_idEvento", MySqlDbType.VarChar, 36) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        public static MySqlParameter[] UpdateEventoParameters(Evento evento)
        {
            string idArtistas = string.Join(",", evento.Artistas.Select(a => a.IdUsuario ?? string.Empty));
            string cdGeneros = string.Join(",", evento.Genero.Select(e => e));
            return new MySqlParameter[]
            {
                new MySqlParameter ("p_idEvento", evento.IdEvento),
                new MySqlParameter ("p_idArtistaList", idArtistas),
                new MySqlParameter ("p_dsDireccion", evento.Domicilio.Direccion),
                new MySqlParameter ("p_nmLatitud", evento.Domicilio.Latitud),
                new MySqlParameter ("p_nmLongitud", evento.Domicilio.Longitud),
                new MySqlParameter ("p_dsProvincia", evento.Domicilio.Provincia.Nombre),
                new MySqlParameter ("p_cdProvincia", evento.Domicilio.Provincia.Codigo),
                new MySqlParameter ("p_dsMunicipio", evento.Domicilio.Municipio.Nombre),
                new MySqlParameter ("p_cdMunicipio", evento.Domicilio.Municipio.Codigo),
                new MySqlParameter ("p_dsLocalidad", evento.Domicilio.Localidad.Nombre),
                new MySqlParameter ("p_cdLocalidad", evento.Domicilio.Localidad.Codigo),
                new MySqlParameter ("p_dsNombre", evento.Nombre),
                new MySqlParameter ("p_dsEvento", evento.Descripcion),
                new MySqlParameter ("p_cdGeneroList", cdGeneros),
                new MySqlParameter ("p_isAfter", evento.IsAfter),
                new MySqlParameter ("p_isLgbt", evento.IsLgbt),
                new MySqlParameter ("p_dtInicioEvento", evento.InicioEvento),
                new MySqlParameter ("p_dtFinEvento", evento.FinEvento),
                new MySqlParameter ("p_cdEstado", evento.CdEstado),
                new MySqlParameter ("p_idFiesta", evento.IdFiesta),
                new MySqlParameter ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                new MySqlParameter ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output }
            };
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

        public static MySqlParameter GetFechasParameters(string idEvento)
        {
            return new MySqlParameter("p_idEvento", idEvento);
        }

        public static MySqlParameter[] SetFechaParameters(Fecha fecha, string idEvento)
        {
            return new MySqlParameter[]
            {
                new ("p_idEvento", idEvento),
                new ("p_dtInicio", fecha.Inicio),
                new ("p_dtFin", fecha.Fin),
                new ("p_cdEstado", fecha.Estado),
                new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                new ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        public static MySqlParameter[] UpdateFechaParameters(Fecha fecha, string idEvento)
        {
            return new MySqlParameter[]
            {
                new ("p_idEvento", idEvento),
                new ("p_dtInicio", fecha.Inicio),
                new ("p_dtFin", fecha.Fin),
                new ("p_cdEstado", fecha.Estado),
                new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                new ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        #endregion

        #region Fiesta PCDS
        public const string PCDCreateFiesta = "PCD_FIESTAS_SetFiesta";
        public const string PCDUpdateFiesta = "PCD_FIESTAS_UpdateFiesta";
        public const string PCDGetFiestas = "PCD_FIESTAS_GetFiesta";
        public const string PCDDeleteFiesta = "PCD_FIESTAS_DeleteFiesta";
        #endregion

        #region Fiesta Parameters
        public static MySqlParameter[] GetFiestasParameters(GetFiestaRequest fiestaRequest)
        {
            return new MySqlParameter[]
            {
                new MySqlParameter ("p_idFiesta", fiestaRequest.IdFiesta),
                new MySqlParameter ("p_idUsuario", fiestaRequest.IdUsuario)
            };
        }
        public static MySqlParameter[] SetFiestaParameters(Fiesta fiesta)
        {
            return new MySqlParameter[]
            {
                new MySqlParameter ("p_idUsuario", fiesta.IdUsuario),
                new MySqlParameter ("p_dsNombre", fiesta.DsNombre),
                new MySqlParameter ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                new MySqlParameter ("p_error", MySqlDbType.Text) { Direction = System.Data.ParameterDirection.Output },
                new MySqlParameter ("p_idFiesta", MySqlDbType.VarChar, 36) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        public static MySqlParameter[] UpdateFiestaParameters(Fiesta fiesta)
        {
            return new MySqlParameter[]
            {
                new MySqlParameter ("idFiesta", fiesta.IdFiesta),
                new MySqlParameter ("p_dsNombre", fiesta.DsNombre),
                new MySqlParameter ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                new MySqlParameter ("p_error", MySqlDbType.Text) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        public static MySqlParameter DeleteFiestaParameters(string idFiesta)
        {
            return new MySqlParameter ("p_idFiesta", idFiesta);
        }
        #endregion
    }
}
