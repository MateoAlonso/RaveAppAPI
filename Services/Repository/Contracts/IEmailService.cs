using ErrorOr;
using RaveAppAPI.Services.RequestModel.Mail;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface IEmailService
    {
        ErrorOr<List<EmailQrRequest>> GetEmailQrData(string idCompra);
    }
}
