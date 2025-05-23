using RaveAppAPI.Services.Models;

namespace RaveAppAPI.Contracts.User
{
    public record UpdateUsuarioRequest(Domicilio domicilio, string Nombre, string Apellido, string Correo, string CBU, string Dni, string Telefono, string NombreFantasia, string Bio, List<int> CdRoles, Socials? Socials, DateTime? DtNacimiento, string? IdUsuario = null);
}