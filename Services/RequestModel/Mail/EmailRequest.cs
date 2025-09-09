using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.RequestModel.Mail
{
    public class EmailRequest
    {
        [ColumnName("dscorreo")]
        public string To { get; set; }
    }
}
