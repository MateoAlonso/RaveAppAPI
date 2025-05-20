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
        [ColumnName("dsnombrereal")]
        public string Nombre { get; set; }
        [ColumnName("dsapellido")]
        public string Apellido { get; set; }
        [ColumnName("dscorreo")]
        public string Correo { get; set; }
        [ColumnName("dscbu")]
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
        [ColumnName("dspass")]
        public string Pass { get; set; }
        public List<RolesUsuario> Roles { get; set; }
        public List<Media> Media { get; set; }
        public Socials Socials { get; set; }
        public Usuario(Domicilio domicilio, string nombre, string apellido, string correo, string cbu, string dni, string tel, int isActivo, DateTime? dtAlta, DateTime? dtBaja, string idUsuario, string nombreFantasia, string bio, List<RolesUsuario> roles, string? pass, List<Media> media, Socials socials)
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
            Roles = roles;
            Pass = pass;
            Media = media;
            Socials = socials;
        }
        public Usuario()
        {

        }

        public static ErrorOr<Usuario> Crear(Domicilio domicilio, string nombre, string apellido, string correo, string cbu, string dni, string tel, string nombreFantasia, string bio, string pass, List<Media> media, Socials socials, int isActivo = 1, DateTime? dtAlta = null, DateTime? dtBaja = null, string? idUsuario = null, List<RolesUsuario>? roles = null)
        {
            List<Error> errors = new();

            // Validaciones

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Usuario(domicilio, nombre, apellido, correo, cbu, dni, tel, isActivo, dtAlta, dtBaja, idUsuario, nombreFantasia, bio, roles, pass, media, socials);
        }


        public static ErrorOr<Usuario> From(CreateUsuarioRequest request)
        {
            Socials socials = request.Socials ?? new() { MdInstagram = string.Empty, MdSoundcloud = string.Empty, MdSpotify = string.Empty };
            return Crear(request.domicilio, request.Nombre, request.Apellido, request.Correo, request.CBU, request.Dni, request.Telefono, request.NombreFantasia, request.Bio, request.Password, media: null, socials: socials);
        }

        public static ErrorOr<Usuario> From(UpdateUsuarioRequest request)
        {
            Socials socials = request.Socials ?? new() { MdInstagram = string.Empty, MdSoundcloud = string.Empty, MdSpotify = string.Empty };
            List<RolesUsuario> roles = new();
            request.CdRoles.ForEach(r => roles.Add(new() { CdRol = r }));
            return Crear(request.domicilio, request.Nombre, request.Apellido, request.Correo, request.CBU, request.Dni, request.Telefono, request.NombreFantasia, request.Bio, null, idUsuario: request.IdUsuario, roles: roles, media: null, socials: socials);
        }
    }
}
