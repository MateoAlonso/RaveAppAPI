﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.RequestModel.Fiesta
{
    public record CreateFiestaRequest(string IdUsuario, string Nombre, bool isActivo);
}
