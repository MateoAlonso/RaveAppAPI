using ErrorOr;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.RequestModel.Evento;
using System.Data;

namespace RaveAppAPI.Services.Models
{
    public class Fecha
    {
        [ColumnName("idfecha")]
        public string IdFecha { get; set; }
        [ColumnName("dtinicio")]
        public DateTime Inicio { get; set; }
        [ColumnName("dtfin")]
        public DateTime Fin { get; set; }
        [ColumnName("dtinicioventa")]
        public DateTime InicioVenta { get; set; }
        [ColumnName("dtfinventa")]
        public DateTime FinVenta { get; set; }
        [ColumnName("dtfinventaeb")]
        public DateTime FinVentaEB { get; set; }
        [ColumnName("cdestado")]
        public int Estado { get; set; }
        public List<Entrada> Entradas { get; set; }
        public Fecha()
        {

        }
        public Fecha(string idFecha, DateTime inicio, DateTime fin, int estado, List<Entrada> entradas)
        {
            this.IdFecha = idFecha;
            this.Inicio = inicio;
            this.Fin = fin;
            this.Estado = estado;
            this.Entradas = entradas;
        }
        public static ErrorOr<Fecha> Crear(string idFecha, DateTime inicio, DateTime fin, int estado, List<Entrada> entradas)
        {
            List<Error> errors = new();

            //TODO Validaciones

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Fecha(idFecha, inicio, fin, estado, entradas);
        }
        public static ErrorOr<Fecha> From(CreateFechaRequest request) 
        {
            return Crear(null, request.FechaInicio, request.FechaFin, request.Estado, null);
        }
    }
}
