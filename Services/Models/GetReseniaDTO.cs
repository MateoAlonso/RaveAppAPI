using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class GetReseniaDTO
    {
        [ColumnName("dsnombrereal")]
        public string NombreUsuario { get; set; }
        [ColumnName("dsapellido")]
        public string ApellidoUsuario { get; set; }
        [ColumnName("nmestrellas")]
        public int Estrellas { get; set; }
        [ColumnName("dscomentario")]
        public string Comentario { get; set; }
        [ColumnName("idfiesta")]
        public string IdFecha { get; set; }

    }
}
