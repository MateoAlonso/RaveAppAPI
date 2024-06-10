using ErrorOr;
using RaveAppAPI.Contracts.User;
using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class Usuario
    {
        [ColumnName("idusuario")]
        public string IdUsuario { get; set; }
        public Domicilio Domicilio { get; set; }
        [ColumnName("dsnombre")]
        public string Nombre { get; set; }
        [ColumnName("dsapellido")]
        public string Apellido { get; set; }
        [ColumnName("dscorreo")]
        public string Correo { get; set; }
        [ColumnName("nmcbu")]
        public string CBU { get; set; }
        [ColumnName("nmdni")]
        public string Dni { get; set; }
        [ColumnName("nmtelefono")]
        public string Telefono { get; set; }
        [ColumnName("isactivo")]
        public int IsActivo { get; set; }
        [ColumnName("dtalta")]
        public DateTime? DtAlta { get; set; }
        [ColumnName("dtbaja")]
        public DateTime? DtBaja { get; set; }
        [ColumnName("dsnombrefantasia")]
        public string NombreFantasia { get; set; }
        [ColumnName("dsbio")]
        public string Bio { get; set; }

        public Usuario(Domicilio domicilio, string nombre, string apellido, string correo, string cbu, string dni, string tel, int isActivo, DateTime? dtAlta, DateTime? dtBaja, string idUsuario, string nombreFantasia, string bio)
        {
            IdUsuario = idUsuario;
            Domicilio = domicilio;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            CBU = cbu;
            Dni = dni;
            Telefono = tel;
            IsActivo = isActivo;
            DtAlta = dtAlta;
            DtBaja = dtBaja;
            NombreFantasia = nombreFantasia;
            Bio = bio;
        }
        public Usuario()
        {

        }

        public static ErrorOr<Usuario> Crear(int Provincia, string Localidad, string Calle, string Altura, string PisoDepartamento, string nombre, string apellido, string correo, string cbu, string dni, string tel, string nombreFantasia, string bio, int isActivo = 1, DateTime? dtAlta = null, DateTime? dtBaja = null, string? idUsuario = null)
        {
            List<Error> errors = new();

            ErrorOr<Domicilio> domicilio = Domicilio.Crear(Provincia, Localidad, Calle, Altura, PisoDepartamento);

            // Validaciones

            if (errors.Count > 0 || domicilio.IsError)
            {
                errors.AddRange(domicilio.Errors);
                return errors;
            }

            return new Usuario(domicilio.Value, nombre, apellido, correo, cbu, dni, tel, isActivo, dtAlta, dtBaja, idUsuario, nombreFantasia, bio);
        }

        public static Usuario Devolver(int Provincia, string Localidad, string Calle, string Altura, string PisoDepartamento, string nombre, string apellido, string correo, string cbu, string dni, string tel, string nombreFantasia, string bio, int isActivo = 1, DateTime? dtAlta = null, DateTime? dtBaja = null, string? idUsuario = null)
        {
            Domicilio domicilio = Domicilio.Devolver(Provincia, Localidad, Calle, Altura, PisoDepartamento);
            return new Usuario(domicilio, nombre, apellido, correo, cbu, dni, tel, isActivo, dtAlta, dtBaja, idUsuario, nombreFantasia, bio);
        }

        public static ErrorOr<Usuario> From(CreateUsuarioRequest request)
        {
            return Crear(request.Provincia, request.Localidad, request.Calle, request.Altura, request.PisoDepartamento, request.Nombre, request.Apellido, request.Correo, request.CBU, request.Dni, request.Telefono, request.NombreFantasia, request.Bio);
        }

        public static ErrorOr<Usuario> From(string idUsuario, UpdateUsuarioRequest request)
        {
            return Crear(request.Provincia, request.Localidad, request.calle, request.Altura, request.PisoDepartamento, request.Nombre, request.Apellido, request.Correo, request.CBU, request.Dni, request.Telefono, request.NombreFantasia, request.Bio, idUsuario: request.IdUsuario);
        }
    }
}
