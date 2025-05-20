using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Resenia;

namespace RaveAppAPI.Controllers
{
    [Authorize]
    public class ReseniaController : ApiController
    {
        private readonly IReseniaService _reseniaService;
        public ReseniaController(IReseniaService reseniaService)
        {
            _reseniaService = reseniaService;
        }
        [HttpPost]
        public IActionResult CreateResenia(CreateReseniaRequest request)
        {
            ErrorOr<Resenia> requestToReseniaResult = Resenia.From(request);

            if (requestToReseniaResult.IsError)
            {
                return Problem(requestToReseniaResult.Errors);
            }

            var resenia = requestToReseniaResult.Value;
            ErrorOr<Created> CreateReseniaResult = _reseniaService.CreateResenia(resenia);

            return CreateReseniaResult.Match(
                created => CreatedAtCreateResenia(resenia),
                errors => Problem(errors));
        }
        [HttpGet("GetResenias")]
        public IActionResult GetResenias([FromQuery] GetReseniaRequest request)
        {
            ErrorOr<List<Resenia>> getReseniaResult = _reseniaService.GetResenias(request);

            return getReseniaResult.Match(
                resenias => Ok(MapReseniaResponse(resenias)),
                errors => Problem(errors));
        }
        [HttpGet("GetAvgResenias")]
        public IActionResult GetAvgResenias([FromQuery] GetAvgReseniaRequest request)
        {
            ErrorOr<List<AvgReseniaDTO>> getReseniaResult = _reseniaService.GetAvgResenias(request);

            return getReseniaResult.Match(
                avgResenias => Ok(MapAvgReseniasResponse(avgResenias)),
                errors => Problem(errors));
        }
        [HttpPut]
        public IActionResult UpdateResenia(UpdateReseniaRequest request)
        {
            ErrorOr<Resenia> requestToReseniaResult = Resenia.From(request);

            if (requestToReseniaResult.IsError)
            {
                return Problem(requestToReseniaResult.Errors);
            }

            var resenia = requestToReseniaResult.Value;
            ErrorOr<Updated> updateReseniaResult = _reseniaService.UpdateResenia(resenia);

            return updateReseniaResult.Match(
                    updated => NoContent(),
                    errors => Problem(errors)
                );
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteResenia(string id)
        {
            ErrorOr<Deleted> deleteReseniaResult = _reseniaService.DeleteResenia(id);

            return deleteReseniaResult.Match(
                    deleted => Ok(),
                    errors => Problem(errors));
        }
        private ReseniaResponse MapReseniaResponse(List<Resenia> resenias)
        {
            return new ReseniaResponse(resenias);
        }
        private GetAvgReseniaResponse MapAvgReseniasResponse(List<AvgReseniaDTO> avgResenias)
        {
            return new GetAvgReseniaResponse(avgResenias);
        }
        private IActionResult CreatedAtCreateResenia(Resenia resenia)
        {
            return CreatedAtAction(nameof(CreateResenia),
                new { id = resenia.IdResenia }, MapCreateReseniaResponse(resenia));
        }
        private CreateReseniaResponse MapCreateReseniaResponse(Resenia resenia)
        {
            return new CreateReseniaResponse(resenia.IdResenia, resenia.IdUsuario, resenia.Correo, resenia.IdFiesta, resenia.DtInsert, resenia.Comentario, resenia.Estrellas);
        }
    }
}
