using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.RequestModel.Evento
{
    public record CreateFechaRequest(DateTime FechaInicio, DateTime FechaFin, DateTime FechaIncioVenta, DateTime FechaFinVentaGeneral, DateTime FechaFinVentaEB, int Estado);
}
