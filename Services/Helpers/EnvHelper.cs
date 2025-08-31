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
        public static string GetMPApi()
        {
#if DEBUG
            return Environment.GetEnvironmentVariable("RAVEAPP_MPApi", EnvironmentVariableTarget.Machine);
#else
            return Environment.GetEnvironmentVariable("RAVEAPP_MPApi");
#endif
        }
        public static string GetTokenMP()
        {
#if DEBUG
            return Environment.GetEnvironmentVariable("RAVEAPP_MPToken", EnvironmentVariableTarget.Machine);
#else
            return Environment.GetEnvironmentVariable("RAVEAPP_MPToken");
#endif
        }
        public static string GetMPNotifSecret()
        {
#if DEBUG
            return Environment.GetEnvironmentVariable("RAVEAPP_MPNotifSecret", EnvironmentVariableTarget.Machine);
#else
            return Environment.GetEnvironmentVariable("RAVEAPP_MPNotifSecret");
#endif
        }

        public static string GetSESAccessKey()
        {
#if DEBUG
            return Environment.GetEnvironmentVariable("RAVEAPP_SESAccessKey", EnvironmentVariableTarget.Machine);
#else
            return Environment.GetEnvironmentVariable("RAVEAPP_SESAccessKey");
#endif
        }

        public static string GetSESSecretKey()
        {
#if DEBUG
            return Environment.GetEnvironmentVariable("RAVEAPP_SESSecretKey", EnvironmentVariableTarget.Machine);
#else
            return Environment.GetEnvironmentVariable("RAVEAPP_SESSecretKey");
#endif
        }

        public static string GetJWTKey()
        {
#if DEBUG
            return Environment.GetEnvironmentVariable("RAVEAPP_SigningKey", EnvironmentVariableTarget.Machine);
#else
            return Environment.GetEnvironmentVariable("RAVEAPP_SigningKey");
#endif
        }

        public static string GetJWTIssuer()
        {
#if DEBUG
            return Environment.GetEnvironmentVariable("RAVEAPP_ValidIssuer", EnvironmentVariableTarget.Machine);
#else
            return Environment.GetEnvironmentVariable("RAVEAPP_ValidIssuer");
#endif
        }
    }
}
