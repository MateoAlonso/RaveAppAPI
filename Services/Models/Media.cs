using ErrorOr;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.RequestModel.Media;

namespace RaveAppAPI.Services.Models
{
    public class Media
    {
        [ColumnName("idmedia")]
        public string IdMedia { get; set; }
        [ColumnName("identidadmedia")]
        public string IdEntidadMedia { get; set; }
        public string Url { get; set; }

        public Media()
        {
        }
        public Media(string idMedia, string idEntidadMedia)
        {
            IdMedia = idMedia;
            IdEntidadMedia = idEntidadMedia;
        }
        public static ErrorOr<Media> Crear(string idMedia, string idEntidadMedia)
        {
            List<Error> errors = new();

            if (errors.Count > 0)
            {
                return errors;
            }
            return new Media(idMedia, idEntidadMedia);
        }
        public static ErrorOr<Media> From(CreateMediaRequest request)
        {
            return Crear(null, request.IdEntidadMedia);
        }
    }

}
