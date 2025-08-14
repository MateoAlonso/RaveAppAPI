using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class Tipo
    {
        [ColumnName("cdtipo")]
        public int CdTipo { get; set; }
        [ColumnName("dstipo")]
        public string DsTipo { get; set; }
    }
}
