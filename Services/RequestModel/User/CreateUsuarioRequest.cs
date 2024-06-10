namespace RaveAppAPI.Contracts.User
{
    public record CreateUsuarioRequest(int Provincia, string Localidad, string Calle, string Altura, string PisoDepartamento, string Nombre, string Apellido, string Correo, string CBU, string Dni, string Telefono, string NombreFantasia, string Bio);
}