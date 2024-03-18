using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class Provincia
    {
        [ColumnName("dsprovincia")]
        public string Nombre { get; }

        public Provincia(string nombre)
        {
            Nombre = nombre;
        }
    }
}
