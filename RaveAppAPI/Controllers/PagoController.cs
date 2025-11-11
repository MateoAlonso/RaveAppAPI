using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Sec;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Repository;
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

            if (!createPagoResult.IsError)
            {
                _pagoService.RefrescarTimerReserva(request.IdCompra);
                _pagoService.PendienteCompra(request.IdCompra, request.Subtotal, request.CargoServicio);
            }

            return createPagoResult.Match(
                pago => Ok(pago),
                errors => Problem(errors));
        }
        [HttpPost("Reembolso"), Authorize]
        public IActionResult Reembolso(string idCompra)
        {
            var getDatosResult = _pagoService.GetDatosReembolso(idCompra);

            if (getDatosResult.IsError)
            {
                return Problem(getDatosResult.Errors);
            }
            var datos = getDatosResult.Value.FirstOrDefault();

            var createRefundResult = CreateRefund(datos.IdMP, datos.Monto);

            if (!createRefundResult.IsError)
            {
                _pagoService.Reembolso(idCompra);
            }

            return createRefundResult.Match(
                response => Ok(response),
                errors => Problem(errors));
        }
        [HttpPost("ReembolsoMasivo"), Authorize]
        public IActionResult ReembolsoMasivo(string idEvento)
        {
            var getDatosResult = _pagoService.GetDatosReembolsoMasivo(idEvento);

            if (getDatosResult.IsError)
            {
                return Problem(getDatosResult.Errors);
            }
            var datos = getDatosResult.Value;

            List<string> comprasError = new List<string>();

            foreach (var item in datos)
            {
                var createRefundResult = CreateRefund(item.IdMP, item.Monto);
                if (!createRefundResult.IsError)
                {
                    _pagoService.Reembolso(item.IdCompra);
                }
                else
                {
                    comprasError.Add(item.IdCompra);
                }
            }

            return Ok(new { ComprasError = comprasError } );
        }
        private ErrorOr<RefundResponse> CreateRefund(long idMP, decimal monto)
        {
            var refundReqBody = new RefundRequest
            {
                Amount = monto
            };
            Dictionary<string, string?> headers = new Dictionary<string, string?> { };
            headers.Add("X-Idempotency-Key", Guid.NewGuid().ToString());
            HttpRequestMessage MPRequest = MpApiHelper.BuildRequest(
                HttpMethod.Post,
                _BaseUrlMPApi,
                string.Format(MpApiHelper.RefundPayment, idMP),
                refundReqBody,
                new AuthenticationHeaderValue("Bearer", EnvHelper.GetTokenMP()),
                headers
            );
            using (var client = new HttpClient())
            {
                var response = client.SendAsync(MPRequest);
                response.Wait();
                if (!response.Result.IsSuccessStatusCode)
                {
                    return Error.Failure();
                }
                var refundResponse = MpApiHelper.MapResponse<RefundResponse>(response.Result);
                return refundResponse;
            }
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
                ExpirationDateTo = DateTime.UtcNow.AddMinutes(5),
                StatementDescriptor = "RaveApp",
                AutoReturn = PaymentStatus.Approved
            };
            HttpRequestMessage MPRequest = MpApiHelper.BuildRequest(
                HttpMethod.Post,
                _BaseUrlMPApi,
                MpApiHelper.CreatePreference,
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
                var PrefResponse = MpApiHelper.MapResponse<PreferenceResponse>(response.Result);
                return new CreatePagoResponse(PrefResponse.InitPoint);
            }
        }

        [HttpPost("NotificacionHook")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> NotificacionHook(NotificacionRequest request)
        {
            var getPaymentResult = GetPayment(request.Data.Id);
            if (getPaymentResult.IsError)
            {
                return Problem(getPaymentResult.Errors);
            }
            var payment = getPaymentResult.Value;
            _logService.LogWebhookMP(payment.Metadata.IdCompra, payment.Status, payment.StatusDetail, payment.TransactionAmount, payment.Id);
            if (!await ProcessPayment(payment.StatusDetail, payment.Metadata.IdCompra))
            {
                return Problem();
            }
            return Ok();
        }
        [HttpPost("PagoMP")]
        public async Task<IActionResult> PagoMP(string idPagoMP)
        {
            var getPaymentResult = GetPayment(idPagoMP);
            if (getPaymentResult.IsError)
            {
                return Problem(getPaymentResult.Errors);
            }
            var payment = getPaymentResult.Value;
            _logService.LogWebhookMP(payment.Metadata.IdCompra, payment.Status, payment.StatusDetail, payment.TransactionAmount, payment.Id);
            if (!await ProcessPayment(payment.StatusDetail, payment.Metadata.IdCompra))
            {
                return Problem();
            }
            return Ok();
        }
        private async Task<bool> ProcessPayment(string status, string idCompra)
        {
            switch (status)
            {
                case PaymentStatus.Approved:
                case PaymentStatus.Authorized:
                case PaymentStatus.Accredited:
                    var finalizarCompraResult = _pagoService.FinalizarCompra(idCompra, (int)MediosPagoEnum.MercadoPago);
                    if (finalizarCompraResult.IsError)
                    {
                        return false;
                    }
                    EntradaController entradaController = new EntradaController(new EntradaService());
                    await entradaController.GenerarQrEntradas(finalizarCompraResult.Value);
                    EmailController emailController = new EmailController(new EmailService());
                    await emailController.EnviarMailsQR(idCompra);
                    return true;
                case PaymentStatus.Rejected:
                case PaymentStatus.Cancelled:
                case PaymentStatus.Refunded:
                case PaymentStatus.ChargedBack:
                case PaymentStatus.PartiallyRefunded:
                    return !_pagoService.AnularCompra(idCompra).IsError;
                case PaymentStatus.Pending:
                    return true; // La compra ya esta pendiente, no se hace nada
                default:
                    return false;
            }
        }

        private ErrorOr<GetPaymentResponse> GetPayment(string paymentId)
        {
            try
            {
                HttpRequestMessage MPRequest = MpApiHelper.BuildRequest(
                    HttpMethod.Get,
                    _BaseUrlMPApi,
                    string.Format(MpApiHelper.GetPayment, paymentId),
                    auth: new AuthenticationHeaderValue("Bearer", EnvHelper.GetTokenMP()));
                using (var client = new HttpClient())
                {
                    var response = client.SendAsync(MPRequest);
                    response.Wait();
                    if (!response.Result.IsSuccessStatusCode)
                    {
                        Logger.LogError($"Error recuperando pago para ID {paymentId}: {response.Result.ReasonPhrase}");
                        return Error.Failure();
                    }
                    var paymentResponse = MpApiHelper.MapResponse<GetPaymentResponse>(response.Result);
                    return paymentResponse;
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return Error.Failure();
            }
        }
    }
}
