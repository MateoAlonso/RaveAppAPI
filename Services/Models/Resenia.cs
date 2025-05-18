using ErrorOr;

namespace RaveAppAPI.Services.Models
{
    public class Resenia
    {
        public string IdResenia { get; set; }
        public string IdUsuario { get; set; }
        public string Correo { get; set; }
        public int Estrellas { get; set; }
        public string Comentario { get; set; }
        public DateTime DtInsert { get; set; }
        public string IdFiesta { get; set; }
        public Resenia()
        {
        }
        public Resenia(string idResenia, string idUsuario, string correo, int estrellas, string comentario, DateTime dtInsert, string idFiesta)
        {
            IdResenia = idResenia;
            IdUsuario = idUsuario;
            Correo = correo;
            Estrellas = estrellas;
            Comentario = comentario;
            DtInsert = dtInsert;
            IdFiesta = idFiesta;
        }
        public ErrorOr<Resenia> Crear(string idResenia, string idUsuario, string correo, int estrellas, string comentario, DateTime dtInsert, string idFiesta)
        {
            //Validaciones
            List<Error> errors = new();
            if (errors.Count > 0)
            {
                return errors;
            }

            return new Resenia(idResenia, idUsuario, correo, estrellas, comentario, dtInsert, idFiesta);
        }
    }
}
