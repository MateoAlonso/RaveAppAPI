using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class GetReservaActivaDTO
    {
        [ColumnName("nmcantidad")]
        public long Cantidad { get; set; }
        [ColumnName("cdtipo")]
        public int TipoEntrada { get; set; }
        [ColumnName("idcompra")]
        public string IdCompra { get; set; }
        [ColumnName("idevento")]
        public string IdEvento { get; set; }
        [ColumnName("idfecha")]
        public string IdFecha { get; set; }
    }
}
