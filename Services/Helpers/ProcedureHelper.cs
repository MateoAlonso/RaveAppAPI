using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.RequestModel.Entrada;
using RaveAppAPI.Services.RequestModel.Artista;
using RaveAppAPI.Services.RequestModel.Evento;
using RaveAppAPI.Services.RequestModel.Fiesta;
using RaveAppAPI.Services.RequestModel.Resenia;
using RaveAppAPI.Services.RequestModel.User;

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
        public const string PCDLoginUsuario = "PCD_USUARIOS_LoginUsuario";
        public const string PCDRecoverPassUsuario = "PCD_USUARIOS_RecoverPass";
        public const string PCDResetPassUsuario = "PCD_USUARIOS_ResetPass";
        public const string PCDToggleEventoFavorito = "PCD_USUARIOS_ToggleEventoFavorito";
        public const string PCDToggleLikeArtista = "PCD_USUARIOS_ToggleLikeArtista";
        public const string PCDGetEventosFavoritos = "PCD_USUARIOS_GetEventosFavoritos";
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
                new ("p_dsPass", usuario.Pass),
                new ("p_mdInstagram", usuario.Socials.MdInstagram),
                new ("p_mdSpotify", usuario.Socials.MdSpotify),
                new ("p_mdSoundcloud", usuario.Socials.MdSoundcloud),
                new ("p_dtNacimiento", usuario.DtNacimiento),
                new ("p_idUsuario", MySqlDbType.VarChar, 36) { Direction = System.Data.ParameterDirection.Output },
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
        public static MySqlParameter[] GetUsuarioParameters(string idUsuario, string mail, bool? isActivo, int? rol)
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
        public static MySqlParameter GetRolesUsuarioParameters(string idusuario)
        {
            return new MySqlParameter("p_idUsuario", idusuario);
        }
        public static MySqlParameter[] UpdateUsuarioParameters(Usuario usuario)
        {
            string cdRolList = null;
            if (!usuario.Roles.IsNullOrEmpty())
            {
                cdRolList = string.Join(",", usuario.Roles.Select(u => u.CdRol));
            }
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
                new ("p_cdRolList", cdRolList),
                new ("p_mdInstagram", usuario.Socials.MdInstagram),
                new ("p_mdSpotify", usuario.Socials.MdSpotify),
                new ("p_mdSoundcloud", usuario.Socials.MdSoundcloud),
                new ("p_dtNacimiento", usuario.DtNacimiento),
                new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                new ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        public static MySqlParameter[] GetLoginUsuarioParameters(LoginUsuarioRequest request)
        {
            return new MySqlParameter[]
            {
                new ("p_dsCorreo", request.Correo),
                new ("p_dspass", request.Password),
                new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        public static MySqlParameter[] ResetPassUsuarioParameters(ResetPassUsuarioRequest request)
        {
            return new MySqlParameter[]
            {
                new ("p_dsCorreo", request.Correo),
                new ("p_dsPass", request.Pass),
                new ("p_dsNewPass", request.NewPass),
                new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        public static MySqlParameter[] RecoverPassUsuarioParameters(RecoverPassUsuarioRequest request)
        {
            return new MySqlParameter[]
            {
                new ("p_dsCorreo", request.Correo),
                new ("p_dsNewPass", request.NewPass)
            };
        }
        public static MySqlParameter[] EventoFavoritoParameters(EventoFavoritoRequest request)
        {
            return new MySqlParameter[]
            {
                new ("p_idUsuario", request.IdUsuario),
                new ("p_idEvento", request.IdEvento)
            };
        }
        public static MySqlParameter[] ArtistaFavoritoParameters(ArtistaFavoritoRequest request)
        {
            return new MySqlParameter[]
            {
                new ("p_idUsuario", request.IdUsuario),
                new ("p_idartista", request.IdArtista)
            };
        }
        public static MySqlParameter GetEventosFavoritosParameters(string idusuario)
        {
            return new MySqlParameter("p_idUsuario", idusuario);
        }
        #endregion

        #region Noticia PCDS
        public const string PCDCreateNoticia = "PCD_NOTICIAS_SetNoticia";
        public const string PCDDeleteNoticia = "PCD_NOTICIAS_DeleteNoticia";
        public const string PCDUpdateNoticia = "PCD_NOTICIAS_UpdateNoticia";
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
                new ("p_dsurlevento", noticia.UrlEvento),
                new ("p_idNoticia", MySqlDbType.VarChar) { Direction = System.Data.ParameterDirection.Output },
                new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                new ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        public static MySqlParameter[] UpdateNoticiaParameters(Noticia noticia)
        {
            return new MySqlParameter[]
            {
                new ("p_dsTitulo", noticia.Titulo),
                new ("p_dsContenido", noticia.Contenido),
                new ("p_idNoticia", noticia.IdNoticia),
                new ("p_dsUrlEvento", noticia.UrlEvento)
            };
        }
        public static MySqlParameter GetNoticiaParameters(string idNoticia)
        {
            return new MySqlParameter("p_idNoticia", idNoticia);
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
        public const string PCDGetGenerosByEvento = "PCD_EVENTOS_GetGenerosByEvento";
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
        public const string PCDGetArtistasEvento = "PCD_EVENTOS_GetArtistasEvento";
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
                new MySqlParameter ("p_idUsuarioOrg", eventoRequest.IdUsuarioOrg),
                new MySqlParameter ("p_idUsuarioFav", eventoRequest.IdUsuarioFav)
            };
        }
        public static MySqlParameter GetGenerosByEventoParameters(string idEvento)
        {
            return new MySqlParameter("p_idEvento", idEvento);
        }
        public static MySqlParameter[] SetEventoParameters(Evento evento)
        {
            string idArtistas = string.Join(",", evento.Artistas.Select(a => a.IdArtista ?? string.Empty));
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
                new MySqlParameter ("p_mdSoundCloud", evento.SoundCloud),
                new MySqlParameter ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                new MySqlParameter ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output },
                new MySqlParameter ("p_idEvento", MySqlDbType.VarChar, 36) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        public static MySqlParameter[] UpdateEventoParameters(Evento evento)
        {
            string idArtistas = string.Join(",", evento.Artistas.Select(a => a.IdArtista ?? string.Empty));
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
                new MySqlParameter ("p_mdSoundCloud", evento.SoundCloud),
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
        public static MySqlParameter GetEntradasFechaParameters(string idFecha)
        {
            return new MySqlParameter("p_idFecha", idFecha);
        }
        public static MySqlParameter[] SetFechaParameters(Fecha fecha, string idEvento)
        {
            return new MySqlParameter[]
            {
                new ("p_idEvento", idEvento),
                new ("p_dtInicio", fecha.Inicio),
                new ("p_dtInicioVenta", fecha.InicioVenta),
                new ("p_dtFin", fecha.Fin),
                new ("p_dtFinVenta", fecha.FinVenta),
                new ("p_cdEstado", fecha.Estado),
                new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                new ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        public static MySqlParameter[] UpdateFechaParameters(Fecha fecha)
        {
            return new MySqlParameter[]
            {
                new ("p_idFecha", fecha.IdFecha),
                new ("p_dtInicio", fecha.Inicio),
                new ("p_dtFin", fecha.Fin),
                new ("p_cdEstado", fecha.Estado),
                new ("p_ok", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output },
                new ("p_error", MySqlDbType.VarChar, 200) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        public static MySqlParameter GetArtistasEventoParameters(string idEvento)
        {
            return new MySqlParameter("p_idEvento", idEvento);
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
            return new MySqlParameter("p_idFiesta", idFiesta);
        }
        #endregion

        #region Entrada PCDS
        public const string PCDCreateEntrada = "PCD_ENTRADAS_SetEntradas";
        public const string PCDGetEntradasFecha = "PCD_ENTRADAS_GetEntradasFecha";
        public const string PCDReservarEntradas = "PCD_ENTRADAS_ReservarEntradas";
        public const string PCDCancelarReserva = "PCD_ENTRADAS_CancelarReserva";
        public const string PCDGetTipoEntradas = "PCD_ENTRADAS_GetTipoEntradas";
        public const string PCDGetEstadosEntrada = "PCD_ENTRADAS_GetEstadosEntrada";
        public const string PCDUpdateEntradas = "PCD_ENTRADAS_UpdateEntradas";
        #endregion

        #region Entrada Parameters
        public static MySqlParameter[] CreateEntradaParameters(Entrada entrada)
        {
            return new MySqlParameter[]
            {
                new MySqlParameter ("p_idFecha", entrada.Fecha.IdFecha),
                new MySqlParameter ("p_cdTipo", entrada.Tipo.CdTipo),
                new MySqlParameter ("p_amPrecio", entrada.Precio),
                new MySqlParameter ("p_vlCant", entrada.Cantidad)
            };
        }
        public static MySqlParameter CancelarReservaParameters(string idCompra)
        {
            return new MySqlParameter("p_idCompra", idCompra);
        }
        public static MySqlParameter[] ReservarEntradasParameters(ReservarEntradasRequest request)
        {
            return new MySqlParameter[]
            {
                new MySqlParameter ("p_nmCantidad", request.Cantidad),
                new MySqlParameter ("p_idUsuario", request.IdUsuario),
                new MySqlParameter ("p_idFecha", request.IdFecha),
                new MySqlParameter ("p_cdTipoEntrada", request.TipoEntrada),
                new MySqlParameter ("p_idCompra", MySqlDbType.VarChar, 36) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        public static MySqlParameter[] UpdateEntradasParameters(Entrada entrada)
        {
            return new MySqlParameter[]
            {
                new MySqlParameter ("p_idFecha", entrada.Fecha.IdFecha),
                new MySqlParameter ("p_amPrecio", entrada.Precio)
            };
        }
        public static MySqlParameter[] GetEntradasFechaParameters(GetEntradasFechaRequest request)
        {
            return new MySqlParameter[]
            {
                new MySqlParameter ("p_idFecha", request.IdFecha),
                new MySqlParameter ("p_cdEstado", request.Estado)
            };
        }
        #endregion
        #region Artista PCDS
        public const string PCDCreateArtista = "PCD_ARTISTAS_SetArtista";
        public const string PCDGetArtistas = "PCD_ARTISTAS_GetArtistas";
        public const string PCDUpdateArtista = "PCD_ARTISTAS_UpdateArtista";
        public const string PCDCDeleteArtista = "PCD_ARTISTAS_DeleteArtista";
        public const string PCDCGetCantLikesArtista = "PCD_ARTISTAS_GetCantLikesArtista";
        #endregion

        #region Artista Parameters
        public static MySqlParameter[] CreateArtistaParameters(Artista artista)
        {
            return new MySqlParameter[]
            {
                new MySqlParameter ("p_dsNombre", artista.Nombre),
                new MySqlParameter ("p_dsBio", artista.Bio),
                new MySqlParameter ("p_mdSpotify", artista.Socials.MdSpotify),
                new MySqlParameter ("p_mdInstagram", artista.Socials.MdInstagram),
                new MySqlParameter ("p_mdSoundcloud", artista.Socials.MdSoundcloud),
                new MySqlParameter ("p_isActivo", artista.IsActivo),
                new MySqlParameter ("p_idArtista", MySqlDbType.VarChar, 36) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        public static MySqlParameter[] GetArtistasParameters(GetArtistaRequest request)
        {
            return new MySqlParameter[]
            {
                new MySqlParameter ("p_idArtista", request.idArtista),
                new MySqlParameter ("p_idUsuario", request.idUsuario),
                new MySqlParameter ("p_isActivo", request.isActivo)
            };
        }
        public static MySqlParameter[] UpdateArtistaParameters(Artista artista)
        {
            return new MySqlParameter[]
            {
                new MySqlParameter ("p_idArtista", artista.IdArtista),
                new MySqlParameter ("p_dsNombre", artista.Nombre),
                new MySqlParameter ("p_dsBio", artista.Bio),
                new MySqlParameter ("p_mdSpotify", artista.Socials.MdSpotify),
                new MySqlParameter ("p_mdInstagram", artista.Socials.MdInstagram),
                new MySqlParameter ("p_mdSoundcloud", artista.Socials.MdSoundcloud),
                new MySqlParameter ("p_isActivo", artista.IsActivo)
            };
        }
        public static MySqlParameter DeleteArtistasParameters(string idArtista)
        {
            return new MySqlParameter("p_idArtista", idArtista);
        }
        public static MySqlParameter[] GetCantLikesArtistaParameters(string idArtista)
        {
            return new MySqlParameter[] 
            {
                new MySqlParameter ("p_idArtista", idArtista),
                new MySqlParameter ("p_Cant", MySqlDbType.Int32) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        #endregion

        #region Media PCDS
        public const string PCDGetMedia = "PCD_MEDIA_GetMedia";
        public const string PCDCreateMedia = "PCD_MEDIA_SetMedia";
        public const string PCDDeleteMedia = "PCD_MEDIA_DeleteMedia";
        #endregion

        #region Media Parameters
        public static MySqlParameter[] CreateMediaParameters(Media media)
        {
            return new MySqlParameter[]
            {
                new MySqlParameter ("p_idEntidadMedia", media.IdEntidadMedia),
                new MySqlParameter ("p_mdVideo", media.MdVideo),
                new MySqlParameter ("p_idMedia", MySqlDbType.VarChar, 36) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        public static MySqlParameter DeleteMediaParameters(string idMedia)
        {
            return new MySqlParameter("p_idMedia", idMedia);
        }
        public static MySqlParameter GetMediaParameters(string idEntidadMedia)
        {
            return new MySqlParameter("p_idEntidadMedia", idEntidadMedia);
        }
        #endregion

        #region Resenia PCDS
        public const string PCDCreateResenia = "PCD_RESENIAS_SetResenia";
        public const string PCDUpdateResenia = "PCD_RESENIAS_UpdateResenia";
        public const string PCDDeleteResenia = "PCD_RESENIAS_DeleteResenia";
        public const string PCDGetResenias = "PCD_RESENIAS_GetResenias";
        public const string PCDGetAvgResenias = "PCD_RESENIAS_GetAvgResenias";
        #endregion

        #region Resenia Parameters
        public static MySqlParameter[] CreateReseniaParameters(Resenia resenia)
        {
            return new MySqlParameter[]
            {
                new MySqlParameter ("p_idFiesta", resenia.IdFiesta),
                new MySqlParameter ("p_idUsuario", resenia.IdUsuario),
                new MySqlParameter ("p_nmEstrellas", resenia.Estrellas),
                new MySqlParameter ("p_dsComentario", resenia.Comentario),
                new MySqlParameter ("p_idResenia", MySqlDbType.VarChar, 36) { Direction = System.Data.ParameterDirection.Output }
            };
        }
        public static MySqlParameter[] UpdateReseniaParameters(Resenia resenia)
        {
            return new MySqlParameter[]
            {
                new MySqlParameter ("p_idResenia", resenia.IdResenia),
                new MySqlParameter ("p_nmEstrellas", resenia.Estrellas),
                new MySqlParameter ("p_dsComentario", resenia.Comentario)
            };
        }
        public static MySqlParameter DeleteReseniaParameters(string idResenia)
        {
            return new MySqlParameter("p_idResenia", idResenia);
        }
        public static MySqlParameter[] GetReseniasParameters(GetReseniaRequest request)
        {
            return new MySqlParameter[]
            {
                new MySqlParameter ("p_idResenia", request.IdResenia),
                new MySqlParameter ("p_idFiesta", request.IdFiesta),
                new MySqlParameter ("p_idUsuario", request.IdUsuario),
                new MySqlParameter ("p_nmEstrellas", request.Estrellas)
            };
        }
        public static MySqlParameter GetAvgReseniaParameters(GetAvgReseniaRequest request)
        {
            return new MySqlParameter("p_idFiesta", request.IdFiesta);
        }
        #endregion
    }
}
