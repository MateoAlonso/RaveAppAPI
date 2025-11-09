using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class DatosReembolsoDTO
    {
        [ColumnName("idpagomp")]
        public long IdMP { get; set; }
        [ColumnName("amsubtotal")]
        public decimal Monto { get; set; }
        [ColumnName("idcompra")]
        public string IdCompra { get; set; }
        [ColumnName("dsnombre")]
        public string NombreEvento { get; set; }
        [ColumnName("dscorreo")]
        public string CorreoComprador { get; set; }
    }
}
