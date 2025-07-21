using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Artista;

namespace RaveAppAPI.Controllers
{
    [Authorize]
    public class ArtistaController : ApiController
    {
        private readonly IArtistaService _artistaService;
        private readonly IMediaService _mediaService;
        public ArtistaController(IArtistaService artistaService, IMediaService mediaService)
        {
            _artistaService = artistaService;
            _mediaService = mediaService;
        }
        [HttpPost("CreateArtista")]
        public IActionResult CreateArtista(CreateArtistaRequest request)
        {
            ErrorOr<Artista> requestToArtistaResult = Artista.From(request);

            if (requestToArtistaResult.IsError)
            {
                return Problem(requestToArtistaResult.Errors);
            }

            var artista = requestToArtistaResult.Value;
            ErrorOr<Created> createArtistaResult = _artistaService.CreateArtista(artista);

            return createArtistaResult.Match(
                created => CreatedAtCreateArtista(artista),
                errors => Problem(errors));

        }
        [HttpGet("GetArtista")]
        public IActionResult GetArtista([FromQuery] GetArtistaRequest request)
        {
            ErrorOr<List<Artista>> getArtistaResult = _artistaService.GetArtistas(request);
            if (!getArtistaResult.IsError)
            {
                foreach (Artista artista in getArtistaResult.Value)
                {
                    ErrorOr<List<Media>> getMediaResult = _mediaService.GetMedia(artista.IdArtista);
                    artista.Likes = _artistaService.GetCantLikesArtista(artista.IdArtista);
                    if (!getMediaResult.IsError)
                    {
                        artista.Media = getMediaResult.Value;
                    }
                }
            }
            return getArtistaResult.Match(
                created => Ok(MapArtistaResponse(getArtistaResult.Value)),
                errors => Problem(errors));
        }
        [HttpGet("GetImgLikesArtista")]
        public IActionResult GetImgLikesArtista(string id)
        {
            ErrorOr<List<string>> getArtistaResult = _artistaService.GetImgLikesArtistas(id);

            return getArtistaResult.Match(
                usuarios => Ok(usuarios),
                errors => Problem(errors));
        }
        [HttpPut("UpdateArtista")]
        public IActionResult UpdateArtista(UpdateArtistaRequest request)
        {
            ErrorOr<Artista> requestToArtistaResult = Artista.From(request);

            if (requestToArtistaResult.IsError)
            {
                return Problem(requestToArtistaResult.Errors);
            }

            var artista = requestToArtistaResult.Value;
            ErrorOr<Updated> updateArtistaResult = _artistaService.UpdateArtista(artista);

            return updateArtistaResult.Match(
                updated => NoContent(),
                errors => Problem(errors));

        }
        [HttpDelete("DeleteArtista/{id}")]
        public IActionResult DeleteArtista(string id)
        {
            ErrorOr<Deleted> deleteArtistaResult = _artistaService.DeleteArtista(id);

            return deleteArtistaResult.Match(
                updated => NoContent(),
                errors => Problem(errors));

        }
        private static ArtistaResponse MapArtistaResponse(List<Artista> artistas)
        {
            return new(artistas);
        }

        private IActionResult CreatedAtCreateArtista(Artista artista)
        {
            return CreatedAtAction(
                actionName: nameof(CreateArtista),
                routeValues: new { id = artista.IdArtista },
                value: artista);
        }
    }
}
