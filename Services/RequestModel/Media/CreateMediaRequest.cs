using Microsoft.AspNetCore.Http;

namespace RaveAppAPI.Services.RequestModel.Media
{
    public record CreateMediaRequest(string Imagen, string Video, string IdEntidadMedia, IFormFile File);
}
