using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Services.Repository.Contracts;

namespace RaveAppAPI.Controllers
{
    [Authorize]
    public class ReporteController : ApiController
    {
        private readonly IReporteService _reporteService;

        public ReporteController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpGet("ReporteVentasEvento")]
        public IActionResult ReporteVentasEvento(string idEvento)
        {
            var reporteResult = _reporteService.GetReporteVentasEvento(idEvento);

            return reporteResult.Match(
                reporte => Ok(reporte),
                errors => Problem(errors));

        }
    }
}
