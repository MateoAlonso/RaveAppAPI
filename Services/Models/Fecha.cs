using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class Fecha
    {
        [ColumnName("idfecha")]
        public string IdFecha { get; set; }
        [ColumnName("dtinicio")]
        public DateTime Inicio { get; set; }
        [ColumnName("dtfin")]
        public DateTime Fin { get; set; }
        [ColumnName("cdestado")]
        public int Estado { get; set; }
        public List<Entrada> Entradas { get; set; }
    }
}
