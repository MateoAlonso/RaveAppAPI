using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.RequestModel.Entrada
{
    public class ControlarEntradaResponse
    {
        [ColumnName("dsestado")]
        public string EstadoEntrada { get; set; }
        [ColumnName("isOk")]
        public int IsOk { get; set; }
        [ColumnName("dstipo")]
        public string TipoEntrada { get; set; }
        [ColumnName("amprecio")]
        public decimal Precio { get; set; }
        [ColumnName("idusuario")]
        public string IdUsuario { get; set; }
        [ColumnName("dsnombre")]
        public string NombreEvento { get; set; }
    }
}
