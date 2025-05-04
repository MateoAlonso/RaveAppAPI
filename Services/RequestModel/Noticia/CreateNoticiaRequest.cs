namespace RaveAppAPI.Services.RequestModel.Noticia
{
    public record CreateNoticiaRequest(string titulo, string contenido, string imagen, DateTime dtpublicado, string? IdEvento, List<Models.Media>? media);
}
