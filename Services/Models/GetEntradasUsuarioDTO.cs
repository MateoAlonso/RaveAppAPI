using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class GetEntradasUsuarioDTO
    {
        [ColumnName("mdqr")]
        public string QR { get; set; }
        [ColumnName("cdestado")]
        public int CdEstado { get; set; }
        [ColumnName("dsestado")]
        public string DsEstado { get; set; }
        [ColumnName("cdtipo")]
        public int CdTipo { get; set; }
        [ColumnName("dstipo")]
        public string DsTipo { get; set; }
        [ColumnName("idfecha")]
        public string IdFecha { get; set; }
        [ColumnName("amprecio")]
        public decimal Precio { get; set; }
    }
}
