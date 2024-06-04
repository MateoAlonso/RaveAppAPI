using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Contracts.User;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;

namespace RaveAppAPI.Controllers
{
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
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

        [HttpGet("id/{id}")]
        public IActionResult GetUsuarioById(string id)
        {
            ErrorOr<Usuario> getUsuarioResult = _usuarioService.GetUsuarioById(id);

            return getUsuarioResult.Match(
                usuario => Ok(MapUsuarioResponse(usuario)),
                errors => Problem(errors));
        }

        [HttpGet("mail/{mail}")]
        public IActionResult GetUsuarioByMail(string mail)
        {
            ErrorOr<Usuario> getUsuarioResult = _usuarioService.GetUsuarioByMail(mail);

            return getUsuarioResult.Match(
                usuario => Ok(MapUsuarioResponse(usuario)),
                errors => Problem(errors));
        }
        [HttpGet("roles/{mail}")]
        public IActionResult GetRolesUsuarioByMail(string mail)
        {
            ErrorOr<List<RolesUsuario>> getRolesUsuarioResult = _usuarioService.GetRolesUsuarioByMail(mail);

            return getRolesUsuarioResult.Match(
                rolesUsuario => Ok(MapRolesUsuarioResponse(rolesUsuario)),
                errors => Problem(errors));
        }


        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(string id, UpdateUsuarioRequest request)
        {
            ErrorOr<Usuario> requestToUsuarioResult = Usuario.From(id, request);

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

        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(string id)
        {
            ErrorOr<Deleted> deleteUsuarioResult = _usuarioService.DeleteUsuario(id);

            return deleteUsuarioResult.Match(
                deleted => NoContent(),
                errors => Problem(errors));
        }

        private static UsuarioResponse MapUsuarioResponse(Usuario usuario)
        {
            return new UsuarioResponse(
                usuario.IdUsuario, usuario.Nombre, usuario.Apellido, usuario.Correo, usuario.CBU, usuario.Dni, usuario.Telefono, usuario.IsOrganizador, usuario.IsActivo, usuario.DtAlta, usuario.DtBaja
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
                value: MapUsuarioResponse(usuario));
        }
    }
}
