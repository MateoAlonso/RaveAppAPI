using Org.BouncyCastle.Asn1.Ocsp;
using RaveAppAPI.Services.Models;

namespace RaveAppAPI.Contracts.User
{
    public record UpdateUsuarioRequest(Domicilio domicilio, string Nombre, string Apellido, string Correo, string CBU, string Dni, string Telefono, string NombreFantasia, string Bio, string? IdUsuario = null);
}