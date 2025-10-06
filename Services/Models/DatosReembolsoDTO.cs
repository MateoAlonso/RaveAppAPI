using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class DatosReembolsoDTO
    {
        [ColumnName("idpagomp")]
        public long IdMP { get; set; }
        [ColumnName("amsubtotal")]
        public decimal Monto { get; set; }
    }
}
