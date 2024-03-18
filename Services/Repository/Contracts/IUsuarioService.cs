using ErrorOr;
using RaveAppAPI.Models;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface IUsuarioService
    {
        ErrorOr<Created> CreateUsuario(Usuario usuario);
        ErrorOr<Usuario> GetUsuarioById(string id);
        ErrorOr<Usuario> GetUsuarioByMail(string mail);
        ErrorOr<Updated> UpdateUsuario(Usuario usuario);
        ErrorOr<Deleted> DeleteUsuario(string id);
    }
}
