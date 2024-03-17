using RaveAppAPI.Services.Models;
using System.Security;

namespace RaveAppAPI.Contracts.User
{ 
    public record CreateUsuarioRequest(string Provincia, string Localidad, string Calle, string Altura, string PisoDepartamento, string Nombre, string Apellido, string Correo, string CBU, string Dni, string Telefono, int IsOrganizador);
}