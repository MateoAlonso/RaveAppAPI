using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Pago;
using System.Net.Http.Headers;
using System.Text.Json;

namespace RaveAppAPI.Controllers
{
    public class PagoController : ApiController
    {
        private readonly IPagoService _pagoService;
        private readonly string _BaseUrlMPApi = EnvHelper.GetMPApi();
        public PagoController(IPagoService pagoService)
        {
            _pagoService = pagoService;
        }

        [HttpPost("CrearPago")]
        public IActionResult CreatePago(CreatePagoRequest request)
        {
            var createPagoResult = CreatePreference(request);

            return createPagoResult.Match(
                pago => Ok(pago),
                errors => Problem(errors));
        }

        private ErrorOr<CreatePagoResponse> CreatePreference(CreatePagoRequest request)
        {
            var prefReqBody = new PreferenceRequest
            {
                Items = new List<Item>
                {
                    new Item
                    {
                        Id = request.IdCompra,
                        Title = "Compra entradas",
                        Quantity = 1,
                        UnitPrice = request.Monto
                    }
                },
                Metadata = new Metadata
                {
                    IdCompra = request.IdCompra
                }
            };
            HttpRequestMessage MPRequest = APIHelper.BuildRequest(
                HttpMethod.Post,
                _BaseUrlMPApi,
                APIHelper.CreatePreference,
                prefReqBody,
                new AuthenticationHeaderValue("Bearer", EnvHelper.GetTokenMP())
            );
            using (var client = new HttpClient())
            {
                var response = client.SendAsync(MPRequest);
                response.Wait();
                if (!response.Result.IsSuccessStatusCode)
                {
                    return Error.Failure();
                }
                var PrefResponse = APIHelper.MapResponse<PreferenceResponse>(response.Result);
                return new CreatePagoResponse(PrefResponse.InitPoint);
            }
        }

        [HttpPost("NotificacionHook")]
        public IActionResult NotificacionHook(NotificacionRequest Request)
        {
//            {
//                "action": "payment.created",
                //  "api_version": "v1",
                //  "data": {
                //                    "id": "121771934776"
                //  },
                //  "date_created": "2025-08-10T18:52:01Z",
                //  "id": 123721568834,
                //  "live_mode": true,
                //  "type": "payment",
                //  "user_id": "2577279652"
                //}
                //checkear pago?
            return Ok();
        }
    }
}
