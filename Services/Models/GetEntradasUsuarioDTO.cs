using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class GetEntradasUsuarioDTO
    {
        [ColumnName("identrada")]
        public string IdEntrada { get; set; }
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
        [ColumnName("idevento")]
        public string IdEvento { get; set; }
    }
}
