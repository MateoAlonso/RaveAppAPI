using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.Models
{
    public class Localidad
    {
        public string DsNombre { get;}
        public Provincia Provincia { get;}

        public Localidad(string dsNombre, string dsProvincia)
        {
            DsNombre = dsNombre;
            Provincia = new Provincia(dsProvincia);
        }

        //public static ErrorOr<Localidad> Create(string dsNombre, string dsProvincia)
        //{
        //    List<Error> errors = new(); añadir validaciones si fuera necesario
        //    Provincia provincia = new Provincia(dsProvincia);
        //    return new Localidad(dsNombre, provincia);
        //}
    }
}
