using ErrorOr;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.RequestModel.Noticia;
using ZstdSharp;

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
        [ColumnName("mdimagen")]
        public string Imagen { get; set; }
        [ColumnName("dtpublicado")]
        public DateTime DtPublicado { get; set; }
        public Noticia(string idNoticia, string titulo, string contenido, string imagen, DateTime fechaPublicado)
        {
            IdNoticia = idNoticia;
            Titulo = titulo;
            Contenido = contenido;
            Imagen = imagen;
            DtPublicado = fechaPublicado;
        }
        public Noticia()
        {
        }

        public static ErrorOr<Noticia> Crear(string titulo, string contenido, string imagen, DateTime dtpublicado, string? idNoticia = null)
        {
            //Validaciones

            List<Error> errors = new();
            if (errors.Count > 0)
            {
                return errors;
            }

            return new Noticia(idNoticia, titulo, contenido, imagen, dtpublicado);
        }

        public static ErrorOr<Noticia> Devolver(string titulo, string contenido, string imagen, DateTime dtpublicado, string? idNoticia)
        {
            return new Noticia(idNoticia, titulo, contenido, imagen, dtpublicado);
        }

        public static ErrorOr<Noticia> From(CreateNoticiaRequest request)
        {
            return Crear(request.titulo, request.contenido,request.imagen, request.dtpublicado);
        }
    }

}
