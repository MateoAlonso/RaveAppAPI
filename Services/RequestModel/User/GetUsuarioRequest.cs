namespace RaveAppAPI.Services.RequestModel.User
{
    public record GetUsuarioRequest(string? IdUsuario, string? Mail, bool? IsActivo, int? Rol);
}
