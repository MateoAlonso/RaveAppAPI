using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class GeneroEvento
    {
        [ColumnName("cdgenero")]
        public int CdGenero { get; set; }
        [ColumnName("dsgenero")]
        public string DsGenero { get; set; }
    }
}
