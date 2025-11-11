namespace RaveAppAPI.Services.RequestModel.Mail
{
    public class EnvioMailGenericoMasivoRequest
    {
        public string IdEvento { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }
        public string BotonUrl { get; set; }
        public string BotonTexto { get; set; }
    }
}
