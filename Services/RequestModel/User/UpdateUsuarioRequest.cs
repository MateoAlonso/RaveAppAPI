using Org.BouncyCastle.Asn1.Ocsp;

namespace RaveAppAPI.Contracts.User
{
    public record UpdateUsuarioRequest(int Provincia, string Localidad, string calle, string Altura, string PisoDepartamento, string Nombre, string Apellido, string Correo, string CBU, string Dni, string Telefono, string NombreFantasia, string Bio, string? IdUsuario = null);
}