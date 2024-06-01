namespace RaveAppAPI.Services.RequestModel.Noticia
{
    public record CreateNoticiaResponse(string idNoticia, string titulo, string contenido, DateTime dtPublicado);
}
