using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Services.Repository.Contracts;

namespace RaveAppAPI.Controllers
{
    public class SistemaController : ApiController
    {
        private readonly ISistemaService _healthService;

        public SistemaController(ISistemaService healthService)
        {
            _healthService = healthService;
        }
        [HttpGet("APIHealth")]
        public IActionResult GetAPIHealth()
        {
            return Ok();
        }

        [HttpGet("DBHealth")]
        public IActionResult GetDBHealth()
        {
            return _healthService.GetDBHealth().Match(
                            health => Ok(health),
                            errors => Problem(errors));
        }
    }
}
