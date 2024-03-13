using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.Models
{
    public class Domicilio
    {
        public Localidad Localidad  { get;}
        public string DsCalle { get;}
        public string NmAltura { get;}
        public string DsPisoDepartamento { get;}

        private Domicilio(Localidad localidad, string dsCalle, string nmAltura, string dsPisoDepartamento) 
        {
            Localidad = localidad;
            DsCalle = dsCalle;
            NmAltura = nmAltura;
            DsPisoDepartamento = dsPisoDepartamento;
        }

        public static ErrorOr<Domicilio> Crear(string dsProvincia, string dsLocalidad, string dsCalle, string nmAltura, string dsPisoDepartamento)
        {
            
            List<Error> errors = new();
            Localidad localidad = new Localidad(dsLocalidad, dsProvincia);
            // Validaciones

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Domicilio(localidad, dsCalle, nmAltura, dsPisoDepartamento);
        }
        
        public static Domicilio Devolver(string dsProvincia, string dsLocalidad, string dsCalle, string nmAltura, string dsPisoDepartamento)
        {
            Localidad localidad = new Localidad(dsLocalidad, dsProvincia);
            return new Domicilio(localidad, dsCalle, nmAltura, dsPisoDepartamento);
        }
    }
}
