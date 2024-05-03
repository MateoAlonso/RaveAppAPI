using ErrorOr;
using RaveAppAPI.Models;
using RaveAppAPI.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface INoticiaService
    {
        ErrorOr<Created> CreateNoticia(Noticia noticia);
        ErrorOr<List<Noticia>> GetNoticias();
        ErrorOr<Updated> UpdateNoticia(Noticia noticia);
        ErrorOr<Deleted> DeleteNoticia(string id);
    }
}
