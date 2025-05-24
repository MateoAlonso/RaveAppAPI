using ErrorOr;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.RequestModel.Entrada;

namespace RaveAppAPI.Services.Models
{
    public class Entrada
    {
        [ColumnName("identrada")]
        public string IdEntrada { get; set; }
        public Fecha Fecha { get; set; }
        [ColumnName("mdqr")]
        public string MdQR { get; set; }
        public Estado Estado { get; set; }
        [ColumnName("amprecio")]
        public decimal Precio { get; set; }
        [ColumnName("nmcantidad")]
        public int Cantidad { get; set; }
        [ColumnName("cdtipo")]
        public int Tipo { get; set; }
        public Entrada()
        {

        }
        public Entrada(string idEntrada, Fecha fecha, string mdQR, Estado estado, decimal precio, int cantidad, int tipo)
        {
            IdEntrada = idEntrada;
            MdQR = mdQR;
            Estado = estado;
            Precio = precio;
            Cantidad = cantidad;
            Fecha = fecha;
            Tipo = tipo;
        }
        public static ErrorOr<Entrada> Crear(string idEntrada, Fecha fecha, string mdQR, Estado estado, decimal precio, int cantidad, int tipo)
        {
            List<Error> errors = new();
            //TODO Validaciones
            if (errors.Count > 0)
            {
                return errors;
            }
            return new Entrada(idEntrada, fecha, mdQR, estado, precio, cantidad, tipo);
        }
        public static ErrorOr<Entrada> From(CreateEntradaRequest request)
        {
            Estado estado = new Estado { CdEstado = request.Estado };
            Fecha fecha = new Fecha { IdFecha = request.IdFecha };
            return Crear(null, fecha, null, estado, request.Precio, request.Cantidad, request.Tipo);
        }
    }

}