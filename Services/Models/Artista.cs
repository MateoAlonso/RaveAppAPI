using ErrorOr;
using RaveAppAPI.Services.Helpers;
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
        public Artista()
        {
            
        }
        public Artista(string idArtista, string nombre, string bio)
        {
            IdArtista = idArtista;
            Nombre = nombre;
            Bio = bio;
        }
        public ErrorOr<Artista> Crear(string idArtista, string nombre, string bio)
        {
            List<Error> errors = new();

            // Validaciones

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Artista(IdArtista, nombre, bio);
        }
    }
}
