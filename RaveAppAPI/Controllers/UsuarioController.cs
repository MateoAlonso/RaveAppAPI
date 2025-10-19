using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Contracts.User;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Artista;
using RaveAppAPI.Services.RequestModel.User;

namespace RaveAppAPI.Controllers
{
    [Authorize]
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMediaService _mediaService;
        private readonly string _jwtKey = EnvHelper.GetJWTKey();
        private readonly string _jwtIssuer = EnvHelper.GetJWTIssuer();

        public UsuarioController(IUsuarioService usuarioService, IMediaService mediaService)
        {
            _usuarioService = usuarioService;
            _mediaService = mediaService;
        }

        [HttpPost("CreateUsuario")]
        public IActionResult CreateUsuario(CreateUsuarioRequest request)
        {
            ErrorOr<Usuario> requestToUsuarioResult = Usuario.From(request);

            if (requestToUsuarioResult.IsError)
            {
                return Problem(requestToUsuarioResult.Errors);
            }

            var usuario = requestToUsuarioResult.Value;

            usuario.Pass = Hasher.HashPassword(request.Correo, request.Password);

            ErrorOr<Created> createUsuarioResult = _usuarioService.CreateUsuario(usuario);

            return createUsuarioResult.Match(
                created => CreatedAtCreateUsuario(usuario),
                errors => Problem(errors));
        }
        [HttpPost("CreateUsuarioControl")]
        public IActionResult CreateUsuarioControl(CreateUsuarioControlRequest request)
        {
            ErrorOr<UsuarioControl> requestToUsuarioResult = UsuarioControl.From(request);

            if (requestToUsuarioResult.IsError)
            {
                return Problem(requestToUsuarioResult.Errors);
            }

            var usuario = requestToUsuarioResult.Value;

            usuario.Password = Hasher.HashPassword(request.NombreUsuario, request.Password);

            ErrorOr<Created> createUsuarioResult = _usuarioService.CrearUsuarioControl(usuario);

            return createUsuarioResult.Match(
                created => CreatedAtCreateUsuarioContol(usuario),
                errors => Problem(errors));
        }

        [HttpGet("GetUsuario")]
        public IActionResult GetUsuario([FromQuery] GetUsuarioRequest request)
        {
            ErrorOr<List<Usuario>> getUsuarioResult = _usuarioService.GetUsuario(request);
            if (!getUsuarioResult.IsError)
            {
                foreach (Usuario usuario in getUsuarioResult.Value)
                {
                    ErrorOr<List<Media>> getMediaResult = _mediaService.GetMedia(usuario.IdUsuario);
                    if (!getMediaResult.IsError)
                    {
                        usuario.Media = getMediaResult.Value;
                    }
                }
            }
            return getUsuarioResult.Match(
                usuario => Ok(MapUsuarioResponse(usuario)),
                errors => Problem(errors));
        }
        [HttpGet("GetUsuariosControl")]
        public IActionResult GetUsuariosControl([FromQuery] string idUsuarioOrg)
        {
            ErrorOr<List<GetUsuariosControlDTO>> getUsuariosControlResult = _usuarioService.GetUsuariosControl(idUsuarioOrg);

            return getUsuariosControlResult.Match(
                usuarios => Ok(usuarios),
                errors => Problem(errors));
        }
        [HttpGet("Login")]
        public IActionResult Login([FromQuery] LoginUsuarioRequest request)
        {
            ErrorOr<string> loginResult = request.IsControl ? _usuarioService.LoginControl(request.Correo) : _usuarioService.Login(request.Correo);

            if (loginResult.IsError)
            {
                return Problem(loginResult.Errors);
            }

            if (!Hasher.VerifyHashedPassword(request.Correo, loginResult.Value, request.Password))
            {
                return Forbid();
            }

            return Ok(true);
        }
        [HttpPut("ResetPass")]
        public IActionResult ResetPass([FromQuery] ResetPassUsuarioRequest request)
        {
            ErrorOr<string> loginResult = _usuarioService.Login(request.Correo);

            if (loginResult.IsError)
            {
                return Problem();
            }

            if (!Hasher.VerifyHashedPassword(request.Correo, loginResult.Value, request.Pass))
            {
                return Forbid();
            }

            string hashedNewPass = Hasher.HashPassword(request.Correo, request.NewPass);
            ErrorOr<Updated> resetPassResult = _usuarioService.ResetPass(request.Correo, hashedNewPass);

            return resetPassResult.Match(
                updated => NoContent(),
                errors => Problem(errors));
        }
        [HttpPut("RecoverPass")]
        public IActionResult RecoverPass([FromQuery] RecoverPassUsuarioRequest request)
        {
            ErrorOr<Updated> recoverPassResult = default;

            if (JwtHelper.ValidateToken(request.Token, _jwtKey, _jwtIssuer))
            {
                string hashedNewPass = Hasher.HashPassword(request.Correo, request.NewPass);
                recoverPassResult = _usuarioService.ResetPass(request.Correo, hashedNewPass);
            }
            else
            {
                recoverPassResult = Error.Validation("Token invalido o expirado");
            }

            return recoverPassResult.Match(
                updated => NoContent(),
                errors => Problem(errors));
        }
        [HttpPut("ConfirmarCuenta")]
        public IActionResult ConfirmarCuenta(ConfirmarCuentaRequest request)
        {
            ErrorOr<Updated> confirmarCuentaResult = default;

            if (JwtHelper.ValidateToken(request.Token, _jwtKey, _jwtIssuer))
            {
                confirmarCuentaResult = _usuarioService.ConfirmarCuenta(request.Correo);
            }
            else
            {
                confirmarCuentaResult = Error.Validation("Token invalido o expirado");
            }

            return confirmarCuentaResult.Match(
                updated => NoContent(),
                errors => Problem(errors));
        }
        [HttpPut("UpdateUsuario")]
        public IActionResult UpdateUsuario(UpdateUsuarioRequest request)
        {
            ErrorOr<Usuario> requestToUsuarioResult = Usuario.From(request);

            if (requestToUsuarioResult.IsError)
            {
                return Problem(requestToUsuarioResult.Errors);
            }

            var usuario = requestToUsuarioResult.Value;
            ErrorOr<Updated> updateUsuarioResult = _usuarioService.UpdateUsuario(usuario);

            return updateUsuarioResult.Match(
                updated => NoContent(),
                errors => Problem(errors));
        }

