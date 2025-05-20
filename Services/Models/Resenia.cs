using ErrorOr;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.RequestModel.Resenia;

namespace RaveAppAPI.Services.Models
{
    public class Resenia
    {
        [ColumnName("idresenia")]
        public string IdResenia { get; set; }
        [ColumnName("idusuario")]
        public string IdUsuario { get; set; }
        [ColumnName("dscorreo")]
        public string Correo { get; set; }
        [ColumnName("nmestrellas")]
        public int Estrellas { get; set; }
        [ColumnName("dscomentario")]
        public string Comentario { get; set; }
        [ColumnName("dtinsert")]
        public DateTime DtInsert { get; set; }
        [ColumnName("idfiesta")]
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
        public static ErrorOr<Resenia> Crear(string idResenia, string idUsuario, string correo, int estrellas, string comentario, DateTime dtInsert, string idFiesta)
        {
            //Validaciones
            List<Error> errors = new();
            if (errors.Count > 0)
            {
                return errors;
            }

            return new Resenia(idResenia, idUsuario, correo, estrellas, comentario, dtInsert, idFiesta);
        }

        public static ErrorOr<Resenia> From(CreateReseniaRequest request)
        {
            return Crear(null, request.IdUsuario, null, request.Estrellas, request.Comentario, DateTime.Now, request.IdFiesta);
        }

        public static ErrorOr<Resenia> From(UpdateReseniaRequest request)
        {
            return Crear(request.IdResenia, null, null, request.Estrellas, request.Comentario, DateTime.Now, null);
        }
    }
}
