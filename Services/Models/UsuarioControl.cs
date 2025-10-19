using ErrorOr;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.RequestModel.User;

namespace RaveAppAPI.Services.Models
{
    public class UsuarioControl
    {
        [ColumnName("IdUsuarioControl")]
        public string IdUsuarioControl { get; set; }
        [ColumnName("dsnombreusuario")]
        public string Password { get; set; }
        [ColumnName("dspass")]
        public string NombreUsuario { get; set; }
        [ColumnName("idusuario")]
        public string IdUsuarioOrg { get; set; }
        public UsuarioControl(string idUsuarioControl, string pass, string nombreUsuario, string idUsuario)
        {
            IdUsuarioControl = idUsuarioControl;
            Password = pass;
            NombreUsuario = nombreUsuario;
            IdUsuarioOrg = idUsuario;
        }

        public static ErrorOr<UsuarioControl> Crear(string idUsuarioControl, string pass, string nombreUsuario, string idUsuarioOrg)
        {
            List<Error> errors = new();

            // Validaciones

            if (errors.Count > 0)
            {
                return errors;
            }
            return new UsuarioControl(idUsuarioControl, pass, nombreUsuario, idUsuarioOrg);
        }

        public static ErrorOr<UsuarioControl> From(CreateUsuarioControlRequest request)
        {
            return Crear(request.IdUsuarioOrg, request.Password, request.NombreUsuario, request.IdUsuarioOrg);
        }
    }
}
