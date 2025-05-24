using ErrorOr;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.RequestModel.Artista;

namespace RaveAppAPI.Services.Models
{
    public class Artista
    {
        [ColumnName("idartista")]
        public string IdArtista { get; set; }
        [ColumnName("dsnombre")]
        public string Nombre { get; set; }
        [ColumnName("dsbio")]
        public string Bio { get; set; }
        [ColumnName("dtalta")]
        public DateTime? DtAlta { get; set; }
        [ColumnName("isactivo")]
        public sbyte? IsActivo { get; set; }
        public List<Media> Media { get; set; }
        public Socials Socials { get; set; }
        public Artista()
        {

        }
        public Artista(string idArtista, string nombre, string bio, DateTime? dtAlta, sbyte? isActivo, List<Media> media, Socials socials)
        {
            IdArtista = idArtista;
            Nombre = nombre;
            Bio = bio;
            DtAlta = dtAlta;
            IsActivo = isActivo;
            Media = media;
            Socials = socials;
        }
        public static ErrorOr<Artista> Crear(string idArtista, string nombre, string bio, DateTime? dtAlta, sbyte? isActivo, List<Media> media, Socials socials)
        {
            List<Error> errors = new();

            // Validaciones

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Artista(idArtista, nombre, bio, dtAlta, isActivo, media, socials);
        }

        public static ErrorOr<Artista> From(CreateArtistaRequest request)
        {
            Socials socials = request.Socials ?? new() { MdInstagram = string.Empty, MdSpotify = string.Empty, MdSoundcloud = string.Empty };
            sbyte isActivo = (sbyte)(request.IsActivo ? 1 : 0);
            return Crear(null, request.Nombre, request.Bio, null, isActivo, null, socials);
        }

        public static ErrorOr<Artista> From(UpdateArtistaRequest request)
        {
            Socials socials = request.Socials ?? new() { MdInstagram = string.Empty, MdSpotify = string.Empty, MdSoundcloud = string.Empty };
            sbyte isActivo = (sbyte)(request.IsActivo ? 1 : 0);
            return Crear(request.idArtista, request.Nombre, request.Bio, null, isActivo, null, socials);
        }
    }
}
