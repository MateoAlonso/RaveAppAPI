using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Evento;

namespace RaveAppAPI.Controllers
{
    [Authorize]
    public class EventoController : ApiController
    {
        private readonly IEventoService _eventoService;
        private readonly IMediaService _mediaService;

        public EventoController(IEventoService eventoService, IMediaService mediaService)
        {
            _eventoService = eventoService;
            _mediaService = mediaService;
        }

        [HttpPost("CrearEvento")]
        public IActionResult CreateEvento(CreateEventoRequest request)
        {
            ErrorOr<Evento> requestToEventoResult = Evento.From(request);

            if (requestToEventoResult.IsError)
            {
                return Problem(requestToEventoResult.Errors);
            }

            var evento = requestToEventoResult.Value;
            ErrorOr<Created> createEventoResult = _eventoService.CreateEvento(evento);
            if (!createEventoResult.IsError)
            {
                createEventoResult = _eventoService.SetFechas(evento);
            }
            return createEventoResult.Match(
                created => CreatedAtCreateEvento(evento),
                errors => Problem(errors));
        }
        [HttpGet("GetEventos")]
        public IActionResult GetEventos([FromQuery] GetEventoRequest request)
        {
            ErrorOr<List<Evento>> getEventoResult = _eventoService.GetEventos(request);
            if (!getEventoResult.IsError)
            {
                foreach (Evento evento in getEventoResult.Value)
                {
                    ErrorOr<List<Media>> getMediaResult = _mediaService.GetMedia(evento.IdEvento);
                    if (!getMediaResult.IsError)
                    {
                        evento.Media = getMediaResult.Value;
                    }
                }
            }

            return getEventoResult.Match(
                eventos => Ok(MapEventoResponse(eventos)),
                errors => Problem(errors));
        }

        [HttpPut("UpdateEvento")]
        public IActionResult UpdateEvento(UpdateEventoRequest request)
        {
            ErrorOr<Evento> requestToEventoResult = Evento.From(request);

            if (requestToEventoResult.IsError)
            {
                return Problem(requestToEventoResult.Errors);
            }

            var evento = requestToEventoResult.Value;
            ErrorOr<Updated> updateEventoResult = _eventoService.UpdateEvento(evento);
            if (!updateEventoResult.IsError)
            {
                updateEventoResult = _eventoService.UpdateFechas(evento);
            }
            return updateEventoResult.Match(
                updated => NoContent(),
                errors => Problem(errors));
        }

        [HttpDelete("DeleteEvento")]
        public IActionResult DeleteEvento(string id)
        {
            ErrorOr<Deleted> deleteEventoResult = _eventoService.DeleteEvento(id);

            return deleteEventoResult.Match(
                deleted => NoContent(),
                errors => Problem(errors));
        }
        [HttpGet("GetGeneros")]
        public IActionResult GetGeneros()
        {
            ErrorOr<List<GeneroEvento>> getGenerosResult = _eventoService.GetGeneros();

            return getGenerosResult.Match(
                generos => Ok(generos),
                errors => Problem(errors));
        }
        [HttpGet("GetEstadosEvento")]
        public IActionResult GetEstadosEvento()
        {
            ErrorOr<List<Estado>> getEstadosEventoResult = _eventoService.GetEstadosEvento();

            return getEstadosEventoResult.Match(
                estados => Ok(estados),
                errors => Problem(errors));
        }
        [HttpGet("GetEstadosFecha")]
        public IActionResult GetEstadosFecha()
        {
            ErrorOr<List<Estado>> getEstadosFechaResult = _eventoService.GetEstadosFecha();

            return getEstadosFechaResult.Match(
                estados => Ok(estados),
                errors => Problem(errors));
        }
        private static EventoResponse MapEventoResponse(List<Evento> eventos)
        {
            return new EventoResponse(eventos);
        }
        private static CreateEventoResponse MapCreateEventoResponse(Evento evento)
        {
            return new CreateEventoResponse(evento.IdEvento);
        }

        private CreatedAtActionResult CreatedAtCreateEvento(Evento evento)
        {
            return CreatedAtAction(
                actionName: nameof(CreateEvento),
                routeValues: new { id = evento.IdEvento },
                value: MapCreateEventoResponse(evento));
        }
    }
}
