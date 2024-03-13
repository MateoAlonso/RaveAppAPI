using RaveAppAPI.Services.Models;

namespace RaveAppAPI.Contracts.User
{
    public record UpdateUsuarioRequest(string DsProvincia, string DsLocalidad, string Dscalle, string NmAltura, string DsPisoDepartamento, string DsNombre, string DsApellido, string DsCorreo, string DsCBU, string NmDni, string NmTelefono, int IsOrganizador);
}