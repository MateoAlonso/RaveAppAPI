namespace RaveAppAPI.Services.RequestModel.Entrada
{
    public record CreateEntradaRequest(string IdFecha, int Tipo, int Estado, decimal Precio, int Cantidad);
}
