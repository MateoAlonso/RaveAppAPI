using QRCoder;

namespace RaveAppAPI.Services.Helpers
{
    public static class QRHelper
    {
        public static byte[] GenerateQRCode(string content)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            var qrCodePngData = new PngByteQRCode(qrCodeData);
            return qrCodePngData.GetGraphic(20);
        }
    }
}
