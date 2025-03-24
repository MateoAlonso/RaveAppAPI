using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Noticia;

namespace RaveAppAPI.Controllers
{
    [Authorize]
    public class NoticiaController : ApiController
    {
        private readonly INoticiaService _noticiaService;

        public NoticiaController(INoticiaService noticiaService)
        {
            _noticiaService = noticiaService;
        }

        [HttpPost]
        public IActionResult CreateNoticia(CreateNoticiaRequest request)
        {
            ErrorOr<Noticia> requestToNoticiaResult = Noticia.From(request);

            if (requestToNoticiaResult.IsError)
            {
                return Problem(requestToNoticiaResult.Errors);
            }

            var noticia = requestToNoticiaResult.Value;
            ErrorOr<Created> createNoticiaResult = _noticiaService.CreateNoticia(noticia);

            return createNoticiaResult.Match(
                created => CreatedAtCreateNoticia(noticia),
                errors => Problem(errors));
        }

        [HttpGet()]
        public IActionResult GetNoticias()
        {
            ErrorOr<List<Noticia>> getNoticiaResult = _noticiaService.GetNoticias();

            return getNoticiaResult.Match(
                noticias => Ok(MapNoticiaResponse(noticias)),
                errors => Problem(errors));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNoticia(/*string id, UpdateNoticiaRequest request*/)
        {
            throw new NotImplementedException();
            //ErrorOr<Noticia> requestToNoticiaResult = Noticia.From(id, request);

            //if (requestToNoticiaResult.IsError)
            //{
            //    return Problem(requestToNoticiaResult.Errors);
            //}

            //var noticia = requestToNoticiaResult.Value;
            //ErrorOr<Updated> updateNoticiaResult = _noticiaService.UpdateNoticia(noticia);

            //return updateNoticiaResult.Match(
            //                   updated => Ok(MapNoticiaResponse(noticia)),
            //                                  errors => Problem(errors));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNoticia(string id)
        {
            ErrorOr<Deleted> deleteNoticiaResult = _noticiaService.DeleteNoticia(id);

            return deleteNoticiaResult.Match(
                deleted => Ok(),
                errors => Problem(errors));
        }

        private CreatedAtActionResult CreatedAtCreateNoticia(Noticia noticia)
        {
            return CreatedAtAction(nameof(CreateNoticia),
                new { id = noticia.IdNoticia }, MapCreateNoticiaResponse(noticia));
        }

        private NoticiaResponse MapNoticiaResponse(List<Noticia> noticias)
        {
            return new NoticiaResponse(noticias);      
        }
        private CreateNoticiaResponse MapCreateNoticiaResponse(Noticia noticia)
        {
            return new CreateNoticiaResponse(noticia.IdNoticia, noticia.Titulo, noticia.Contenido, noticia.DtPublicado);      
        }
    }
}
