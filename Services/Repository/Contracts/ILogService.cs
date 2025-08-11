using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface ILogService
    {
        void LogWebhookMP(string idCompra, string estadoPago, string setalleEstadoPago, decimal monto);
    }
}
