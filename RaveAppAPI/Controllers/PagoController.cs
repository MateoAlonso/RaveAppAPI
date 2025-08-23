using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Pago;
using System.Net.Http.Headers;

namespace RaveAppAPI.Controllers
{
    public class PagoController : ApiController
    {
        private readonly IPagoService _pagoService;
        private readonly ILogService _logService;
        private readonly string _BaseUrlMPApi = EnvHelper.GetMPApi();
        public PagoController(IPagoService pagoService, ILogService logService)
        {
            _pagoService = pagoService;
            _logService = logService;
        }

        [HttpPost("CrearPago"), Authorize]
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
                        UnitPrice = request.Subtotal + request.CargoServicio
                    }
                },
                Metadata = new Metadata
                {
                    IdCompra = request.IdCompra
                },
                BackUrls = new BackUrls
                {
                    Success = request.BackUrl,
                    Failure = request.BackUrl,
                    Pending = request.BackUrl
                },
                PaymentMethods = new PaymentMethods
                {
                    ExcludedPaymentTypes = new List<ExcludedPaymentType>
                    {
                        new ExcludedPaymentType
                        {
                            Id = PaymentTypes.Ticket
                        },
                        new ExcludedPaymentType
                        {
                            Id = PaymentTypes.DigitalCurrency
                        },
                        new ExcludedPaymentType
                        {
                            Id = PaymentTypes.CryptoTransfer
                        },
                        new ExcludedPaymentType
                        {
                            Id = PaymentTypes.BankTransfer
                        },
                        new ExcludedPaymentType
                        {
                            Id = PaymentTypes.VoucherCard
                        }
                    },
                    Installments = 1,
                    DefaultPaymentMethodId = PaymentTypes.AccountMoney
                },
                ExternalReference = request.IdCompra,
                Expires = true,
                ExpirationDateFrom = DateTime.UtcNow,
                ExpirationDateTo = DateTime.UtcNow.AddMinutes(10),
                StatementDescriptor = "RaveApp",
                AutoReturn = "approved"
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
        public IActionResult NotificacionHook(NotificacionRequest request)
        {
            var getPaymentResult = GetPayment(request.Data.Id);
            if (getPaymentResult.IsError)
            {
                return Problem(getPaymentResult.Errors);
            }
            var payment = getPaymentResult.Value;
            _logService.LogWebhookMP(payment.Metadata.IdCompra, payment.Status, payment.StatusDetail, payment.TransactionAmount, payment.Id);
            if (payment.Status == PaymentStatus.Approved)
            {
                var finalizarCompraResult = _pagoService.FinalizarCompra(payment.Metadata.IdCompra, (int)MediosPagoEnum.MercadoPago);
                if (finalizarCompraResult.IsError)
                {
                    return Problem();
                }
            }
            return Ok();
        }

        private ErrorOr<GetPaymentResponse> GetPayment(string paymentId)
        {
            HttpRequestMessage MPRequest = APIHelper.BuildRequest(
                HttpMethod.Get,
                _BaseUrlMPApi,
                string.Format(APIHelper.GetPayment, paymentId),
                auth: new AuthenticationHeaderValue("Bearer", EnvHelper.GetTokenMP()));
            using (var client = new HttpClient())
            {
                var response = client.SendAsync(MPRequest);
                response.Wait();
                if (!response.Result.IsSuccessStatusCode)
                {
                    return Error.Failure();
                }
                var paymentResponse = APIHelper.MapResponse<GetPaymentResponse>(response.Result);
                return paymentResponse;
            }
        }
    }
}
