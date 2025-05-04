using ErrorOr;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.RequestModel.Media;

namespace RaveAppAPI.Services.Models
{
    public class Media
    {
        [ColumnName("idmedia")]
        public string IdMedia { get; set; }
        [ColumnName("mdimagen")]
        public string Imagen { get; set; }
        [ColumnName("mdvideo")]
        public string Video { get; set; }
        [ColumnName("identidadmedia")]
        public string IdEntidadMedia { get; set; }

        public Media()
        {
        }
        public Media(string idMedia, string imagen, string video, string idEntidadMedia)
        {
            IdMedia = idMedia;
            Imagen = imagen;
            Video = video;
            IdMedia = idEntidadMedia;
        }
        public static ErrorOr<Media> Crear(string idMedia, string imagen, string video, string idEntidadMedia)
        {
            List<Error> errors = new();

            if (!string.IsNullOrEmpty(video) && !string.IsNullOrEmpty(imagen))
            {
                errors.Add(Error.Validation(description: "Solo setear un elemento a la vez."));
            }

            if (errors.Count > 0)
            {
                return errors;
            }
            return new Media(idMedia, imagen, video, idEntidadMedia);
        }
        public static ErrorOr<Media> From(CreateMediaRequest request)
        {
            return Crear(null, request.Imagen, request.Video, request.IdEntidadMedia);
        }
    }

}
