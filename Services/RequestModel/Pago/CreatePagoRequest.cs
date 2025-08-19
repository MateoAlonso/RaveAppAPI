namespace RaveAppAPI.Services.RequestModel.Pago
{
    public record CreatePagoRequest(string IdCompra, decimal Subtotal, decimal CargoServicio, string BackUrl);
}
