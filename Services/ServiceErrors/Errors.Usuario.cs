using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;

namespace RaveAppAPI.Services.ServiceErrors
{
    public static class Errors
    {
        public static class ErrorUsuario
        {
            public static Error NotFound => Error.NotFound(
                code: "UsuarioNotFound",
                description: "Usuario no encontrado"
                );
            public static Error Panic => Error.Unexpected(
                code: "UsuarioNotFound",
                description: "Usuario no encontrado"
                );
        }
    }
}
