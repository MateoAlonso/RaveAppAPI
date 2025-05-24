namespace RaveAppAPI.Services.RequestModel.Entrada
{
    public record ReservarEntradasRequest(int Cantidad, string IdUsuario, string IdFecha, int TipoEntrada);
}
