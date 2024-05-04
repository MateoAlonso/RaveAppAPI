using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Models;
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

        [HttpPost]
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

        [HttpGet("id/{id}")]
        public IActionResult GetEventoById(string id)
        {
            ErrorOr<Evento> getEventoResult = _eventoService.GetEventoById(id);

            return getEventoResult.Match(
                evento => Ok(MapCreateEventoResponse(evento)),
                errors => Problem(errors));
        }

        [HttpGet("estado/{estado}")]
        public IActionResult GetEventosByEstado(string estado)
        {
            ErrorOr<List<Evento>> getEventoResult = _eventoService.GetEventosByEstado(estado);

            return getEventoResult.Match(
                eventos => Ok(MapEventoResponse(eventos)),
                errors => Problem(errors));
        }

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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
