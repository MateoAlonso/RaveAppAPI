using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.RequestModel.Noticia
{
    public record CreateNoticiaResponse(string idNoticia, string titulo, string contenido, DateTime dtPublicado);
}
