namespace RaveAppAPI.Services.RequestModel.Noticia
{
    public record CreateNoticiaRequest(string titulo, string contenido, string imagen, DateTime dtpublicado);
}
