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

        public ErrorOr<List<string>> GetCorreosByIdEvento(string idEvento)
        {
            try
            {
                using (MySqlConnection dbcon = new(connectionString))
                {
                    dbcon.Open();
                    MySqlCommand cmd = new(ProcedureHelper.PCDGetCorreosEvento, dbcon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(ProcedureHelper.GetCorreosEventoParameters(idEvento));
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<string> emails = ReaderMaper.ReaderToSimpleType<string>(reader).ToList();
                            return emails;
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
                            List<EmailQrRequest> emails = ReaderMaper.ReaderToObjectRecursive<EmailQrRequest>(reader).ToList();
                            return emails;
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
