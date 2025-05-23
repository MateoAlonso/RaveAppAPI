namespace RaveAppAPI.Services.Helpers
{
    public static class DbHelper
    {
        public static string GetConnectionString()
        {
#if DEBUG
        return Environment.GetEnvironmentVariable("RAVEAPP_dbcon", EnvironmentVariableTarget.Machine);
#else
            return Environment.GetEnvironmentVariable("RAVEAPP_dbcon");
#endif
        }
    }
}
