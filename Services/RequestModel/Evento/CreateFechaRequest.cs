namespace RaveAppAPI.Services.RequestModel.Evento
{
    public record CreateFechaRequest(DateTime FechaInicio, DateTime FechaFin, DateTime FechaIncioVenta, DateTime FechaFinVentaGeneral, DateTime FechaFinVentaEB, int Estado);
}
