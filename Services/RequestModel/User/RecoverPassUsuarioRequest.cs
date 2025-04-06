using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.RequestModel.User
{
    public record RecoverPassUsuarioRequest(string Correo, string NewPass);
}
