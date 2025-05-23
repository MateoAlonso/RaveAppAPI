using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.RequestModel.Entrada
{
    public record CreateEntradaRequest(string IdFecha, int Tipo, int Estado, DateTime DtInicio, DateTime DtFin, double Precio, int Cantidad);
}
