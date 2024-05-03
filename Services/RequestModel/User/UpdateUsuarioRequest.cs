namespace RaveAppAPI.Contracts.User
{
    public record UpdateUsuarioRequest(string Provincia, string Localidad, string calle, string Altura, string PisoDepartamento, string Nombre, string Apellido, string Correo, string CBU, string Dni, string Telefono, int IsOrganizador, string? IdUsuario = null);
}