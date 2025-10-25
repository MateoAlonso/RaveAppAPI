using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Reporte;

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
        public IActionResult ReporteVentasEvento([FromQuery] ReporteVentasEventoRequest request)
        {
            var reporteResult = _reporteService.GetReporteVentasEvento(request);

            return reporteResult.Match(
                reporte => Ok(reporte),
                errors => Problem(errors));

        }
    }
}
