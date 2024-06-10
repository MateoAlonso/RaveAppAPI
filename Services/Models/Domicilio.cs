using ErrorOr;
using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class Domicilio
    {
        public Localidad Localidad { get; }
        [ColumnName("dscalle")]
        public string Calle { get; }
        [ColumnName("nmaltura")]
        public string Altura { get; }
        [ColumnName("dspisodepartamento")]
        public string PisoDepartamento { get; }

        public Domicilio(Localidad localidad, string calle, string altura, string pisoDepartamento)
        {
            Localidad = localidad;
            Calle = calle;
            Altura = altura;
            PisoDepartamento = pisoDepartamento;
        }

        public static ErrorOr<Domicilio> Crear(int provincia, string localidad, string calle, string altura, string pisoDepartamento)
        {

            List<Error> errors = new();
            Localidad loc = new Localidad(localidad, provincia);
            // Validaciones

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Domicilio(loc, calle, altura, pisoDepartamento);
        }

        public static Domicilio Devolver(int provincia, string localidad, string calle, string altura, string dsPisoDepartamento)
        {
            Localidad loc = new Localidad(localidad, provincia);
            return new Domicilio(loc, calle, altura, dsPisoDepartamento);
        }
    }
}
