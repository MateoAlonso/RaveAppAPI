using ErrorOr;
using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class Municipio
    {
        [ColumnName("dsmunicipio")]
        public string Nombre { get; set; }
        [ColumnName("cdmunicipio")]
        public string Codigo { get; set; }
        public Municipio()
        {

        }
        private Municipio(string codigo, string nombre)
        {
            Codigo = codigo;
            Nombre = nombre;
        }
        public static ErrorOr<Municipio> Crear(string codigo, string nombre)
        {
            List<Error> errors = new();

            // Validaciones

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Municipio(codigo, nombre);
        }
    }

}
