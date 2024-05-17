using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Services.Repository.Contracts;

namespace RaveAppAPI.Controllers
{
    public class HealthController : ApiController
    {
        private readonly IHealthService _healthService;

        public HealthController(IHealthService healthService)
        {
            _healthService = healthService;
        }
        [HttpGet("API")]
        public IActionResult GetAPIHealth()
        {
            return Ok();
        }

        [HttpGet("DB")]
        public IActionResult GetDBHealth()
        {
            return _healthService.GetDBHealth().Match(
                            health => Ok(health),
                            errors => Problem(errors));
        }
    }
}
