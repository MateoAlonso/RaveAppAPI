namespace RaveAppAPI.Contracts.User
{
    public record UsuarioResponse(string IdUsuario, string DsNombre, string DsApellido, string DsCorreo, string DsCBU, string NmDni, string NmTelefono, int? IsActivo, DateTime? DtAlta, DateTime? DtBaja);
}