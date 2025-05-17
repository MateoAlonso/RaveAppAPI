namespace RaveAppAPI.Services.RequestModel.Noticia
{
    public record CreateNoticiaRequest(string Titulo, string Contenido, DateTime DtPublicado, string? UrlEvento);
}
