using ErrorOr;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.RequestModel.Artista;
using RaveAppAPI.Services.RequestModel.User;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface IUsuarioService
    {
        ErrorOr<Created> CreateUsuario(Usuario usuario);
        ErrorOr<List<Usuario>> GetUsuario(GetUsuarioRequest request);
        ErrorOr<Updated> UpdateUsuario(Usuario usuario);
        ErrorOr<Deleted> DeleteUsuario(string id);
        ErrorOr<List<RolesUsuario>> GetRolesUsuario(string idusuario);
        ErrorOr<string> Login(string correo);
        ErrorOr<Updated> ResetPass(string correo, string pass);
        ErrorOr<Updated> ConfirmarCuenta(string correo);
        ErrorOr<Updated> EventoFavorito(EventoFavoritoRequest request);
        ErrorOr<Updated> ArtistaFavorito(ArtistaFavoritoRequest request);
        ErrorOr<List<string>> GetEventosFavoritos(string idUsuario);
        ErrorOr<List<GetEntradasUsuarioDTO>> GetEntradas(string idUsuario);
        ErrorOr<string> LoginControl(string correo);
        ErrorOr<Created> CrearUsuarioControl(UsuarioControl usuario);
        ErrorOr<Deleted> DeleteUsuarioControl(DeleteUsuarioControlRequest request);
        ErrorOr<List<GetUsuariosControlDTO>> GetUsuariosControl(string idUsuarioOrg);
    }
}
