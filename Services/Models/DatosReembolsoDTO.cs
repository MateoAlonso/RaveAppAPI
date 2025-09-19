using RaveAppAPI.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.Models
{
    public class DatosReembolsoDTO
    {
        [ColumnName("idpagomp")]
        public long IdMP { get; set; }
        [ColumnName("ammonto")]
        public decimal Monto { get; set; }
    }
}
