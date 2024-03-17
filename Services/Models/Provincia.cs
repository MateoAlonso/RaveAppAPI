using RaveAppAPI.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.Models
{
    public class Provincia
    {
        [ColumnName("dsprovincia")]
        public string Nombre { get;}

        public Provincia(string nombre)
        {
            Nombre = nombre;
        }
    }
}
