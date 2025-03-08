using ErrorOr;
using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class Domicilio
    {
        public Localidad Localidad { get; set; }
        public Municipio Municipio { get; set; }
        public Provincia Provincia { get; set; }
        [ColumnName("dsdireccion")]
        public string Direccion { get; set; }
        [ColumnName("nmlatitud")]
        public decimal Latitud { get; set; }
        [ColumnName("nmlongitud")]
        public decimal Longitud { get; set; }
        public Domicilio()
        {
        }
        public Domicilio(Provincia provincia, Municipio municipio, Localidad localidad, string direccion)
        {
            Provincia = provincia;
            Municipio = municipio;
            Localidad = localidad;
            Direccion = direccion;
        }

        public static ErrorOr<Domicilio> Crear(Provincia provincia, Municipio municipio, Localidad localidad, string direccion)
        {

            List<Error> errors = new();

            // Validaciones

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Domicilio(provincia, municipio, localidad, direccion);
        }
    }
}
