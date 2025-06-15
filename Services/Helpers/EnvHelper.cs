namespace RaveAppAPI.Services.Helpers
{
    public static class EnvHelper
    {
        public static string GetConnectionString()
        {
#if DEBUG
            return Environment.GetEnvironmentVariable("RAVEAPP_dbcon", EnvironmentVariableTarget.Machine);
#else
            return Environment.GetEnvironmentVariable("RAVEAPP_dbcon");
#endif
        }
        public static string GetBucketName()
        {
#if DEBUG
            return Environment.GetEnvironmentVariable("RAVEAPP_bucketname", EnvironmentVariableTarget.Machine);
#else
            return Environment.GetEnvironmentVariable("RAVEAPP_bucketname");
#endif
        }
        public static string GetAccessKey()
        {
#if DEBUG
            return Environment.GetEnvironmentVariable("RAVEAPP_accesskey", EnvironmentVariableTarget.Machine);
#else
            return Environment.GetEnvironmentVariable("RAVEAPP_accesskey");
#endif
        }
        public static string GetSecretKey()
        {
#if DEBUG
            return Environment.GetEnvironmentVariable("RAVEAPP_secretkey", EnvironmentVariableTarget.Machine);
#else
            return Environment.GetEnvironmentVariable("RAVEAPP_secretkey");
#endif
        }
        public static string GetR2Endpoint()
        {
#if DEBUG
            return Environment.GetEnvironmentVariable("RAVEAPP_r2endpoint", EnvironmentVariableTarget.Machine);
#else
            return Environment.GetEnvironmentVariable("RAVEAPP_r2endpoint");
#endif
        }
    }
}
