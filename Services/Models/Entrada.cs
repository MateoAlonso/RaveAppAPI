using ErrorOr;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.RequestModel.Entrada;

namespace RaveAppAPI.Services.Models
{
    public class Entrada
    {
        [ColumnName("identrada")]
        public string IdEntrada { get; set; }
        [ColumnName("mdqr")]
        public string MdQR { get; set; }
        public Estado Estado { get; set; }
        [ColumnName("amprecio")]
        public double Precio { get; set; }
        [ColumnName("nmcantidad")]
        public int Cantidad { get; set; }
        public Entrada()
        {
            
        }
        public Entrada(string idEntrada, string mdQR, Estado estado, double precio, int cantidad)
        {
            IdEntrada = idEntrada;
            MdQR = mdQR;
            Estado = estado;
            Precio = precio;
            Cantidad = cantidad;
        }
        public static ErrorOr<Entrada> Crear(string idEntrada, string mdQR, Estado estado, double precio, int cantidad)
        {
            List<Error> errors = new();
            //TODO Validaciones
            if (errors.Count > 0)
            {
                return errors;
            }
            return new Entrada(idEntrada, mdQR, estado, precio, cantidad);
        }
        public static ErrorOr<Entrada> From(CreateEntradaRequest request)
        {
            Estado estado = new Estado { CdEstado = request.Estado };
            return Crear(null, null, estado, request.Precio, request.Cantidad);
        }
    }

}