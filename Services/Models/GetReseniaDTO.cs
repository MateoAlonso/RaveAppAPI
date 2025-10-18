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
        public string IdFiesta { get; set; }
        [ColumnName("idusuario")]
        public string IdUsuario { get; set; }
        [ColumnName("dtinsert")]
        public DateTime DtInsert { get; set; }
        [ColumnName("idresenia")]
        public string IdResenia { get; set; }


    }
}
