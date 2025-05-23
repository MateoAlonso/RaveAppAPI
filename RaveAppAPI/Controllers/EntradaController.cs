using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Entrada;

namespace RaveAppAPI.Controllers
{
    [Authorize]
    public class EntradaController : ApiController
    {
        private readonly IEntradaService _entradaService;
        public EntradaController(IEntradaService entradaService)
        {
            _entradaService = entradaService;
        }
        public IActionResult CreateEntradas(CreateEntradaRequest request)
        {
            ErrorOr<Entrada> requestToEntradaResult = Entrada.From(request);

            if (requestToEntradaResult.IsError)
            {
                return Problem(requestToEntradaResult.Errors);
            }

            var entrada = requestToEntradaResult.Value;

            ErrorOr<Created> createEntradaResult = _entradaService.CreateEntrada(entrada);
            return createEntradaResult.Match(
                created => CreatedAtCreateEntrada(entrada),
                errors => Problem(errors)
                );
        }

        private IActionResult CreatedAtCreateEntrada(Entrada entrada)
        {
            return CreatedAtAction(
                actionName: nameof(CreateEntradas),
                routeValues: new { cantidad = entrada.Cantidad },
                value: entrada);
        }
    }
    
}
