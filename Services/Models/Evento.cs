using ErrorOr;
using RaveAppAPI.Models;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.RequestModel.Evento;

namespace RaveAppAPI.Services.Models
{
    public class Evento
    {
        public Usuario Usuario { get; set; }
        public Domicilio Domicilio { get; set; }
        [ColumnName("idevento")]
        public string IdEvento { get; set; }
        [ColumnName("dsnombre")]
        public string Nombre { get; set; }
        [ColumnName("dsevento")]
        public string Descripcion { get; set; }
        [ColumnName("dsgenero")]
        public string Genero { get; set; }
        [ColumnName("isafter")]
        public bool IsAfter { get; set; }
        [ColumnName("islgbt")]
        public bool IsLgbt { get; set; }
        [ColumnName("dtinicioventa")]
        public DateTime InicioVenta { get; set; }
        [ColumnName("dtfinventa")]
        public DateTime FinVenta { get; set; }
        [ColumnName("dtinicioevento")]
        public DateTime InicioEvento { get; set; }
        [ColumnName("dtfinevento")]
        public DateTime FinEvento { get; set; }
        [ColumnName("dsestado")]
        public string Estado { get; set; }

        public Evento(Usuario usuario, Domicilio domicilio, string nombre, string descripcion, string genero, bool isAfter, bool isLgbt, DateTime inicioVenta, DateTime finVenta, DateTime inicioEvento, DateTime finEvento, string estado)
        {
            Usuario = usuario;
            Domicilio = domicilio;
            Nombre = nombre;
            Descripcion = descripcion;
            Genero = genero;
            IsAfter = isAfter;
            IsLgbt = isLgbt;
            InicioVenta = inicioVenta;
            FinVenta = finVenta;
            InicioEvento = inicioEvento;
            FinEvento = finEvento;
            Estado = estado;
        }
        public Evento()
        {

        }

        public static ErrorOr<Evento> Crear(string nombre, string descripcion, string genero, bool isAfter, bool isLgbt, DateTime inicioVenta, DateTime finVenta, DateTime inicioEvento, DateTime finEvento, string? estado = null, Usuario? usuario = null, Domicilio? domicilio = null) 
        {
            List<Error> errors = new();

            //TODO : Validaciones

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Evento(usuario, domicilio, nombre, descripcion, genero, isAfter, isLgbt, inicioVenta, finVenta, inicioEvento, finEvento, estado);
        }

        public static Evento Devolver()
        {
            return new Evento();
        }

        public static ErrorOr<Evento> From(CreateEventoRequest request)
        {
            return Crear(request.nombre,request.descripcion, request.genero, request.isAfter, request.isLgbt, request.inicioVenta, request.finVenta, request.inicioEvento, request.finEvento, null, request.usuario, request.domicilio);
        }
        public static ErrorOr<Evento> From(string idEvento, UpdateEventoRequest request)
        {
            return Crear(request.nombre, request.descripcion, request.genero, request.isAfter, request.isLgbt, request.inicioVenta, request.finVenta, request.inicioEvento, request.finEvento, null, request.usuario, request.domicilio);
        }
    }

}