        [HttpDelete("DeleteUsuario/{id}")]
        public IActionResult DeleteUsuario(string id)
        {
            ErrorOr<Deleted> deleteUsuarioResult = _usuarioService.DeleteUsuario(id);

            return deleteUsuarioResult.Match(
                deleted => NoContent(),
                errors => Problem(errors));
        }
        [HttpDelete("DeleteUsuarioControl")]
        public IActionResult DeleteUsuarioControl(DeleteUsuarioControlRequest request)
        {
            ErrorOr<Deleted> deleteUsuarioResult = _usuarioService.DeleteUsuarioControl(request);

            return deleteUsuarioResult.Match(
                deleted => NoContent(),
                errors => Problem(errors));
        }
        [HttpGet("GetRoles")]
        public IActionResult GetRoles(string? idUsuario)
        {
            ErrorOr<List<RolesUsuario>> getRolesResult = _usuarioService.GetRolesUsuario(idUsuario);

            return getRolesResult.Match(
                roles => Ok(MapRolesUsuarioResponse(roles)),
                errors => Problem(errors));
        }
        [HttpPut("EventoFavorito")]
        public IActionResult EventoFavorito(EventoFavoritoRequest request)
        {
            ErrorOr<Updated> eventoFavoritoResult = _usuarioService.EventoFavorito(request);

            return eventoFavoritoResult.Match(
                created => Ok(),
                errors => Problem(errors));
        }
        [HttpPut("ArtistaFavorito")]
        public IActionResult ArtistaFavorito(ArtistaFavoritoRequest request)
        {
            ErrorOr<Updated> artistaFavoritoResult = _usuarioService.ArtistaFavorito(request);

            return artistaFavoritoResult.Match(
                created => Ok(),
                errors => Problem(errors));
        }
        [HttpGet("GetEventosFavoritos")]
        public IActionResult GetEventosFavoritos(string idUsuario)
        {
            ErrorOr<List<string>> getEventosFavResult = _usuarioService.GetEventosFavoritos(idUsuario);

            return getEventosFavResult.Match(
                eventos => Ok(MapEventosFavoritosResponse(eventos)),
                errors => Problem(errors));
        }
        [HttpGet("GetEntradas")]
        public IActionResult GetEntradas(string idUsuario)
        {
            ErrorOr<List<GetEntradasUsuarioDTO>> getEntradasResult = _usuarioService.GetEntradas(idUsuario);

            return getEntradasResult.Match(
                entradas => Ok(entradas),
                errors => Problem(errors));
        }
        private static GetEventosFavoritosResponse MapEventosFavoritosResponse(List<string> eventos)
        {
            return new GetEventosFavoritosResponse(eventos);
        }

        private static UsuarioResponse MapUsuarioResponse(List<Usuario> usuarios)
        {
            return new UsuarioResponse(
                usuarios
                );
        }
        private static UsuarioControlResponse MapUsuarioControlResponse(UsuarioControl usuario)
        {
            return new UsuarioControlResponse(usuario.IdUsuarioControl, usuario.IdUsuarioOrg, usuario.NombreUsuario);
        }
        private static RolesUsuarioResponse MapRolesUsuarioResponse(List<RolesUsuario> rolesUsuario)
        {
            return new RolesUsuarioResponse(rolesUsuario);
        }
        private CreatedAtActionResult CreatedAtCreateUsuario(Usuario usuario)
        {
            return CreatedAtAction(
                actionName: nameof(CreateUsuario),
                routeValues: new { id = usuario.IdUsuario },
                value: MapUsuarioResponse(new List<Usuario> { usuario }));
        }
        private CreatedAtActionResult CreatedAtCreateUsuarioContol(UsuarioControl usuario)
        {
            return CreatedAtAction(
                actionName: nameof(CreateUsuario),
                routeValues: new { id = usuario.IdUsuarioControl },
                value: MapUsuarioControlResponse(usuario));
        }
    }
}
