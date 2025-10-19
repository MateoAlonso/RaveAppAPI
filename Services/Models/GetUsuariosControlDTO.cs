using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class GetUsuariosControlDTO
    {
        [ColumnName("IdUsuarioControl")]
        public string IdUsuarioControl { get; set; }
        [ColumnName("dsnombreusuario")]
        public string NombreUsuario { get; set; }
    }
}
