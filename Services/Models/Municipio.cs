using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.Models
{
    public class Municipio
    {
        public string Nombre { get; set; }
        public int Codigo { get; set; }
        public Municipio()
        {
            
        }
        private Municipio(int codigo, string nombre)
        {
            Codigo = codigo;
            Nombre = nombre;
        }
        public static ErrorOr<Municipio> Crear(int codigo, string nombre)
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
