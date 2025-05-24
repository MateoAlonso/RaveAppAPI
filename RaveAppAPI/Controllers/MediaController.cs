using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Media;

namespace RaveAppAPI.Controllers
{
    [Authorize]
    public class MediaController : ApiController
    {

        private readonly IMediaService _mediaService;
        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpGet()]
        public IActionResult GetMedia(string idEntidadMedia)
        {
            ErrorOr<List<Media>> getMediaResult = _mediaService.GetMedia(idEntidadMedia);

            return getMediaResult.Match(
                medias => Ok(MapMediaResponse(medias)),
                errors => Problem(errors));
        }

        [HttpPost]
        public IActionResult CreateMedia(CreateMediaRequest request)
        {
            ErrorOr<Media> requestToMediaResult = Media.From(request);

            if (requestToMediaResult.IsError)
            {
                return Problem(requestToMediaResult.Errors);
            }

            var media = requestToMediaResult.Value;
            ErrorOr<Created> createMediaResult = _mediaService.CreateMedia(media);

            return createMediaResult.Match(
                created => CreatedAtCreateMedia(media),
                errors => Problem(errors));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMedia(string id)
        {
            ErrorOr<Deleted> deleteMediaResult = _mediaService.DeleteMedia(id);

            return deleteMediaResult.Match(
                deleted => Ok(),
                errors => Problem(errors));
        }

        private CreatedAtActionResult CreatedAtCreateMedia(Media media)
        {
            return CreatedAtAction(nameof(CreateMedia),
                new { id = media.IdMedia }, MapCreateMediaResponse(media));
        }

        private CreateMediaResponse MapCreateMediaResponse(Media media)
        {
            return new(media.IdMedia);
        }

        private MediaResponse MapMediaResponse(List<Media> medias)
        {
            return new(medias);
        }
    }
}
