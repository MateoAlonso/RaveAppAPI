﻿using ErrorOr;
using RaveAppAPI.Contracts.User;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.RequestModel.User;

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
        public List<RolesUsuario> Roles { get; set; }
        public Usuario(Domicilio domicilio, string nombre, string apellido, string correo, string cbu, string dni, string tel, int isActivo, DateTime? dtAlta, DateTime? dtBaja, string idUsuario, string nombreFantasia, string bio, List<RolesUsuario> roles)
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
        }
        public Usuario()
        {

        }

        public static ErrorOr<Usuario> Crear(Domicilio domicilio, string nombre, string apellido, string correo, string cbu, string dni, string tel, string nombreFantasia, string bio, int isActivo = 1, DateTime? dtAlta = null, DateTime? dtBaja = null, string? idUsuario = null, List<RolesUsuario>? roles = null)
        {
            List<Error> errors = new();

            // Validaciones

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Usuario(domicilio, nombre, apellido, correo, cbu, dni, tel, isActivo, dtAlta, dtBaja, idUsuario, nombreFantasia, bio, roles);
        }


        public static ErrorOr<Usuario> From(CreateUsuarioRequest request)
        {
            return Crear(request.domicilio, request.Nombre, request.Apellido, request.Correo, request.CBU, request.Dni, request.Telefono, request.NombreFantasia, request.Bio);
        }

        public static ErrorOr<Usuario> From(UpdateUsuarioRequest request)
        {
            List<RolesUsuario> roles = new();
            request.CdRoles.ForEach(r => roles.Add(new() {CdRol = r }));
            return Crear(request.domicilio, request.Nombre, request.Apellido, request.Correo, request.CBU, request.Dni, request.Telefono, request.NombreFantasia, request.Bio, idUsuario: request.IdUsuario, roles: roles);
        }
    }
}
