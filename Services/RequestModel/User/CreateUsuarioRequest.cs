using RaveAppAPI.Services.Models;

namespace RaveAppAPI.Contracts.User
{
    public record CreateUsuarioRequest(Domicilio domicilio, string Nombre, string Apellido, string Correo, string CBU, string Dni, string Telefono, string Bio, string Password, Socials? Socials, DateTime? DtNacimiento);
}