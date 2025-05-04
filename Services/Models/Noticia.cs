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
        [ColumnName("idEvento")]
        public string? IdEvento { get; set; }
        public Noticia(string idNoticia, string titulo, string contenido, string imagen, DateTime fechaPublicado, string? idEvento, List<Media> media)
        {
            IdNoticia = idNoticia;
            Titulo = titulo;
            Contenido = contenido;
            DtPublicado = fechaPublicado;
            IdEvento = idEvento;
            Media = media;
        }
        public Noticia()
        {
        }

        public static ErrorOr<Noticia> Crear(string titulo, string contenido, string imagen, DateTime dtpublicado, string? idNoticia, string? idEvento, List<Media> media)
        {
            //Validaciones

            List<Error> errors = new();
            if (errors.Count > 0)
            {
                return errors;
            }

            return new Noticia(idNoticia, titulo, contenido, imagen, dtpublicado, idEvento, media);
        }
        public static ErrorOr<Noticia> From(CreateNoticiaRequest request)
        {
            return Crear(request.titulo, request.contenido, request.imagen, request.dtpublicado, null, request.IdEvento, null);
        }
        public static ErrorOr<Noticia> From(UpdateNoticiaRequest request)
        {
            return Crear(request.Titulo, request.Contenido, request.Imagen, request.DtPublicado, request.IdNoticia, request.IdEvento, null);
        }
    }

}
