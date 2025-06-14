namespace RaveAppAPI.Services.RequestModel.Evento
{
    public record CreateFechaRequest(DateTime FechaInicio, DateTime FechaFin, DateTime FechaIncioVenta, DateTime FechaFinVenta, int Estado);
}
