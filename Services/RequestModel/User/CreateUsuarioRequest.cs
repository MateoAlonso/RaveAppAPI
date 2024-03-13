using RaveAppAPI.Services.Models;
using System.Security;

namespace RaveAppAPI.Contracts.User
{ 
    public record CreateUsuarioRequest(string DsProvincia, string DsLocalidad, string Dscalle, string NmAltura, string DsPisoDepartamento, string DsNombre, string DsApellido, string DsCorreo, string DsCBU, string NmDni, string NmTelefono, int IsOrganizador);
}