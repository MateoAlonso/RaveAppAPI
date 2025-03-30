using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Contracts.User;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.User;

namespace RaveAppAPI.Controllers
{
    [Authorize]
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
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
            ErrorOr<Created> createUsuarioResult = _usuarioService.CreateUsuario(usuario);

            return createUsuarioResult.Match(
                created => CreatedAtCreateUsuario(usuario),
                errors => Problem(errors));
        }

        [HttpGet("GetUsuario")]
        public IActionResult GetUsuario([FromQuery]GetUsuarioRequest request)
        {
            ErrorOr<List<Usuario>> getUsuarioResult = _usuarioService.GetUsuario(request);

            return getUsuarioResult.Match(
                usuario => Ok(MapUsuarioResponse(usuario)),
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
        [HttpGet("GetRoles")]
        public IActionResult GetRoles(string? idUsuario)
        {
            ErrorOr<List<RolesUsuario>> getRolesResult = _usuarioService.GetRolesUsuario(idUsuario);

            return getRolesResult.Match(
                roles => Ok(MapRolesUsuarioResponse(roles)),
                errors => Problem(errors));
        }
        private static UsuarioResponse MapUsuarioResponse(List<Usuario> usuarios)
        {
            return new UsuarioResponse(
                usuarios
                );
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
    }
}
