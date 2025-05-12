using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.RequestModel.Social
{
    public record UpdateSocialRequest(string IdSocial, string? Instagram, string? Spotify, string? Soundcloud);
}
