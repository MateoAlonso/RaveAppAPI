using RaveAppAPI.Models;

namespace RaveAppAPI.Services.Models
{
    public class Evento
    {
        public Usuario Usuario { get; set; }
        public Domicilio Domicilio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Genero { get; set; }
        public bool IsAfter { get; set; }
        public bool IsLgbt { get; set; }
        public DateTime InicioVenta { get; set; }
        public DateTime FinVenta { get; set; }
        public DateTime InicioEvento { get; set; }
        public DateTime FinEvento { get; set; }
        public string Estado { get; set; }
    }
}
