using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Evento;

namespace RaveAppAPI.Controllers
{
    public class EventoController : ApiController
    {
        private readonly IEventoService _eventoService;

        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;
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

            return createEventoResult.Match(
                created => CreatedAtCreateEvento(evento),
                errors => Problem(errors));
        }
        [HttpPost("GetEventos")]
        public IActionResult GetEventos(GetEventoRequest request)
        {
            ErrorOr<List<Evento>> getEventoResult = _eventoService.GetEventos(request);

            return getEventoResult.Match(
                eventos => Ok(MapEventoResponse(eventos)),
                errors => Problem(errors));
        }

        [HttpPut("UpdateEvento")]
        public IActionResult UpdateEvento(string id, UpdateEventoRequest request)
        {
            ErrorOr<Evento> requestToEventoResult = Evento.From(id, request);

            if (requestToEventoResult.IsError)
            {
                return Problem(requestToEventoResult.Errors);
            }

            var evento = requestToEventoResult.Value;
            ErrorOr<Updated> updateEventoResult = _eventoService.UpdateEvento(evento);

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
