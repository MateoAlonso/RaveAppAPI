using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class AvgReseniaDTO
    {
        [ColumnName("avgestrellas")]
        public decimal AvgEstrellas { get; set; }
        [ColumnName("idfiesta")]
        public string IdFiesta { get; set; }
        [ColumnName("cantresenias")]
        public long CantResenias { get; set; }
    }
}
