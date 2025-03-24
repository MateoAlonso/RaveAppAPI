using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Evento;
using RaveAppAPI.Services.RequestModel.Fiesta;

namespace RaveAppAPI.Controllers
{
    [Authorize]
    public class FiestaController : ApiController
    {
        private readonly IFiestaService _fiestaService;

        public FiestaController(IFiestaService fiestaService)
        {
            _fiestaService = fiestaService;
        }
        [HttpPost("CrearFiesta")]
        public IActionResult CreateFiesta(CreateFiestaRequest request) 
        {
            ErrorOr<Fiesta> requestToFiestaResult = Fiesta.From(request);

            if (requestToFiestaResult.IsError)
            {
                return Problem(requestToFiestaResult.Errors);
            }

            var fiesta = requestToFiestaResult.Value;
            ErrorOr<Created> createFiestaResult = _fiestaService.CreateFiesta(fiesta);
            return createFiestaResult.Match(
                created => CreatedAtCreateFiesta(fiesta),
                errors => Problem(errors)
                );
        }
        [HttpPost("GetFiestas")]
        public IActionResult GetFiestas(GetFiestaRequest request)
        {
            ErrorOr<List<Fiesta>> getFiestaResult = _fiestaService.GetFiestas(request);

            return getFiestaResult.Match(
                fiestas => Ok(MapFiestaResponse(fiestas)),
                errors => Problem(errors));
        }

        [HttpPut("UpdateFiesta")]
        public IActionResult UpdateFiesta(UpdateFiestaRequest request)
        {
            ErrorOr<Fiesta> requestToFiestaResult = Fiesta.From(request);

            if (requestToFiestaResult.IsError)
            {
                return Problem(requestToFiestaResult.Errors);
            }

            var fiesta = requestToFiestaResult.Value;
            ErrorOr<Updated> updateFiestaResult = _fiestaService.UpdateFiesta(fiesta);
            return updateFiestaResult.Match(
                updated => NoContent(),
                errors => Problem(errors));
        }

        [HttpDelete("DeleteFiesta")]
        public IActionResult DeleteFiesta(string id)
        {
            ErrorOr<Deleted> deleteFiestaResult = _fiestaService.DeleteFiesta(id);

            return deleteFiestaResult.Match(
                deleted => NoContent(),
                errors => Problem(errors));
        }
        private CreatedAtActionResult CreatedAtCreateFiesta(Fiesta fiesta)
        {
            return CreatedAtAction(
                actionName: nameof(CreateFiesta),
                routeValues: new {id = fiesta.IdFiesta },
                value: MapCreateFiestaResponse(fiesta)
                );
        }
        private CreateFiestaResponse MapCreateFiestaResponse(Fiesta fiesta)
        {
            return new CreateFiestaResponse(fiesta.IdFiesta);
        }
        private FiestaResponse MapFiestaResponse(List<Fiesta> fiestas)
        {
            return new FiestaResponse(fiestas);
        }
    }
}
