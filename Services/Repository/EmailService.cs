using ErrorOr;
using MySql.Data.MySqlClient;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Repository.Contracts;
using RaveAppAPI.Services.RequestModel.Mail;

namespace RaveAppAPI.Services.Repository
{
    public class EmailService : IEmailService
    {
        private readonly string connectionString = EnvHelper.GetConnectionString();
        public ErrorOr<List<EmailQrRequest>> GetEmailQrData(string idCompra)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDEmailQr, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(ProcedureHelper.EmailQrParameters(idCompra));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<EmailQrRequest> artistas = ReaderMaper.ReaderToObjectRecursive<EmailQrRequest>(reader).ToList();
                            return artistas;
                        }
                        else
                        {
                            return Error.NotFound();
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return Error.Unexpected();
            }
        }
    }
}
