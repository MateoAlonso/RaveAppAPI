using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Services.Repository.Contracts;

namespace RaveAppAPI.Controllers
{
    public class SistemaController : ApiController
    {
        private readonly ISistemaService _sistemaService;

        public SistemaController(ISistemaService sistemaService)
        {
            _sistemaService = sistemaService;
        }
        [HttpGet("APIHealth")]
        public IActionResult GetAPIHealth()
        {
            return Ok();
        }

        [HttpGet("DBHealth")]
        public IActionResult GetDBHealth()
        {
            return _sistemaService.GetDBHealth().Match(
                            health => Ok(health),
                            errors => Problem(errors));
        }
    }
}
