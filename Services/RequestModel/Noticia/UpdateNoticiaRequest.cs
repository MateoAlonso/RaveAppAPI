namespace RaveAppAPI.Services.RequestModel.Noticia
{
    public record UpdateNoticiaRequest(string IdNoticia, string Titulo, string Contenido, string Imagen, DateTime DtPublicado, string? IdEvento, List<Models.Media>? media);
}
