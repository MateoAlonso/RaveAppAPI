using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using FastMember;
using ZstdSharp;
using System.Data;

namespace RaveAppAPI.Services.Helpers
{
    public static class ReaderMaper
    {
        private static List<string> columns = new List<string>();
        //recieves an sqldatareader and returns a list of T
        public static IEnumerable<T> ConvertToObject<T>(MySqlDataReader rd) where T : class, new()
        {
            bool isCheckedCols = columns.Count != 0;
            while (rd.Read())
            {
                if (!isCheckedCols)
                {
                    for (int i = 0; i < rd.FieldCount; i++)
                    {
                        columns.Add(rd.GetName(i).ToLower());
                    }
                }
            }


            Type type = typeof(T);
            var accessor = TypeAccessor.Create(type);
            var members = accessor.GetMembers();
            var t = new T();

            for (int i = 0; i < rd.FieldCount; i++)
            {
                if (!rd.IsDBNull(i))
                {
                    string fieldName = rd.GetName(i);

                    if (members.Any(m => string.Equals(m.Name, fieldName, StringComparison.OrdinalIgnoreCase)))
                    {
                        accessor[t, fieldName] = rd.GetValue(i);
                    }
                }
            }

            throw new NotImplementedException();
        }
    }
}
