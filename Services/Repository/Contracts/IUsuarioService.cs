﻿using ErrorOr;
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
        ErrorOr<bool> Login(LoginUsuarioRequest request);
        ErrorOr<Updated> ResetPass(ResetPassUsuarioRequest request);
        ErrorOr<Updated> RecoverPass(RecoverPassUsuarioRequest request);
        ErrorOr<Updated> EventoFavorito(EventoFavoritoRequest request);
        ErrorOr<Updated> ArtistaFavorito(ArtistaFavoritoRequest request);
        ErrorOr<List<string>> GetEventosFavoritos(string idUsuario);
    }
}
