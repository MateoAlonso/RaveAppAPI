﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.RequestModel.Artista
{
    public record ArtistaFavoritoRequest(string IdUsuario, string IdArtista);
}
