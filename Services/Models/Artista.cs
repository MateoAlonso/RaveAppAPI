using ErrorOr;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.RequestModel.Artista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Artista()
        {
            
        }
        public Artista(string idArtista, string nombre, string bio, DateTime? dtAlta, sbyte? isActivo)
        {
            IdArtista = idArtista;
            Nombre = nombre;
            Bio = bio;
            DtAlta = dtAlta;
            IsActivo = isActivo;
        }
        public static ErrorOr<Artista> Crear(string idArtista, string nombre, string bio, DateTime? dtAlta, sbyte? isActivo)
        {
            List<Error> errors = new();

            // Validaciones

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Artista(idArtista, nombre, bio, dtAlta, isActivo);
        }

        public static ErrorOr<Artista> From(CreateArtistaRequest request)
        {
            return Crear(null, request.Nombre, request.Bio, null, null);
        }

        public static ErrorOr<Artista> From(UpdateArtistaRequest request)
        {
            return Crear(request.idArtista, request.Nombre, request.Bio, null, request.IsActivo);
        }
    }
}
