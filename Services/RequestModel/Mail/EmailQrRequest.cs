using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.RequestModel.Mail
{
    public class EmailQrRequest : EmailRequest
    {
        [ColumnName("dstipo")]
        public string TipoEntrada { get; set; }
        [ColumnName("dsnombre")]
        public string NombreEvento { get; set; }
        [ColumnName("dtinicio")]
        public DateTime DtInicioFecha { get; set; }
        [ColumnName("mdqr")]
        public string MdQr { get; set; }
        [ColumnName("identrada")]
        public string IdEntrada { get; set; }
    }
}
