using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class Estado
    {
        [ColumnName("cdestado")]
        public int CdEstado { get; set; }
        [ColumnName("dsestado")]
        public string DsEstado { get; set; }
    }
}
