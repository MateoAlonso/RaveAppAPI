namespace RaveAppAPI.Services.RequestModel.User
{
    public record ResetPassUsuarioRequest(string Correo, string Pass, string NewPass);
}
