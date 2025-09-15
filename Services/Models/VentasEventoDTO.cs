using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class VentasEventoDTO
    {
        [ColumnName("idFecha")]
        public int IdFecha { get; set; }
        [ColumnName("dsTipoEntrada")]
        public string Entrada { get; set; }
        [ColumnName("vlStockInicial")]
        public int StockInicial { get; set; }
        [ColumnName("vlVendidas")]
        public int CantidadVendida { get; set; }
        [ColumnName("amPrecioEntrada")]
        public decimal PrecioEntrada { get; set; }
        [ColumnName("amTotalVenta")]
        public decimal MontoVenta { get; set; }
        [ColumnName("amSubTotal")]
        public decimal MontoSubTotal { get; set; }
        [ColumnName("amTotalServicio")]
        public decimal MontoCostoServicio { get; set; }
        [ColumnName("vlStockActual")]
        public int StockActual { get; set; }

    }
}
