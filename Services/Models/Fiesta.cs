using ErrorOr;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.RequestModel.Fiesta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.Models
{
    public class Fiesta
    {
        [ColumnName("idfiesta")]
        public string IdFiesta { get; set; }
        [ColumnName("idusuario")]
        public string IdUsuario { get; set; }
        [ColumnName("dsnombre")]
        public string DsNombre { get; set; }
        [ColumnName("isactivo")]
        public bool IsActivo { get; set; }
        public Fiesta(string idFiesta, string idUsuario, string nombre, bool isActivo) 
        {
            IdFiesta = idFiesta;
            IdUsuario = idUsuario;
            DsNombre = nombre;
            IsActivo = isActivo;
        }
        public Fiesta()
        {

        }
        public static ErrorOr<Fiesta> Crear(string idFiesta, string idUsuario, string nombre, bool isActivo)
        {
            List<Error> errors = new();

            //TODO Validaciones

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Fiesta(idFiesta, idUsuario, nombre, isActivo);
        }
        public static ErrorOr<Fiesta> From(CreateFiestaRequest request)
        {
            return Crear(null, request.IdUsuario, request.Nombre, request.isActivo);
        }
        public static ErrorOr<Fiesta> From(UpdateFiestaRequest request)
        {
            return Crear(request.idFiesta, null, request.Nombre, true);
        }
    }
}