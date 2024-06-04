using System.Data;
using System.Reflection;

namespace RaveAppAPI.Services.Helpers
{
    public static class ReaderMaper
    {
        public static IEnumerable<T> ReaderToObject<T>(IDataReader rd) where T : class, new()
        {
            IEnumerable<T> list = new List<T>();
            Type type = typeof(T);
            var attr = type.GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(ColumnNameAttribute)))
                .ToDictionary(p => ((ColumnNameAttribute)Attribute.GetCustomAttribute(p, typeof(ColumnNameAttribute))).Name, p => p);
            while (rd.Read())
            {
                var t = new T();
                for (int i = 0; i < rd.FieldCount; i++)
                {
                    if (!rd.IsDBNull(i))
                    {
                        string fieldName = rd.GetName(i).ToUpper();

                        if (attr.ContainsKey(fieldName))
                        {
                            PropertyInfo propInfo = attr.GetValueOrDefault(fieldName);
                            object value = rd.GetValue(i);
                            propInfo.SetValue(t, value);
                        }
                    }
                }
                yield return t;
            }
        }
    }
}
