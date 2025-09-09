namespace RaveAppAPI.Services.Helpers
{
    public class ApiMailHelper : ApiHelper
    {
        public const string FromEmail = "comunicaciones@raveapp.com.ar";
        public const string DomainName = "raveapp.com.ar";
        public const string ConfirmarMailTemplate = "EmailConfirmationTemplate";
        public const string PassRecoveryTemplate = "PassRecoveryTemplate";
        public const string EntradaQrTemplate = "EntradaQrTemplate";
        public const string BaseUrl = "https://api.mailgun.net";
        public const string MessagesEndpoint = "/v3/{0}/messages";
        public static string BuildQrEmail(string nombreEvento, string fecha, string qrHtml)
        {
            return $@"<!DOCTYPE html>
                <html>
                    <head>
                    <meta charset='UTF-8'>
                    <title>Entrada - {nombreEvento}</title>
                    <style>
                        body {{ font-family: Arial, sans-serif; color: #333; }}
                        .container {{ max-width: 600px; margin: auto; border: 1px solid #ddd; border-radius: 12px; padding: 20px; background: #f9f9f9; }}
                        .header {{ text-align: center; padding-bottom: 20px; border-bottom: 1px solid #ddd; }}
                        .event-name {{ font-size: 24px; font-weight: bold; color: #2c3e50; }}
                        .details {{ margin: 20px 0; }}
                        .qr {{ text-align: center; margin: 30px 0; }}
                        .footer {{ font-size: 12px; color: #888; text-align: center; margin-top: 30px; }}
                    </style>
                    </head>
                    <body>
                    <div class='container'>
                        <div class='header'>
                        <div class='event-name'>{nombreEvento}</div>
                        <p>Tu entrada digital</p>
                        </div>
                        <div class='details'>
                        <p><strong>Fecha:</strong> {fecha}</p>
                        </div>
                        <div class='qr'>
                        <p>Presenta este código en la entrada:</p>
                        {qrHtml}
                        </div>
                        <div class='footer'>
                        <p>Gracias por tu compra! <br/>No compartas este QR con terceros. 
                        El equipo de <strong>RaveApp</strong></p>
                        </div>
                    </div>
                    </body>
                </html>";
        }
        public static string BuildQrSection(string inlineQr, string tipoEntrada) 
        {
            return $@"<p><strong>{tipoEntrada}</strong></p>
                        <img src=""cid:{inlineQr}"" width=""200"" height=""200"" />";
        }
    }
}
