using ErrorOr;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.RequestModel.Noticia;

namespace RaveAppAPI.Services.Models
{
    public class Noticia
    {
        [ColumnName("idnoticia")]
        public string IdNoticia { get; set; }
        [ColumnName("dstitulo")]
        public string Titulo { get; set; }
        [ColumnName("dscontenido")]
        public string Contenido { get; set; }
        public List<Media> Media { get; set; }
        [ColumnName("dtpublicado")]
        public DateTime DtPublicado { get; set; }
        [ColumnName("dsurlevento")]
        public string? UrlEvento { get; set; }
        public Noticia(string idNoticia, string titulo, string contenido, DateTime fechaPublicado, string? urlEvento, List<Media> media)
        {
            IdNoticia = idNoticia;
            Titulo = titulo;
            Contenido = contenido;
            DtPublicado = fechaPublicado;
            UrlEvento = urlEvento;
            Media = media;
        }
        public Noticia()
        {
        }

        public static ErrorOr<Noticia> Crear(string titulo, string contenido, DateTime dtpublicado, string? idNoticia, string? urlEvento, List<Media> media)
        {
            //Validaciones

            List<Error> errors = new();
            if (errors.Count > 0)
            {
                return errors;
            }

            return new Noticia(idNoticia, titulo, contenido, dtpublicado, urlEvento, media);
        }
        public static ErrorOr<Noticia> From(CreateNoticiaRequest request)
        {
            return Crear(request.Titulo, request.Contenido, request.DtPublicado, null, request.UrlEvento, null);
        }
        public static ErrorOr<Noticia> From(UpdateNoticiaRequest request)
        {
            return Crear(request.Titulo, request.Contenido, request.DtPublicado, request.IdNoticia, request.UrlEvento, null);
        }
    }

}
