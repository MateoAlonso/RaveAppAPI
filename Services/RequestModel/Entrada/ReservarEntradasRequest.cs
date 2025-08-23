namespace RaveAppAPI.Services.RequestModel.Entrada
{
    public record ReservarEntradasRequest(List<ReservarEntradaDTO> Entradas, string IdUsuario, string IdFecha);
}
