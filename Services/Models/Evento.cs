using ErrorOr;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.RequestModel.Evento;

namespace RaveAppAPI.Services.Models
{
    public class Evento
    {
        public Usuario Usuario { get; set; }
        public List<Artista> Artistas { get; set; }
        public Domicilio Domicilio { get; set; }
        [ColumnName("idevento")]
        public string IdEvento { get; set; }
        [ColumnName("idfiesta")]
        public string IdFiesta { get; set; }
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
        public List<Media> Media { get; set; }
        [ColumnName("mdsoundcloud")]
        public string SoundCloud { get; set; }
        [ColumnName("isfavorito")]
        public int IsFavorito { get; set; }
        public Evento(string idEvento, Usuario usuario, List<Artista> artistas, Domicilio domicilio, string nombre, string descripcion, List<int> genero, bool isAfter, bool isLgbt, DateTime inicioEvento, DateTime finEvento, int estado, List<Fecha> fechas, string idFiesta, List<Media> media, string soundCloud)
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
            IdFiesta = idFiesta;
            Media = media;
            SoundCloud = soundCloud;
        }
        public Evento()
        {
        }
        public static ErrorOr<Evento> Crear(string idEvento, Usuario usuario, List<Artista> artistas, Domicilio domicilio, string nombre, string descripcion, List<int> genero, bool isAfter, bool isLgbt, DateTime inicioEvento, DateTime finEvento, int estado, List<Fecha> fechas, string idFiesta, List<Media> media, string soundCloud)
        {
            List<Error> errors = new();

            //TODO Validaciones

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Evento(idEvento, usuario, artistas, domicilio, nombre, descripcion, genero, isAfter, isLgbt, inicioEvento, finEvento, estado, fechas, idFiesta, media, soundCloud);
        }
        public static ErrorOr<Evento> From(CreateEventoRequest request)
        {
            Usuario usr = new();
            usr.IdUsuario = request.idUsuario;
            List<Artista> artistas = new List<Artista>();
            List<Fecha> fechas = new List<Fecha>();
            foreach (string idArtista in request.idArtistas)
            {
                Artista artista = new();
                artista.IdArtista = idArtista;
                artistas.Add(artista);
            }
            foreach (CreateFechaRequest req in request.fechas)
            {
                fechas.Add(Fecha.From(req).Value);
            }
            return Crear(null, usr, artistas, request.domicilio, request.nombre, request.descripcion, request.genero, request.isAfter, request.isLgbt, request.inicioEvento, request.finEvento, request.estado, fechas, request.idFiesta, null, request.SoundCloud);
        }
        public static ErrorOr<Evento> From(UpdateEventoRequest request)
        {
            List<Artista> artistas = new List<Artista>();
            foreach (string idArtista in request.idArtistas)
            {
                Artista artista = new();
                artista.IdArtista = idArtista;
                artistas.Add(artista);
            }
            return Crear(request.idEvento, null, artistas, request.domicilio, request.nombre, request.descripcion, request.genero, request.isAfter, request.isLgbt, request.inicioEvento, request.finEvento, request.estado, request.fechas, request.idFiesta, null, request.SoundCloud);
        }
    }

}
