using ErrorOr;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.RequestModel.Evento;

namespace RaveAppAPI.Services.Models
{
    public class Evento
    {
        public Usuario Usuario { get; set; }
        public List<Usuario> Artistas { get; set; }
        public Domicilio Domicilio { get; set; }
        [ColumnName("idevento")]
        public string IdEvento { get; set; }
        [ColumnName("dsnombre")]
        public string Nombre { get; set; }
        [ColumnName("dsevento")]
        public string Descripcion { get; set; }
        [ColumnName("cdgenero")]
        public List<int> Genero { get; set; }
        [ColumnName("isafter")]
        public bool IsAfter { get; set; }
        [ColumnName("islgbt")]
        public bool IsLgbt { get; set; }
        [ColumnName("dtinicioevento")]
        public DateTime InicioEvento { get; set; }
        [ColumnName("dtfinevento")]
        public DateTime FinEvento { get; set; }
        [ColumnName("cdestado")]
        public int CdEstado { get; set; }
        public List<Fecha> Fechas { get; set; }
        public Evento(string idEvento, Usuario usuario, List<Usuario> artistas, Domicilio domicilio, string nombre, string descripcion, List<int> genero, bool isAfter, bool isLgbt, DateTime inicioEvento, DateTime finEvento, int estado, List<Fecha> fechas)
        {
            IdEvento = idEvento;
            Usuario = usuario;
            Artistas = artistas;
            Domicilio = domicilio;
            Nombre = nombre;
            Descripcion = descripcion;
            Genero = genero;
            IsAfter = isAfter;
            IsLgbt = isLgbt;
            InicioEvento = inicioEvento;
            FinEvento = finEvento;
            CdEstado = estado;
            Fechas = fechas;
        }
        public Evento()
        {
        }
        public static ErrorOr<Evento> Crear(string idEvento, Usuario usuario,List<Usuario> artistas, Domicilio domicilio, string nombre, string descripcion, List<int> genero, bool isAfter, bool isLgbt, DateTime inicioEvento, DateTime finEvento, int estado, List<Fecha> fechas)
        {
            List<Error> errors = new();

            //TODO : Validaciones

            if (errors.Count > 0)
            {
                return errors;
            }
            
            return new Evento(idEvento, usuario, artistas, domicilio, nombre, descripcion, genero, isAfter, isLgbt, inicioEvento, finEvento, estado, fechas);
        }
        public static ErrorOr<Evento> From(CreateEventoRequest request)
        {
            Usuario usr = new();
            usr.IdUsuario = request.idUsuario;
            List<Usuario> artistas = new List<Usuario>();
            foreach (string idArtista in request.idArtistas)
            {
                Usuario artista = new();
                artista.IdUsuario = idArtista;
                artistas.Add(artista);
            }
            return Crear(null, usr, artistas, request.domicilio, request.nombre, request.descripcion, request.genero, request.isAfter, request.isLgbt, request.inicioEvento, request.finEvento, request.estado, request.fechas);
        }
        public static ErrorOr<Evento> From(UpdateEventoRequest request)
        {
            List<Usuario> artistas = new List<Usuario>();
            foreach (string idArtista in request.idArtistas)
            {
                Usuario artista = new();
                artista.IdUsuario = idArtista;
                artistas.Add(artista);
            }
            return Crear(request.idEvento, null, artistas, request.domicilio, request.nombre, request.descripcion, request.genero, request.isAfter, request.isLgbt, request.inicioEvento, request.finEvento, request.estado, request.fechas);
        }
    }

}
