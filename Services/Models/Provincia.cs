using ErrorOr;
using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class Provincia
    {
        [ColumnName("dsprovincia")]
        public string Nombre { get; set; }
        [ColumnName("cdprovincia")]
        public string Codigo { get; set; }
        public Provincia()
        {

        }
        private Provincia(string codigo, string nombre)
        {
            Nombre = nombre;
            Codigo = codigo;
        }
        public static ErrorOr<Provincia> Crear(string codigo, string nombre)
        {
            List<Error> errors = new();

            //TODO validaciones

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Provincia(codigo, nombre);
        }
    }
}
