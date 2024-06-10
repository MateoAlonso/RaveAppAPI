using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.Helpers
{
    public static class DbHelper
    {
        public static string GetConnectionString()
        {
#if DEBUG
        return Environment.GetEnvironmentVariable("dbcon", EnvironmentVariableTarget.Machine);
#else
        return Environment.GetEnvironmentVariable("dbcon");
#endif
        }          
    }
}
