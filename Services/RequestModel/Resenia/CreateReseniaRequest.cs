namespace RaveAppAPI.Services.RequestModel.Resenia
{
    public record CreateReseniaRequest(string IdUsuario, int Estrellas, string Comentario, string IdFiesta);
}
