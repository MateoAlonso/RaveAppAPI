using ErrorOr;
using RaveAppAPI.Contracts.User;
using RaveAppAPI.Services.Models;

namespace RaveAppAPI.Models
{
    public class Usuario
    {
        public string IdUsuario { get;}
        public Domicilio Domicilio { get;}
        public string DsNombre { get;}
        public string DsApellido { get;}
        public string DsCorreo { get;}
        public string DsCBU { get;}
        public string NmDni { get;}
        public string NmTelefono { get;}
        public int IsOrganizador { get;}
        public int? IsActivo { get;}
        public DateTime? DtAlta { get;}
        public DateTime? DtBaja { get;}

        public Usuario(Domicilio domicilio, string dsNombre, string dsApellido, string dsCorreo, string dsCBU, string nmDni, string nmTelefono, int isOrganizador, int? isActivo, DateTime? dtAlta, DateTime? dtBaja, string idUsuario )
        {
            IdUsuario = idUsuario;
            Domicilio = domicilio;
            DsNombre = dsNombre;
            DsApellido = dsApellido;
            DsCorreo = dsCorreo;
            DsCBU = dsCBU;
            NmDni = nmDni;
            NmTelefono = nmTelefono;
            IsOrganizador = isOrganizador;
            IsActivo = isActivo;
            DtAlta = dtAlta;
            DtBaja = dtBaja;
        }

        public static ErrorOr<Usuario> Crear(string dsProvincia, string dsLocalidad, string dsCalle, string nmAltura, string dsPisoDepartamento, string dsNombre, string dsApellido, string dsCorreo, string dsCBU, string nmDni, string nmTelefono, int isOrganizador, int? isActivo = null, DateTime? dtAlta = null, DateTime? dtBaja = null, string? idUsuario = null)
        {
            List<Error> errors = new();

            ErrorOr<Domicilio> domicilio = Domicilio.Crear(dsProvincia, dsLocalidad, dsCalle, nmAltura, dsPisoDepartamento);

            // Validaciones

            if (errors.Count > 0 || domicilio.IsError)
            {
                errors.AddRange(domicilio.Errors);
                return errors;
            }

            return new Usuario(domicilio.Value, dsNombre, dsApellido, dsCorreo, dsCBU, nmDni, nmTelefono, isOrganizador, isActivo, dtAlta, dtBaja, idUsuario);
        }

        public static Usuario Devolver(string dsProvincia, string dsLocalidad, string dsCalle, string nmAltura, string dsPisoDepartamento, string dsNombre, string dsApellido, string dsCorreo, string dsCBU, string nmDni, string nmTelefono, int isOrganizador, int? isActivo = null, DateTime? dtAlta = null, DateTime? dtBaja = null, string? idUsuario = null)
        {
            Domicilio domicilio = Domicilio.Devolver(dsProvincia, dsLocalidad, dsCalle, nmAltura, dsPisoDepartamento);
            return new Usuario(domicilio, dsNombre, dsApellido, dsCorreo, dsCBU, nmDni, nmTelefono, isOrganizador, isActivo, dtAlta, dtBaja, idUsuario);
        }

        public static ErrorOr<Usuario> From(CreateUsuarioRequest request)
        {
            return Crear(request.DsProvincia, request.DsLocalidad, request.Dscalle, request.NmAltura, request.DsPisoDepartamento, request.DsNombre, request.DsApellido, request.DsCorreo, request.DsCBU, request.NmDni, request.NmTelefono, request.IsOrganizador);
        }
        
        public static ErrorOr<Usuario> From(string idUsuario, UpdateUsuarioRequest request)
        {
            return Crear(request.DsProvincia, request.DsLocalidad, request.Dscalle, request.NmAltura, request.DsPisoDepartamento, request.DsNombre, request.DsApellido, request.DsCorreo, request.DsCBU, request.NmDni, request.NmTelefono, request.IsOrganizador);
        }
    }
}
