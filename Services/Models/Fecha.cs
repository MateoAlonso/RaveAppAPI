using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class Fecha
    {
        [ColumnName("dtinicio")]
        public DateTime Inicio { get; set; }
        [ColumnName("dtfin")]
        public DateTime Fin { get; set; }
    }
}
