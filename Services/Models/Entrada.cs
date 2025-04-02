using RaveAppAPI.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}