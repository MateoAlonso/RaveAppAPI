using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.RequestModel.Entrada
{
    public class ControlarEntradaResponse
    {
        [ColumnName("dsestado")]
        public string EstadoEntrada { get; set; }
        [ColumnName("isOk")]
        public int IsOk { get; set; }
    }
}
