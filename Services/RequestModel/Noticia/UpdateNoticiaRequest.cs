namespace RaveAppAPI.Services.RequestModel.Noticia
{
    public record UpdateNoticiaRequest(string IdNoticia, string Titulo, string Contenido, DateTime DtPublicado, string? UrlEvento);
}
