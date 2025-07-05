using Microsoft.AspNetCore.Http;

namespace RaveAppAPI.Services.RequestModel.Media
{
    public record CreateMediaRequest(string IdEntidadMedia, IFormFile? File, string? Video);
}
