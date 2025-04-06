﻿using ErrorOr;
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
        public ArtistaController(IArtistaService artistaService)
        {
            _artistaService = artistaService;
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
            return getArtistaResult.Match(
                created => Ok(MapArtistaResponse(getArtistaResult.Value)),
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
