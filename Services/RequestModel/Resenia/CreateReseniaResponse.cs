namespace RaveAppAPI.Services.RequestModel.Resenia
{
    public record CreateReseniaResponse(string IdResenia, string IdUsuario, string Correo, string IdFiesta, DateTime DInsert, string Comentario, int Estrellas);
}
