using ErrorOr;
using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class Localidad
    {
        [ColumnName("dslocalidad")]
        public string Nombre { get; set; }
        public int Codigo { get; set;
        }
        public Localidad()
        {
        }
        private Localidad(int codigo, string nombre)
        {
            Codigo = codigo;
            Nombre = nombre;
        }

        public static ErrorOr<Localidad> Create(int codigo, string nombre)
        {
            List<Error> errors = new();

            //TODO validaciones

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Localidad(codigo, nombre);
        }
    }
}
