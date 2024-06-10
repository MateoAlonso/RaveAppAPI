using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class Provincia
    {
        [ColumnName("dsprovincia")]
        public string Nombre { get; }
        [ColumnName("cdprovincia")]
        public int Codigo { get; }

        public Provincia(int codigo, string nombre = null)
        {
            Nombre = nombre;
            Codigo = codigo;
        }
    }
}
