using System;
using Serilog;
namespace RaveAppAPI.Services.Helpers
{
    public static class Logger
    {
        static Logger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("logs/log_.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 50)
                .CreateLogger();
        }
        
        public static void LogInfo(string message)
        {
            Log.Information(message);
        }
        
        public static void LogWarning(string message)
        { 
            Log.Logger.Warning(message);
        }

        public static void LogError(string message)
        {
            Log.Logger.Error(message);
        }
    }
}
