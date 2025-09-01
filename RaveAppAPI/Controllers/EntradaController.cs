using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.Repository;
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
        [HttpPost("CrearEntradas")]
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
        [HttpGet("GetEntradasFecha")]
        public IActionResult GetEntradasFecha([FromQuery] GetEntradasFechaRequest request)
        {
            ErrorOr<List<Entrada>> getEntradasFechaResult = _entradaService.GetEntradasFecha(request);
            return getEntradasFechaResult.Match(
                entradas => Ok(entradas),
                errors => Problem(errors));
        }
        [HttpPut("ReservarEntradas")]
        public IActionResult ReservarEntradas(ReservarEntradasRequest request)
        {
            ErrorOr<string> reservarEntradasResult = _entradaService.ReservarEntradas(request);
            return reservarEntradasResult.Match(
                idCompra => Ok(idCompra),
                errors => Problem(errors));
        }
        [HttpPut("CancelarReserva")]
        public IActionResult CancelarReserva(string idCompra)
        {
            ErrorOr<Updated> cancelarReservaResult = _entradaService.CancelarReserva(idCompra);
            return cancelarReservaResult.Match(
                updated => NoContent(),
                errors => Problem(errors));
        }
        [HttpGet("GetReservaActiva")]
        public IActionResult GetReservaActiva(string idUsuario)
        {
            ErrorOr<List<GetReservaActivaDTO>> getReservaActivaResult = _entradaService.GetReservaActiva(idUsuario);

            return getReservaActivaResult.Match(
                reserva => Ok(reserva),
                errors => Problem(errors));
        }
        [HttpGet("GetEstadosEntrada")]
        public IActionResult GetEstadosEntrada()
        {
            ErrorOr<List<Estado>> getEstadosEntradaResult = _entradaService.GetEstadosEntrada();

            return getEstadosEntradaResult.Match(
                estados => Ok(estados),
                errors => Problem(errors));
        }
        [HttpGet("GetTiposEntrada")]
        public IActionResult GetTiposEntrada()
        {
            ErrorOr<List<Tipo>> getTiposEntradaResult = _entradaService.GetTiposEntrada();

            return getTiposEntradaResult.Match(
                tipos => Ok(tipos),
                errors => Problem(errors));
        }
        [HttpPut("UpdateEntrada")]
        public IActionResult UpdateEntrada(UpdateEntradaRequest request)
        {
            ErrorOr<Entrada> requestToEntradaResult = Entrada.From(request);

            if (requestToEntradaResult.IsError)
            {
                return Problem(requestToEntradaResult.Errors);
            }

            var entrada = requestToEntradaResult.Value;

            ErrorOr<Updated> updateEntradaResult = _entradaService.UpdateEntrada(entrada);
            return updateEntradaResult.Match(
                updated => NoContent(),
                errors => Problem(errors));
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public async void GenerarQrEntradas(List<string> entradas)
        {
            foreach (var entrada in entradas)
            {
                string uuid = Guid.NewGuid().ToString();
                string QrContent = $"{entrada},{uuid}";
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(QrContent, QRCodeGenerator.ECCLevel.Q);
                var qrCodePngData = new PngByteQRCode(qrCodeData);
                var qrCode = qrCodePngData.GetGraphic(20);
                MediaController media = new MediaController(new MediaService());
                var res = await media.CrearMediaQrEntrada(qrCode, entrada);
                if (!res.IsError)
                {
                    _entradaService.SetQrEntrada(entrada, uuid);
                }
            }
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
